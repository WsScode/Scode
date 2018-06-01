using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 所有敌人的基类
/// </summary>
public class Enemy : MonoBehaviour {

    [HideInInspector]public Animator m_animator;


    public float m_AttackDistance;
    public int m_hp;
    public float m_AttackRate;
    public float m_TraceDistance;

   



    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            m_animator.SetTrigger("Run");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_animator.SetTrigger("Walk");
        }
    }

    /// <summary>
    /// 被击
    /// 得到技能效果的参数
    /// 0 伤害
    /// 1 击退距离
    /// 2 击飞高度
    /// 3 减速时间
    /// 4 静止时间
    /// </summary>
    /// <returns></returns>
    public virtual void DoDamage(ArrayList para)
    {
        float damage = float.Parse(para[0]+"");
        float backDis = float.Parse(para[1] + "");
        float flyDis = float.Parse(para[2] + "");
        float slowtime = float.Parse(para[3] + "");
        float dizztime = float.Parse(para[4] + "");

        if (m_hp <= damage) { m_hp = 0; Death();return; }
        m_hp -= (int)damage;
        
    }

    /// <summary>
    /// 死亡的逻辑
    /// </summary>
    void Death()
    {
        transform.GetComponent<CapsuleCollider>().enabled = false;
        m_animator.SetTrigger("Death");
        print("Enemy Death");
    }
}
