using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 初始化敌人的状态机
/// </summary>
public class EnemyStateController : AIBase {
    private EnemyIdleState idleState;
    private EnemyMoveState moveState;
    private EnemyFightState fightState;
    private EnemyDeathState deathState;
    

    private void Start()
    { 
        InitStateMachine();
    }



    protected override void InitStateMachine()
    {
        m_FSM = new FSMSystem();
        m_FSM.attr = attr;

        idleState = new EnemyIdleState();
        moveState = new EnemyMoveState();
        fightState = new EnemyFightState();
        deathState = new EnemyDeathState();


       

        m_FSM.AddState(idleState, this);
        idleState.AddTransition(Transition.IsMove, StateID.Move);
        idleState.AddTransition(Transition.IsFight, StateID.Fight);


        m_FSM.AddState(moveState,this);
        moveState.AddTransition(Transition.IsIdle,StateID.Idle);
        moveState.AddTransition(Transition.IsFight,StateID.Fight);


        m_FSM.AddState(fightState,this);
        fightState.AddTransition(Transition.IsIdle,StateID.Idle);

        m_FSM.AddState(deathState,this);

        //初始状态为Idle
        m_FSM.SetCurrentState(idleState);

    }

    /// <summary>
    /// 在idle状态会左右旋转巡视
    /// </summary>
    public void IdleToMove()
    {

    }


    private void Update()
    {
        if (CheckIsDie())
        {
            m_FSM.SetCurrentState(deathState);
        }   
    }


    /// <summary>
    ///玩家是否进入范围
    /// </summary>
    /// <returns></returns>
    public Transform PlayerAround()
    {
        
        Collider[] cs = Physics.OverlapSphere(attr.transform.position, ((EnemyAttr)attr).TraceDistance);
        foreach (Collider c in cs)
        {
            if (c.gameObject.tag == Tags.Player) return c.transform;
        }
        return null;
    }


    //public void CheckPlayer()
    //{
    //    Collider[] cs = Physics.OverlapSphere(attr.transform.position, ((EnemyAttr)attr).TraceDistance);


    //    foreach (Collider c in cs)
    //    {
    //        if (c.gameObject.tag == Tags.Player)
    //        {

    //        }
    //    }


    //}





    private bool CheckIsDie()
    {
        if (attr.HP <= 0) return true;
        return false;
    }

    public override void DoDamage()
    {
        
    }

}



