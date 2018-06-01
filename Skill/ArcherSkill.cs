using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// skillMachine暂时未完成  先使用旧方法 实现技能释放
/// 一个技能一个方法逻辑处理
/// </summary>
public class ArcherSkill : MonoBehaviour {

    private Dictionary<string, GameObject> m_EffectDict = new Dictionary<string, GameObject>();
    private AttrController m_Attr;
    private GameObject m_SkillEffect;


    private void Start()
    {

        m_Attr = AttrController.Instance;

    }

    /// <summary>
    /// 技能释放
    /// 
    /// </summary>
    /// <param name="skill"></param>
    public  IEnumerator Skill(SkillInfo skill)
    {
        Vector3 dir = m_Attr.transform.forward;
        //播放动画
        m_Attr.GetComponent<Animator>().SetTrigger("Skill" + skill.M_Id);
        yield return new WaitForSeconds(0.3f);
        //释放技能特效
        CreatEffect(skill, m_Attr.transform.position + new Vector3(0, 0.5f, 0), m_Attr.transform.rotation);


        //GameObject go = GameObject.CreatePrimitive(PrimitiveTypecv .Cube);
    /// <returns></returns>
        //go.transform.position = m_Attr.transform.position + new Vector3(0,0,1);
        yield return null;

    }



    void GetForwardEnemy()
    {

    }


    void GetRangeEnemy()
    {

    }

    void CreatEffect(SkillInfo skill,Vector3 pos,Quaternion dir)
    {
        m_SkillEffect = m_EffectDict.TryGetTool(skill.M_EffectName);
        if (m_SkillEffect == null)
        {
            m_SkillEffect = Instantiate(Resources.Load<GameObject>("SkillEffect/" + skill.M_EffectName));
            m_SkillEffect.transform.rotation = dir;
            m_SkillEffect.transform.position = pos;
            SkillEffect se = m_SkillEffect.AddComponent<SkillEffect>();
            se.m_Skill = skill;
            m_EffectDict.Add(skill.M_EffectName, m_SkillEffect);
            return;

        }
       
        m_SkillEffect.transform.position = pos;
        m_SkillEffect.transform.rotation = dir;
        m_SkillEffect.SetActive(true);
        Debug.Log(m_SkillEffect.transform.forward);

    }
    
}
