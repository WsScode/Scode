using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SystemPanel : BasePanel {

    private Slider m_AudioSlider;//声音开关
    private float m_CurrentVolue;
    private float m_Volue;
    MyEvent evt;


    public override void Awake()
    {
        base.Awake();
        showUIType = ShowUIType.Normal;//TODO 应该为ShowUIType.PopMethod
        m_AudioSlider = transform.Find("AudioSwitch").GetComponent<Slider>();
        evt = new MyEvent(MyEvent.MyEventType.Volume);
        //注册事件
        m_AudioSlider.onValueChanged.AddListener(delegate {
            
            evt.m_FloatPara = m_AudioSlider.value;
            MyEventSystem.m_MyEventSystem.PushEvent(evt);
        });
        


        m_Volue = m_CurrentVolue = m_AudioSlider.value;
       
    }

    private void Start()
    {
        evt.m_FloatPara = m_AudioSlider.value;
        MyEventSystem.m_MyEventSystem.PushEvent(evt);
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

    protected override void OnEvent(MyEvent eventType)
    {
        
    }



    private void Update()
    {
        
    }


}
