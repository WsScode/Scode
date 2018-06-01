using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


/// <summary>
/// 暂时没想好 好的处理方式 （脱离于UIController控制）就用事件消息触发
/// </summary>
public class MessageBoxPanel : BasePanel {

    private bool m_IsShow = false;

    private Text messageText;
    MyEvent evt;//注册的事件
    private Animation m_Animation;


    public override void Awake()
    {
        base.Awake();
        showUIType = ShowUIType.Normal;
        messageText = GetComponent<Text>();
        evt = new MyEvent(MyEvent.MyEventType.MessageBox);
        MyEventSystem.m_MyEventSystem.RegisterEvent(evt.m_MyEventType,OnEvent);
        m_Animation = GetComponent<Animation>();
    }

    public override void OnEnter(UIController uIController)
    {
        base.OnEnter(uIController);
        canvasGroup.alpha = 1;
    }

    public override void OnPause()
    {
        canvasGroup.alpha = 0;
    }

    public override void OnResume()
    {
        canvasGroup.alpha = 1;
    }

    protected override IEnumerator OnLogic()
    {

        canvasGroup.alpha = 1;
        yield return new WaitForSeconds(2f);
        transform.DOLocalMoveY(350,1);
        //GetComponent<Renderer>().material.DOFade(0,1);

        yield return new WaitForSeconds(0);
    }


   protected override void  OnEvent(MyEvent evt)
    {
        messageText.text = evt.m_StringPara;
        StartCoroutine(OnLogic());
    }


  

}
