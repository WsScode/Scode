using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;


/// <summary>
/// 玩家 人形NPC的状态基类
/// </summary>
public class HumanState : FSMState {
    protected GameObject m_Player;

    protected bool m_IsMove;



    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        attr = fsm.GetAttr();
        if (attr == null) return;
     
        m_animator = attr.GetComponent<Animator>();

    }

    public void SetIdle() {
        m_animator = attr.GetComponent<Animator>();
        m_animator.SetBool("Fight", false);
        m_animator.SetFloat("MoveSpeed", 0);
        fsm.PerformTransition(Transition.IsIdle);
    }

    public void SetMoveState()
    {
    }

    /// <summary>
    /// 受伤
    /// </summary>
    public virtual IEnumerator DoDamage()
    {
        yield return null;
    }

   
    /// <summary>
    /// 得到当前播放的动画
    /// </summary>
    /// <returns></returns>
    protected AnimatorClipInfo GetCurrentAnim()
    {
        return m_animator.GetCurrentAnimatorClipInfo(0)[0];
    }


    protected void SetAnimationEvent(AnimatorClipInfo clip, float length)
    {
       
    }


    protected void Jump()
    {

    }



}
