using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 技能特效属性管理类
/// 自身的显示和隐藏逻辑
/// 伤害计算该由谁做？？？？TODO
/// </summary>
public class SkillEffect : MonoBehaviour {
    
    [HideInInspector]public SkillInfo m_Skill;//当前特效对应的技能信息
    [Tooltip("特效持续时间")]
    public float m_EffectTime = 5;//特效持续时间
    [Tooltip("矩形特效范围")]
    public Vector3 boxRange = Vector3.zero;//矩形特效范围
    [Tooltip("圆形特效范围")]
    public float radiusRange = 0;//圆形特效范围
    //public Vector3 m_Offset;//与玩家位置的偏移
    public float timer;
    public float m_Speed = 10;

    //public SkillEffect()
    //{

    //}

    private void Awake()
    {

        //StopAllCoroutines();
        ////StartCoroutine(Show());
        //StartCoroutine(AutoHide());
    }
    private void Start()
    {
        
    }

    //protected virtual IEnumerator Show()
    //{

    //    if (gameObject.activeSelf == false) gameObject.SetActive(true);
    //    yield return new WaitForSeconds(m_EffectTime);


    //    if (gameObject.activeSelf) gameObject.SetActive(false);
    //    yield return null;
    //} 

    /// <summary>
    /// 检测技能范围内的敌人
    /// </summary>
    protected virtual void  CheckSkillRangeEmeny()
    {
       
    }
    /// <summary>
    /// 由特效来设置技能命中敌人后 敌人的状态
    /// 得到技能效果的参数
    /// 0 伤害
    /// 1 击退距离
    /// 2 击飞高度
    /// 3 减速时间
    /// 4 静止时间
    /// </summary>
    /// <returns></returns>
    public ArrayList SkillEffectPara()
    {

        switch (m_Skill.M_EffectType)
        {
            case EffectType.Fly:
                return new ArrayList { 0, m_Skill.M_EffectApplyValue, 0, 0 };
            case EffectType.Back:
                return new ArrayList { m_Skill.M_EffectApplyValue, 0, 0, 0 };
            case EffectType.Slow:
                return new ArrayList { 0, 0, m_Skill.M_EffectApplyValue, 0 };
            case EffectType.Dizzy:
                return new ArrayList { 0, 0, 0, m_Skill.M_EffectApplyValue };
        }

        return null;
    }

    private void OnTriggerEnter(Collider c)
    {
        Debug.Log(c.name);
        if (c.tag == Tags.Enemy)
        {
            //伤害 命中效果
            Debug.Log("FuckYou");
        }
    }

    private void Update()
    {
        //if (!gameObject.activeInHierarchy)
        //{
        //    timer = 0;
        //    return;
        //}
        ////transform.Translate(transform.position * 5 * Time.deltaTime, Space.Self);
        //if (!gameObject.activeSelf) return;
        //transform.position += transform.forward * m_Speed * Time.deltaTime;
        //timer += Time.deltaTime;
        //if (timer >= 2)
        //{
        //    gameObject.SetActive(false);
        //    timer = 0;
        //}


    }

    IEnumerator AutoHide()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
        yield return null;
    }

}
