using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMachine : MonoBehaviour {

    public GameObject m_Attacker { get; set; }//技能施放者

    public GameObject m_Target { get; set; }//技能目标

    public SkillInfo m_SkillInfo;//技能信息

    public List<GameObject> AllRunner = new List<GameObject>();//所有的技能运行层

    public bool m_Stop = false;//状态机停止标志

    public SkillDataConfiger m_SkillDataConfiger { get; set; }//技能层设置

    public int m_LifeTime = 50;//技能生命周期 默认为5秒

    public Vector3 m_InitPos;//初始位置






    //事件
    public static List<MyEvent.MyEventType> m_EvtType = new List<MyEvent.MyEventType>()
    {
        MyEvent.MyEventType.SkillTrigger,MyEvent.MyEventType.SkillMissileDie,MyEvent.MyEventType.AnimationOver,
    };


    void RegisterEvent()
    {
        foreach (MyEvent.MyEventType et in m_EvtType)
        {
            MyEventSystem.m_MyEventSystem.RegisterEvent(et, OnEvent);
        }
    }

    void DropEvent()
    {
        foreach (MyEvent.MyEventType et in m_EvtType)
        {
            MyEventSystem.m_MyEventSystem.DropEvent(et, OnEvent);
        }
    }

    void DropOneEvent(MyEvent.MyEventType et)
    {
        MyEventSystem.m_MyEventSystem.DropEvent(et, OnEvent);
    }




    public void OnEvent(MyEvent evt)
    {

        switch (evt.m_MyEventType)
        {
            case MyEvent.MyEventType.SkillTrigger://技能命中
                OnHit(evt);
                break;
            case MyEvent.MyEventType.SkillMissileDie://粒子释放
                OnMissileDie(evt);
                break;
            case MyEvent.MyEventType.AnimationOver://人物动画结束
                OnAnimationOver();
                break;
        }
    }

    /// <summary>
    /// 技能命中
    /// </summary>
    void OnHit(MyEvent evt)
    {
        if (!m_Stop)
        {
            if (m_SkillDataConfiger != null)
            {
                foreach (SkillDataConfiger.EventItem item in m_SkillDataConfiger.m_EventItemList)
                {
                    if (item.M_Evt == MyEvent.MyEventType.SkillTrigger)
                    {
                        InitLayout(item, evt);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 技能子弹命中
    /// 命中后取消命中判定
    /// </summary>
    /// <param name="evt"></param>
    void OnMissileDie(MyEvent evt)
    {
        foreach (SkillDataConfiger.EventItem item in m_SkillDataConfiger.m_EventItemList)
        {
            if (item.M_Evt == MyEvent.MyEventType.SkillMissileDie)
            {
                InitLayout(item, evt);
            }
        }
        DropOneEvent(MyEvent.MyEventType.SkillMissileDie);
    }

    /// <summary>
    /// 动画结束 取消事件处理
    /// </summary>
    void OnAnimationOver()
    {
        DropOneEvent(MyEvent.MyEventType.SkillTrigger);
    }


    /// <summary>
    /// 技能层的初始化
    /// 或者生成子技能
    /// </summary>
    void InitLayout(SkillDataConfiger.EventItem item, MyEvent evt)
    {
        if (item.m_Layout != null)
        {
            
            GameObject go = Instantiate(item.m_Layout);//实例化模板
            SkillLayoutRunner runner = go.AddComponent<SkillLayoutRunner>();//添加SkillLayoutRunner
            runner.m_SkillMachine = this;
            runner.m_EventItem = item;
            runner.m_MyEvent = evt;
            AllRunner.Add(go);Debug.Log(go.name);
        }
        else if (item.m_ChildSkillID != 0 && item.m_ChildSkillID != -1)
        {
            SkillLogic.CreateSkillMechine(SkillsInfo.Instance.GetSkillByID(item.m_ChildSkillID), evt.m_MissilePos.position, m_Attacker);
        }
    }


    /// <summary>
    /// 触发EventStart事件
    /// </summary>
    void OnStart()
    {
        if (m_SkillDataConfiger != null)
        {

            foreach (SkillDataConfiger.EventItem item in m_SkillDataConfiger.m_EventItemList)
            {
                if (item.M_Evt == MyEvent.MyEventType.EventStart)
                {
                    InitLayout(item, null);
                }
            }
        }
    }

    /// <summary>
    /// 是否需要销毁？？？
    /// </summary>
    /// <returns></returns>
    IEnumerator ClearSkill()
    {
        yield return new WaitForSeconds(m_LifeTime);
        foreach (GameObject g in AllRunner)
        {
            Destroy(g);
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// 开始
    /// </summary>
    void Awake()
    {
        RegisterEvent();
        
    }

    private void Start()
    {
        StartCoroutine(ClearSkill());
        OnStart();
    }

    public void Stop()
    {
        m_Stop = true;
    }

}
