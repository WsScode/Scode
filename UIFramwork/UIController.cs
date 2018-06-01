using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

/// <summary>
/// 原理 用栈来管理UI 依据栈顶的BasePanel对象来显示UI 
/// 利用词典 将key设置为UIPanelType 来得到path（实例化用） 和 Basepanel(调用方法)
/// ******先将常用的实例化出来 但是不显示*******
/// </summary>

public class UIController : MonoBehaviour {

    public static  UIController _instance;
    

    //保存解析的所有UIpanelType和地址 方便根据
    public Dictionary<UIPanelType, string> paneltype_PathDic;
    public Dictionary<UIPanelType, BasePanel> paneltype_BasePanelDic;//实例化过的全部（包含隐藏的）panel
    public Dictionary<UIPanelType, BasePanel> current_ShowPanel;//当前正在显示（未隐藏）的panel 方便全部关闭

    public Stack<BasePanel> basePanlStack;//创建栈来保存BasePanel

    
    private Transform mParent;
    private BasePanel basePanel;

    private string message = "";

    private void Awake()
    {
        
        ParsePanelInfo();
        //GameObject can = GameObject.FindGameObjectWithTag("Canvas");
        //DontDestroyOnLoad(can);
        mParent = GameObject.FindGameObjectWithTag("UIParent").GetComponent<Transform>();
        _instance = this;


    }
    private void Start()
    {
        transform.position = new Vector3(0,0,0);
        //PushPanel(UIPanelType.MainMenu);//默认先开启主界面UI
        //ShowPanel(UIPanelType.MainMenu);
        InitMainPanel();
        basePanel = null;

    }

    /// <summary>
    /// 解析信息 得到PanelType 和 path
    /// </summary>
    void ParsePanelInfo() {
        TextAsset panelInfo = Resources.Load<TextAsset>("UIPanelTypeJ"); 
        JsonData info = JsonMapper.ToObject(panelInfo.text); 
        paneltype_PathDic = new Dictionary<UIPanelType, string>();
       
        if (info.Count == 0) { Debug.Log("未解析到信息"); }
        foreach (JsonData temp in info) {//此时temp为Object类型
            
            UIPanelType pt = (UIPanelType) System.Enum.Parse(typeof(UIPanelType),temp["panelTypeString"].ToString());//解析json文件中的UIPanelType
            string  path = temp["path"].ToString();
            paneltype_PathDic.Add(pt,path);

        }
    }

    /// <summary>
    /// 得到panel 但是不实例化
    /// </summary>
   public  BasePanel GetPanelByType(UIPanelType panelType) {
        if (paneltype_BasePanelDic == null)
        {
            paneltype_BasePanelDic = new Dictionary<UIPanelType, BasePanel>();
        }
        BasePanel bp;
        bp = paneltype_BasePanelDic.TryGetTool(panelType);
        if (bp == null)
        {
            string path = GetPath(panelType);
            GameObject panel = Instantiate(Resources.Load<GameObject>(path),mParent);
            bp = panel.GetComponent<BasePanel>();
            bp.OnEnter(this);
            bp.OnPause();
            paneltype_BasePanelDic.Add(panelType,bp);
        }
        return bp;
    }

    /// <summary>
    /// 根据UI显示类型来决定哪个调用方法
    /// </summary>
    public void ShowPanel(UIPanelType uiPanelType)
    { 
        BasePanel basepanel = GetPanelByType(uiPanelType);
        if (basepanel == null) return;
        switch (basepanel.m_showUIType)
        {
            case ShowUIType.Normal:
                NormalShow(uiPanelType);
                break;
            case ShowUIType.PopMethod:
                PushPanel(uiPanelType);
                break;
            case ShowUIType.HideOther:
                HideOtherShow(uiPanelType);
                break;
        }
    }


    /// <summary>
    /// normalType类型的UI显示  一般显示
    /// 先在paneltype_BasePanelDic查找是否显示过 没有就新创建 有就调用OnResume
    /// </summary>
    public void NormalShow(UIPanelType uiPanelType) {

        if (current_ShowPanel == null) { current_ShowPanel = new Dictionary<UIPanelType, BasePanel>(); }
        if (paneltype_BasePanelDic == null)
        {
            paneltype_BasePanelDic = new Dictionary<UIPanelType, BasePanel>();
        }
        basePanel = paneltype_BasePanelDic.TryGetTool(uiPanelType);

        if (basePanel != null)
        {
            //if (basePanel is MessageBoxPanel) {
            //    ((MessageBoxPanel)basePanel).SetMessage(message);
            //    ((MessageBoxPanel)basePanel).OnResume();
            //    return;
            //}
            basePanel.OnResume();
            if (current_ShowPanel.ContainsKey(uiPanelType)) return;//防止多次点击显示按钮
            current_ShowPanel.Add(uiPanelType, basePanel);
            return;
        }
        else
        {
            BasePanel basepanel = Instantiate(Resources.Load<GameObject>(GetPath(uiPanelType)), mParent).GetComponent<BasePanel>();
            //if (basepanel is MessageBoxPanel)
            //{
            //    ((MessageBoxPanel)basepanel).SetMessage(message);
            //}
            basepanel.OnEnter(this);
            if (current_ShowPanel.ContainsKey(uiPanelType))
            {
                return;
            }
            paneltype_BasePanelDic.Add(uiPanelType, basepanel);
            current_ShowPanel.Add(uiPanelType, basepanel); 
        }
        
       
    }

    /// <summary>
    /// 隐藏界面 并从显示列表中移除
    /// </summary>
    /// <param name="uipanelType"></param>
    public void Hide(UIPanelType uipanelType) {
        if (current_ShowPanel.ContainsKey(uipanelType) == false) {
            Debug.LogError("current_ShowPanel中没有Key:" + uipanelType + "无法移除");
            return;
        }

        current_ShowPanel.Remove(uipanelType);

        if (paneltype_BasePanelDic.TryGetTool(uipanelType).m_showUIType == ShowUIType.HideOther)
        {
            foreach (KeyValuePair<UIPanelType,BasePanel> kv in current_ShowPanel)
            {
                kv.Value.OnResume();
            }
        }

        
    }


    /// <summary>
    /// 只显示这一个 隐藏其余全部的UI 只用作需要全屏显示的UI的时候
    /// </summary>
    private void HideOtherShow(UIPanelType panelType) {
        //TODO
        
        if (current_ShowPanel == null) current_ShowPanel = new Dictionary<UIPanelType, BasePanel>();
        if (paneltype_BasePanelDic == null) paneltype_BasePanelDic = new Dictionary<UIPanelType, BasePanel>();
        BasePanel bp = paneltype_BasePanelDic.TryGetTool(panelType);
        //if (current_ShowPanel.Count == 0) return;
        if (current_ShowPanel.Count > 0)
        {
            foreach (KeyValuePair<UIPanelType, BasePanel> kv in current_ShowPanel)
            {
                kv.Value.OnPause();//此时不从移除current_ShowPanel移除 
            }
        }

        if (bp != null)
        {
           
            bp.OnResume(); 
            if (current_ShowPanel.ContainsKey(panelType)) return;//防止多次显示
            current_ShowPanel.Add(panelType, basePanel);
            return;

        }
        BasePanel basepanel = Instantiate(Resources.Load<GameObject>(GetPath(panelType)), mParent).GetComponent<BasePanel>();
        basepanel.OnEnter(this);
        if (current_ShowPanel.ContainsKey(panelType))
        {
            return;
        }
        paneltype_BasePanelDic.Add(panelType, basepanel);
        current_ShowPanel.Add(panelType, basepanel);

    }


    public void HideOtherHide(UIPanelType panelType) {
        //TODO



    }


    
    /// <summary>
    /// 将需要显示的panel放入栈顶 并显示  
    /// isPauseElse是否需要暂停此UIpanel以外的UIpanel的操作
    /// </summary>
    public void PushPanel(UIPanelType uiPanelType) {
        
        basePanel = GetPanel(uiPanelType);

        if (basePanlStack == null) {
            basePanlStack = new Stack<BasePanel>();
        }
        if (basePanlStack.Count > 0)
        {
            BasePanel topPanel = basePanlStack.Peek();//得到栈顶对象
            topPanel.OnPause();

        }
        //将页面放入栈顶  并显示
        basePanlStack.Push(basePanel);

        //Instantiate(Resources.Load<GameObject>(GetPath(uiPanelType)), mParent).GetComponent<BasePanel>().OnEnter(this);
        basePanel.OnEnter(this);
    }





    /// <summary>
    /// 页面对象出栈 关闭页面 如果后面还有页面 就显示后一个
    /// 默认移除栈顶 但是这必须是只显示栈顶 否则会导致关闭的可能不是想关闭的那个UI
    /// </summary>
    public void PopPanel() {
        if (basePanlStack == null) {
            basePanlStack = new Stack<BasePanel>();
        }
        if (basePanlStack.Count == 0) return;
        BasePanel topPanel = basePanlStack.Pop();
        bool isCon = basePanlStack.Contains(topPanel);

        topPanel.OnPause();
        if (basePanlStack.Count == 0) return;//加入当前只有一个 所以再一次判断
        BasePanel topPanel2 = basePanlStack.Peek();
        topPanel2.OnResume();

    }


    public void PopPanel(BasePanel basePanel)
    {
        if (basePanlStack == null)
        {
            basePanlStack = new Stack<BasePanel>();
        }
        if (basePanlStack.Count == 0) return;
        foreach (BasePanel m_basepanel in basePanlStack) {
            if (m_basepanel == basePanel) {
                
                basePanel.OnPause();
            }
        }

    }


    /// <summary>
    /// 得到basepanel 并实例化 
    /// </summary>
    /// <param name="panelType"></param>
    /// <returns></returns>
    public BasePanel GetPanel(UIPanelType panelType) {
        if (paneltype_BasePanelDic == null) {
            paneltype_BasePanelDic = new Dictionary<UIPanelType, BasePanel>();
        }
        BasePanel bp;
        bp = paneltype_BasePanelDic.TryGetTool(panelType);
        if (bp == null)
        {
            string path = GetPath(panelType);
            GameObject panel = (GameObject)Instantiate(Resources.Load(path), mParent);//实力化 显示UI
            //GameObject panel = Resources.Load<GameObject>(path);
            bp = panel.GetComponent<BasePanel>();
            paneltype_BasePanelDic.Add(panelType, bp);//此时字典就保存了已经显示过的UIpanel了
        }
        return bp;
    }

    public string GetPath(UIPanelType panelType) {
        return paneltype_PathDic.TryGetTool(panelType);
    }


    public void SetMessage(string msg) {
        message = msg;
    }

    public GameObject GetPanelGameObject(string panelName) {
        return mParent.Find(panelName).gameObject;
    }




     /// <summary>
     /// 初始化所有UIPanelType中的UI 并关闭 MainPanel 和 FIghtButtonPanel 以外的所有UI
     /// </summary>
    private void InitMainPanel()
    {
        if (current_ShowPanel == null) current_ShowPanel = new Dictionary<UIPanelType, BasePanel>();
        if (paneltype_BasePanelDic == null){paneltype_BasePanelDic = new Dictionary<UIPanelType, BasePanel>();}
        foreach (UIPanelType ui in Enum.GetValues(typeof(UIPanelType)))
        {
            BasePanel bp;
            bp = paneltype_BasePanelDic.TryGetTool(ui);
            if (bp == null)
            {
                string path = GetPath(ui);
                GameObject panel = (GameObject)Instantiate(Resources.Load(path), mParent);//实力化 显示UI
                bp = panel.GetComponent<BasePanel>();
                paneltype_BasePanelDic.Add(ui, bp);//此时字典就保存了已经显示过的UIpanel了
                bp.OnEnter(this);
                if (ui == UIPanelType.MainMenu || ui == UIPanelType.FightButton)
                {
                    current_ShowPanel.Add(ui, bp);
                    continue;//不暂停主界面
                } 
                bp.OnPause();
            }
        }
    }


}
