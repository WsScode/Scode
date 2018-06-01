using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : BasePanel {

    private CanvasGroup canvasgroup;

    private Image hpbar;
    private Text hpbarText;
    private Image mpbar;
    private Text mpbarText;
    

    public override void Awake()
    {
        base.Awake();
        showUIType = ShowUIType.Normal;
        hpbar = transform.Find("PlayerInfoSimplePanel/HPBar").GetComponent<Image>();
        hpbarText = transform.Find("PlayerInfoSimplePanel/HPBarText").GetComponent<Text>();
        mpbar = transform.Find("PlayerInfoSimplePanel/MPBar").GetComponent<Image>();
        mpbarText = transform.Find("PlayerInfoSimplePanel/MPBarText").GetComponent<Text>();
    }

    private void Start()
    {
        MyEventSystem.m_MyEventSystem.RegisterEvent(MyEvent.MyEventType.HP, OnEvent);
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

    public override void OnResume()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }


    protected override void OnEvent(MyEvent _event) {
        if (_event.m_MyEventType == MyEvent.MyEventType.HP || _event.m_MyEventType == MyEvent.MyEventType.MP) {
            SetUITest();
        }
    }


    public void SetUITest() {
        hpbarText.text = "31263/41654";
        mpbarText.text = "1165/66546";
    }
    /// <summary>
    /// 所有UI开关的方法  这个对开关的命名必须和 json文件中的uipaneltype保持一致
    /// </summary>
    //public void OnUISwitch( string uiPanelType) {
    //    foreach (Transform child in transform) {
    //        if (!transform.gameObject.name.Contains("Button")) continue;
    //        //child.gameObject.AddComponent<>();
    //    }
    //    UIPanelType type = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), uiPanelType);
    //    UIController._instance.PushPanel(type);
    //}
}
