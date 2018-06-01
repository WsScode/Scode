using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 技能层运行逻辑
/// </summary>
public class SkillLayoutRunner : MonoBehaviour {


    public SkillMachine m_SkillMachine { get; set; }

    public SkillDataConfiger.EventItem m_EventItem { get; set; }

    public MyEvent m_MyEvent;

    private Dictionary<string, GameObject> m_GODic;





    /// <summary>
    /// 显示粒子
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowPartical()
    {
        if (m_GODic == null) m_GODic = new Dictionary<string, GameObject>();
        //加载技能层配置SkillLayoutConfiger
        SkillLayoutConfiger slc = m_EventItem.m_Layout.GetComponent<SkillLayoutConfiger>();
        GameObject particle = slc.m_Particle;
        if (particle != null)
        {
            if (slc.m_DelayTime > 0)
            {
                yield return new WaitForSeconds(slc.m_DelayTime);
            }

            GameObject particalgo = null ;
           
            if (m_GODic.TryGetTool(particle.name) != null)
            {
                particalgo = m_GODic.TryGetTool(particle.name).gameObject;
                particalgo.SetActive(true);
            }
            else
            {
                particalgo = Instantiate(particle);//实例化粒子 
                m_GODic.Add(particalgo.name, particalgo);
            }
            SkillEffect se = particalgo.GetComponent<SkillEffect>();

            se.enabled = false;
            //固定骨骼位置（玩家自身）
            if (slc.m_BoneName != "")
            {
                //TODO
                if (slc.m_BoneName == "body")//表示玩家整体方向
                {
                    particalgo.transform.position = m_SkillMachine.m_Attacker.transform.position + slc.m_Position;
                }
            }
            else
            {

                if (m_EventItem.m_TargetPos)
                {
                    if (m_SkillMachine.m_Target != null)//技能目标放在skillmachine里面 (这个是技能一开始就明确目标 如果是指向性技能可能在触碰到敌人时才能明确目标)
                    {
                        particalgo.transform.position = m_SkillMachine.m_Target.transform.position + slc.m_Position;
                        particalgo.transform.localRotation = Quaternion.identity;
                        particalgo.transform.localScale = Vector3.one;
                    }
                    else if(m_MyEvent.m_GOPara != null)
                    {
                        particalgo.transform.position = m_MyEvent.m_GOPara.transform.position + slc.m_Position;
                        particalgo.transform.localRotation = Quaternion.identity;
                        particalgo.transform.localScale = Vector3.one;
                    }
                }
                else if (m_EventItem.m_AttachTarget)//粒子附着在目标上 如冰冻等效果
                {
                    if (m_SkillMachine.m_Target != null)
                    {
                        particalgo.transform.parent = m_SkillMachine.m_Target.transform;
                        particalgo.transform.position = m_SkillMachine.m_Target.transform.position + slc.m_Position;
                        particalgo.transform.localRotation = Quaternion.identity;
                        particalgo.transform.localScale = Vector3.one;
                    }
                }
                else
                {
                    particalgo.transform.parent = transform;
                    particalgo.transform.localPosition = slc.m_Position;
                    particalgo.transform.localRotation = Quaternion.identity;
                    particalgo.transform.localScale = Vector3.one;
                }
            }
            se.enabled = true;
            yield return null;
        }
    }


    private void Start()
    {
        if (m_EventItem.m_Layout.GetComponent<SkillLayoutConfiger>() != null)
        {
            StartCoroutine(ShowPartical());
        }
    }

}
