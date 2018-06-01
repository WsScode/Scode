using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Tools/AB")]
/// <summary>
/// 技能的配置信息 
/// 在面板上配置技能信息
/// 放在每个技能模板上
/// </summary>
public class SkillDataConfiger : MonoBehaviour {

    [System.Serializable] 
    public class EventItem
    {
        [Header("技能模板配置")]
        [Tooltip("配置对象")]
        public GameObject m_Layout;

        [Tooltip("对应事件")]
        public MyEvent.MyEventType M_Evt;

        public int m_ChildSkillID = -1;

        [Tooltip("粒子特效是否加载玩家身上")]
        public bool m_AttachAttacker = false;

        [Tooltip("粒子特效加在目标位置")]
        public bool m_TargetPos = false;

        [Tooltip("粒子附着在目标上")]
        public bool m_AttachTarget = false;

        public int m_EvtID = 0;

        public SkillEffectData m_SkillEffectData;


    }

    public List<EventItem> m_EventItemList;//管理所有技能层
    


    [System.Serializable]
    public class SkillEffectData
    {
        public EffectType m_EffectType;

        [Tooltip("效果持续时间")]
        public float m_Duration = 0;

        [Tooltip("效果显示粒子")]
        public GameObject m_EffectParticle;

        [Tooltip("产生的效果是否只能叠加一次 比如连续冰冻技能 冰冻时间不会叠加")]
        public bool m_KeepOld = false;
        

    }

    


}






