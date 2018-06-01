using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// 事件类
/// </summary>
public class MyEvent
{
    public enum MyEventType
    {
        //1、玩家信息改动事件
        HP, MP, level, EXP, Strength, Agility, Intellect, Energy, Fighting, PlayerInfo,
        BaseAttack,
        BaseAttackEffect,
        SkillTrigger,//技能命中敌人的事件
        EventStart,
        SpawnParticle,
        SkillMissileDie,//特效完毕
        AnimationOver,//技能动画完毕
        TaskAccept,//接受任务
        TaskAbort,//放弃任务
        TaskReach,//达成任务
        TaskFinish,//完成任务

        Volume,
        AudioClip,//更换音乐

        MessageBox,//显示提示信息

    }

    public MyEventType m_MyEventType;//当前事件
    public Transform m_MissilePos;
    public int m_IntPara;
    public float m_FloatPara;
    public string m_StringPara;
    public Task m_Task;
    public GameObject m_GOPara;



    public MyEvent(MyEventType evtType)
    {
        m_MyEventType = evtType;
    }

}




/// <summary>
/// 委托
/// </summary>
/// <param name="myet"></param>
public delegate void m_MyDelegate(MyEvent myet);

/// <summary>
/// 事件管理类
/// </summary>
public class MyEventSystem  {

    public static MyEventSystem m_MyEventSystem = new MyEventSystem();

    public event m_MyDelegate m_MyDelegateEvent;//事件
    //管理MyEventType对应的委托集合的Dictionary 也就是所有监听者的集合
    public Dictionary<MyEvent.MyEventType, List<m_MyDelegate>> myListener = new Dictionary<MyEvent.MyEventType, List<m_MyDelegate>>();

    /// <summary>
    /// 注册事件 添加到myDeDic管理
    /// </summary>
    public void RegisterEvent(MyEvent.MyEventType eventType, m_MyDelegate callback) {
        List<m_MyDelegate> delegateList = null;
        if (myListener.TryGetValue(eventType, out delegateList))
        {
            delegateList.Add(callback);
        }
        else
        {
            delegateList = new List<m_MyDelegate>();
            delegateList.Add(callback);
            myListener.Add(eventType, delegateList);
            //Debug.Log("注册事件：" + eventType);
        }
    }

    /// <summary>
    /// 推送事件
    /// </summary>
    /// <param name="eventType"></param>
    public void PushEvent(MyEvent _event) {
        List<m_MyDelegate> l;
        //Debug.Log("推送事件：" + _event.m_MyEventType);
        if (myListener.TryGetValue(_event.m_MyEventType, out l))
        {
            for (int i = 0; i < l.Count; i++)
            {
                l[i](_event); 
            }
        }
    }

    /// <summary>
    /// 注销事件
    /// </summary>
    public void DropEvent(MyEvent.MyEventType eventType,m_MyDelegate callback) {
        List<m_MyDelegate> l;
        if (myListener.TryGetValue(eventType, out l))
        {
            l.Remove(callback);
            //Debug.Log("注销事件:" + eventType);
        }
    }
	
}
