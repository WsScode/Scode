using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 技能层逻辑管理类
/// 管理特效 实例化的特效保存在玩家模型下
/// </summary>
public class SkillManager : MonoBehaviour {

    public Dictionary<int, SkillEffect> m_SkillEffectDic;//保存实例化过的特效
    private SkillEffect m_SkillEffect;

    private Transform m_PlayerEffect;//模型父物体
    private SkillInfo m_ActiveSkill;

    public bool m_ISkill = false;//是否在释放技能中

    
    



    private void Start()
    {
        m_PlayerEffect = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Transform>().Find("EffectParent");
        //m_PlayerEffect = GameObject.Find("EffectParent").transform;
        SetSkillToPos();
    }


    /// <summary>
    /// 设置当前要触发的技能
    /// </summary>
    /// <param name="skill"></param>
    public void SetActiveSkill(SkillInfo skill)
    {
        //print(skill.M_Name);
        //print(m_ActiveSkill);
        if (skill == null || skill.M_SkillType != SkillType.Initiative || m_ActiveSkill != null) return;
        AttrController.Instance.GetComponent<PlayerStateController>().SetNextState(StateID.Cast_Skill);
        m_ActiveSkill = skill;
        print(m_ActiveSkill.M_Name);
    }

 


    /// <summary>
    /// 获取当前触发的技能
    /// </summary>
    public SkillInfo GetActiveSkill()
    {
        return m_ActiveSkill;
    }
    /// <summary>
    /// 将触发技能设置为空
    /// </summary>
    public void RestActiveSkill()
    {
        m_ActiveSkill = null;
    }

    /// <summary>
    /// 老方法 
    /// skillmmachine实现后替代
    /// </summary>
    void SkillStart()
    {
        SkillInfo skill = GetActiveSkill();

        AttrController.Instance.GetComponent<ArcherSkill>().StartCoroutine("Skill" + skill.M_Id);
    }

    /// <summary>
    /// 如果学习了技能是系统自动放置默认位置 还是可以自己调节位置？？？
    /// 设置技能到技能键上
    /// </summary>
    public void SetSkillToPos()
    {
        
        GameObject.FindGameObjectWithTag(Tags.UIParent)
            .transform.Find("FightButtonPanel(Clone)")
            .GetComponent<FightButtonPanel>()
            .SetSkillButton(GetComponent<SkillsInfo>().GetSkillByID(1));
    }


 



    /// <summary>
    /// 加载技能特效 只有主动技能才有特效
    /// 第一次加载的就保存到词典 然后实例化 播放完毕后就隐藏  后面触发只需要设置active
    /// </summary>
    /// <param name="skill"></param>
    public void LoadSkillEffect(SkillInfo skill, Vector3 offset)
    { 
        if (m_SkillEffectDic == null)
        {
            m_SkillEffectDic = new Dictionary<int, SkillEffect>();
        }
        if (skill.M_SkillType != SkillType.Initiative) return;
        if (m_SkillEffectDic.TryGetTool(skill.M_Id) != null)
        {
            print("第二次触发");
            //不是第一次触发次特效
            m_SkillEffect = m_SkillEffectDic.TryGetTool(skill.M_Id);
            m_SkillEffect.m_Skill = skill;
            if (m_SkillEffect.gameObject.activeSelf == false) m_SkillEffect.gameObject.SetActive(true);
            m_SkillEffect.transform.localPosition = offset;
            m_SkillEffect.StartCoroutine("Show");

        }
        else
        {
            //第一次触发次特效
            //print(skill.M_EffectName + ":" + m_PlayerEffect);
            //加载技能特效Prefab
            m_SkillEffect = Instantiate(Resources.Load<GameObject>("SkillEffect/" + skill.M_EffectName), m_PlayerEffect).GetComponent<SkillEffect>();
            m_SkillEffect.transform.localPosition = offset;
            m_SkillEffect.m_Skill = skill;
            m_SkillEffect.StartCoroutine("Show");

            if (!m_SkillEffectDic.ContainsValue(m_SkillEffect))
            {
                m_SkillEffectDic.Add(skill.M_Id, m_SkillEffect);
            }

        }



    }

}



