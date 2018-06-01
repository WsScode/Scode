using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 主动技能 冰霜之箭
/// </summary>
public class SkillBSZJ : SkillEffect
{
    private void Start()
    {
        m_EffectTime = 3;
    }

    protected override void CheckSkillRangeEmeny()
    {
        
        Collider[] cols = Physics.OverlapBox(transform.position, new Vector3(2, 1, 1), Quaternion.identity);
        foreach (Collider c in cols)
        {
            if (c.tag == Tags.Enemy)
            {
                c.gameObject.GetComponent<Enemy>().DoDamage(SkillEffectPara());//TODO 被技能击中的一些效果 用数组传递
            }
        }
    }

    //protected override IEnumerator Show()
    //{
    //    m_EffectTime = 3;
    //    return base.Show();
    //}


    private void Update()
    {
       
        
    }
}
