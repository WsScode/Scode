using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCPanel : BasePanel  {

    private Text m_Name;
    private Button m_FunctionButton;
    private Button m_TaskButton;
    private Button m_ChatButton;
    private Button m_CloseButton;


    public NPC m_NPC;

    public override void Awake()
    {
        base.Awake();
        m_FunctionButton = transform.Find("FunctionButton").GetComponent<Button>();
        m_TaskButton = transform.Find("TaskButton").GetComponent<Button>();
        m_ChatButton = transform.Find("ChatButton").GetComponent<Button>();
        m_CloseButton = transform.Find("CloseButton").GetComponent<Button>();
        m_CloseButton.onClick.AddListener(delegate() {
            OnClose();
            GameObject.Find("Cameras").transform.Find("Camera1").gameObject.SetActive(false);
            });
    }


    public override void OnEnter(UIController uIController)
    {
        base.OnEnter(uIController);
    }

    /// <summary>
    /// 初始化面板的显示内容和属性
    /// </summary>
    public void SetPanelInfo(NPCData data)
    {
        
        //UIController._instance.ShowPanel();
        m_FunctionButton.onClick.AddListener(delegate() {
            //怎么传递商店物品的配置数据 
            UIController._instance.ShowPanel((UIPanelType)System.Enum.Parse(typeof(UIPanelType), data.M_NpcType + ""));
            
        });
        m_FunctionButton.transform.Find("Text").GetComponent<Text>().text = data.M_TypeText;
        //先判断（人物等级 任务前置条件等）当前是否有任务需要发布
        //TODO

        m_ChatButton.onClick.AddListener(delegate () {
            UIController._instance.ShowPanel((UIPanelType)System.Enum.Parse(typeof(UIPanelType), data.M_NpcType + ""));
        });


    }


    //public void ShowPanel()
    //{

    //}


    void ShowNPCTask()
    {

    }

    void ShowDialoguePanel() { }


    
}
