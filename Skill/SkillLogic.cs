using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 技能逻辑部分
/// </summary>
public class SkillLogic  {

    //skill模板以及后续的技能运行层在退出地图时再销毁
    private static Dictionary<int, GameObject> m_SkillGoCache;








    /// <summary>
    /// 创建技能状态机
    /// </summary>
    /// <param name="activeSkill">技能机对应技能</param>
    /// <param name="pos">技能机初始位置</param>
    /// <param name="attacker">技能施放者</param>
    /// <param name="enemy">敌人信息 默认为null(非指定性技能)</param>
    public static SkillMachine CreateSkillMechine(SkillInfo activeSkill,Vector3 pos,GameObject attacker,GameObject enemy = null)
    {
        GameObject go = new GameObject("SkillMachine" + activeSkill.M_Name);
        SkillMachine sm = go.AddComponent<SkillMachine>();
        sm.m_SkillInfo = activeSkill;
        sm.m_SkillDataConfiger = GetSkillInfo(activeSkill);
        sm.m_InitPos = pos;
        sm.m_Attacker = attacker;
        sm.m_Target = enemy;
        //go.transform.parent = GameManager.GM.gameObject.transform;//放到GameController组件物体下
        go.transform.localPosition = Vector3.zero;
        return sm;
    }

    /// <summary>
    /// 实例化技能模板 并返回技能配置信息
    /// </summary>
    public static SkillDataConfiger GetSkillInfo(SkillInfo activeSkill)
    {
        if (activeSkill == null) return null;
        if (m_SkillGoCache == null) { m_SkillGoCache = new Dictionary<int, GameObject>(); }
        if (!m_SkillGoCache.ContainsKey(activeSkill.M_Id) || m_SkillGoCache[activeSkill.M_Id] == null)
        {
            //Debug.Log(activeSkill.M_SkillDataConfiger);
            //Debug.Log(activeSkill.M_EffectName);//改为模板配置名称
            GameObject g = GameObject.Instantiate(Resources.Load<GameObject>(activeSkill.M_SkillDataConfiger) );
            GameObject.DontDestroyOnLoad(g);
            m_SkillGoCache.Add(activeSkill.M_Id,g);
        }
        return m_SkillGoCache[activeSkill.M_Id].GetComponent<SkillDataConfiger>();

    }

}
