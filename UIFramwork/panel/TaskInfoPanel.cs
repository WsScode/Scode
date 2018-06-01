using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskInfoPanel : BasePanel {


    public Task M_ShowTask;//需要显示的任务信息

    private Text m_TaskTitle;
    private Text m_TaskContent;
    private Transform m_Award;//奖励物品内容 （物品 经验 钱币）
    private Transform m_AwardItem;
    private Text m_AwardEXP;
    private Text m_AwardCoin;
    private Button m_Btn;

    List<MyEvent.MyEventType> evtList = new List<MyEvent.MyEventType> { MyEvent.MyEventType.TaskAccept, MyEvent.MyEventType.TaskFinish,MyEvent.MyEventType.TaskAbort };
    MyEvent evt;

    public override void Awake()
    {
        base.Awake();
        m_showUIType = ShowUIType.Normal;

        m_TaskTitle = transform.Find("TaskTitle").GetComponent<Text>();
        m_TaskContent = transform.Find("TaskContent").GetComponent<Text>();
        m_AwardItem = transform.Find("AwardContent/AwardItem");
        m_AwardEXP = transform.Find("AwardContent/EXPAward").GetComponent<Text>();
        m_AwardCoin = transform.Find("AwardContent/CoinAward").GetComponent<Text>();
        m_Btn = transform.Find("AcceptButton").GetComponent<Button>();

        RegisterEvent();
        evt = new MyEvent(MyEvent.MyEventType.TaskAccept);
    }


    private void Start()
    {
       
        ////任务按钮点击事件（接受 放弃）
        //MyEvent.MyEventType evtype = MyEvent.MyEventType.TaskAccept;
        //if (M_ShowTask.M_TaskState == Task.TaskState.Normal)
        //{
        //    evtype = MyEvent.MyEventType.TaskAccept;
        //}
        //else if (M_ShowTask.M_TaskState == Task.TaskState.Accepted)
        //{
        //    evtype = MyEvent.MyEventType.TaskAbort;//接受还未完成 只有放弃一个事件
        //}
        //else if (M_ShowTask.M_TaskState == Task.TaskState.Reach)
        //{
        //    evtype = MyEvent.MyEventType.TaskFinish;
        //}

        //evt = new MyEvent(evtype);
        //evt.m_Task = M_ShowTask;
        //m_Btn.onClick.AddListener(delegate () { MyEventSystem.m_MyEventSystem.PushEvent(evt); });
    }





    public override void OnEnter(UIController uIController)
    {
        base.OnEnter(uIController);
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;//检测点击
    }


    public override void OnPause()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;//检测点击
    }

    private void Update()
    {
        
    }


    void RegisterEvent()
    {
        foreach (MyEvent.MyEventType evt in evtList)
        {
            MyEventSystem.m_MyEventSystem.RegisterEvent(evt, OnEvent);
        }
    }


    protected override void OnEvent(MyEvent evt)
    {
        switch (evt.m_MyEventType)
        {
            case MyEvent.MyEventType.TaskAccept:
                AcceptUpdate();
                break;
            case MyEvent.MyEventType.TaskFinish:
                //AbortUpdate();
                break;
            case MyEvent.MyEventType.TaskAbort:
                AbortUpdate();
                break;
        }
    }

    /// <summary>
    /// 接受任务后面板更新
    /// </summary>
    void AcceptUpdate()
    {
        M_ShowTask.Accept();
        m_Btn.transform.Find("Text").GetComponent<Text>().text = "放弃";
        AddBtnListener(MyEvent.MyEventType.TaskAbort);
        transform.GetComponent<Image>().sprite = LoadSprite.LoadSpriteByName("CommonUI/RestaurantAtlas", "RestaurantAtlas_1");
    }
    /// <summary>
    /// 放弃任务的更新
    /// </summary>
    void AbortUpdate()
    {
        M_ShowTask.GiveUP();
        m_Btn.transform.Find("Text").GetComponent<Text>().text = "接受";
        AddBtnListener(MyEvent.MyEventType.TaskAccept);
        transform.GetComponent<Image>().sprite = LoadSprite.LoadSpriteByName("CommonUI/RestaurantAtlas", "RestaurantAtlas_3");
    }
    ///// <summary>
    ///// 完成任务  显示提示UI
    ///// </summary>
    //void FinishUpdate()
    //{
    //    //TODO
    //}

    void AddBtnListener(MyEvent.MyEventType evtype)
    {
        
        m_Btn.onClick.RemoveAllListeners();
        evt.m_MyEventType = evtype;
        evt.m_Task = M_ShowTask;
        m_Btn.onClick.AddListener(delegate () { MyEventSystem.m_MyEventSystem.PushEvent(evt); });
    }

    /// <summary>
    /// 技能面板显示时的初始信息
    /// </summary>
    public override void UpdateShow()
    {
        if (M_ShowTask == null) return;
        if (M_ShowTask.M_TaskState == Task.TaskState.Accepted)
        {
            AddBtnListener(MyEvent.MyEventType.TaskAbort);
            m_Btn.transform.Find("Text").GetComponent<Text>().text = "放弃";
            transform.GetComponent<Image>().sprite = LoadSprite.LoadSpriteByName("CommonUI/RestaurantAtlas", "RestaurantAtlas_1");
        }
        else if (M_ShowTask.M_TaskState == Task.TaskState.Finished)
        {
            m_Btn.enabled = false;
            m_Btn.transform.Find("Text").GetComponent<Text>().text = "已完成";
            transform.GetComponent<Image>().sprite = LoadSprite.LoadSpriteByName("CommonUI/RestaurantAtlas", "RestaurantAtlas_0");
        }
        else if (M_ShowTask.M_TaskState == Task.TaskState.Normal)
        {
            AddBtnListener(MyEvent.MyEventType.TaskAccept);
            m_Btn.transform.Find("Text").GetComponent<Text>().text = "接受";
            transform.GetComponent<Image>().sprite = LoadSprite.LoadSpriteByName("CommonUI/RestaurantAtlas", "RestaurantAtlas_3");
        }
        else if (M_ShowTask.M_TaskState == Task.TaskState.Reach)
        {
            //TODO
            AddBtnListener(MyEvent.MyEventType.TaskReach);
            m_Btn.transform.Find("Text").GetComponent<Text>().text = "完成";
            transform.GetComponent<Image>().sprite = LoadSprite.LoadSpriteByName("CommonUI/RestaurantAtlas", "RestaurantAtlas_3");
        }
        //m_TaskInfo.gameObject.SetActive(true);
        m_TaskTitle.text = M_ShowTask.M_TaskTitle;
        m_TaskContent.text = M_ShowTask.M_TaskContent;
        m_AwardEXP.text = M_ShowTask.M_AwardEXP == 0 ? "" : M_ShowTask.M_AwardEXP + "";
        m_AwardCoin.text = M_ShowTask.M_AwardCoin + "";
        foreach (Transform t in m_AwardItem) { if (t == null) return;  Destroy(t.gameObject); }
        //显示奖励物品
        for (int i = 0; i < M_ShowTask.M_AwardItemsID.Length; i++)
        {
            if (M_ShowTask.M_AwardItemsID[i] == 0) return;
            GameObject go = new GameObject("AwardItem");
            go.AddComponent<Image>().sprite = Resources.Load<Sprite>(Inventory.Instance.GetItemByID(M_ShowTask.M_AwardItemsID[i]).m_Sprite);
            go.transform.SetParent(m_AwardItem);
        }
    }





}
