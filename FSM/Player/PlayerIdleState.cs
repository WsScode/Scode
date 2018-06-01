using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerIdleState : HumanState
{

    public PlayerIdleState() {
        stateID = StateID.Idle;
    }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        
        Debug.Log( " Enter IdleState");
    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
        fsm.attr.StopCoroutine("DoLogic");
    }

    

    public override IEnumerator DoLogic()
    {
        while (!isExitState)
        {

            if (Mathf.Abs(attr.myJoystick.axisX.axisValue) > 0.1f || Mathf.Abs(attr.myJoystick.axisY.axisValue) > 0.1f )
            {
                //m_MyJoystick.tmSpeed = 300;
                //m_animator.SetBool("Sit", false);
                fsm.PerformTransition(Transition.IsMove);//切换到移动状态
                break;
            }

            if (attr.Fight()) { fsm.PerformTransition(Transition.IsFight); break; }
            //if (playerctrl.Fight())
            //{
            //    yield return new WaitForSeconds(0);
            //    m_animator.SetBool("Fight", true);
            //    fsm.PerformTransition(Transition.IsFight);
            //    break;
            //}
            //else if (playerctrl.OnSkillAttack() > 0)
            //{
            //    yield return new WaitForSeconds(0);
            //    m_animator.SetBool("Fight", true);
            //    fsm.PerformTransition(Transition.IsCast_Skill);
            //    break;
            //}

            yield return null;
        }
        
    }

    public override void RunLogic()
    {

        //while (!isExitState)
        //{

        //    //if (Mathf.Abs(attr.myJoystick.axisX.axisValue) > 0.1f || Mathf.Abs(attr.myJoystick.axisY.axisValue) > 0.1f)
        //    //{
        //    //    //m_MyJoystick.tmSpeed = 300;
        //    //    //m_animator.SetBool("Sit", false);
        //    //    fsm.PerformTransition(Transition.IsMove);//切换到移动状态
        //    //    break;
        //    //}
        //    attr.StartCoroutine("IE");
        //}

    }

  

    /// <summary>
    ///判断  当前周围有敌人 或者是都在地图里面 TODO
    /// </summary>
    /// <returns></returns>
    //public bool IsFight()
    //{
    //    //if (SceneManager.GetActiveScene().buildIndex > 1 || ) { }
    //    Collider[] cols = Physics.OverlapSphere(m_Player.transform.position, 5);
    //    if (cols.Length > 0)
    //    {
    //        foreach (Collider c in cols)
    //        {
    //            if (c.tag == "Enemy")
    //            {
    //                Debug.Log("Enemy");
    //                return true;
    //            }
    //        }
    //    }
    //    return false;

    //}



}
