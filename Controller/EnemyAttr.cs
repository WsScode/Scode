using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttr : AttrController {


    private EnemyStateController m_ESCtrl;

    private Enemy m_EnemyInfo;


    private void Awake()
    {
        m_ESCtrl = gameObject.AddComponent<EnemyStateController>();
        m_ESCtrl.attr = this;

        m_EnemyInfo = GetComponent<Enemy>();
    }

    public override int HP{ get { return m_EnemyInfo.m_hp; } }
    public float AttackDistance { get { return m_EnemyInfo.m_AttackDistance; } set { m_EnemyInfo.m_AttackDistance = value; } }
    public float AttackRate { get { return m_EnemyInfo.m_AttackRate; } }
    public float TraceDistance { get { return m_EnemyInfo.m_TraceDistance; } }

    public void GetText() { }




}
