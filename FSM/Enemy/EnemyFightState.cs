using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightState : HumanState {

    private float m_Distance;

    public EnemyFightState() { stateID = StateID.Fight; }


    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        m_Distance = Vector3.Distance(attr.transform.position,((EnemyStateController) stateCtrl).PlayerAround().position);

    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
      
    }


    public override IEnumerator DoLogic()
    {
        float timer = ((EnemyAttr)attr).AttackRate;
        while (!isExitState)
        {
            if (m_Distance <= ((EnemyAttr)attr).AttackDistance && timer >= ((EnemyAttr)attr).AttackRate)
            {
                m_animator.SetInteger("AttackNum",2);
                //AnimationController.GetAnimationLenth(m_animator, "Attack" + 1);
                attr.StartCoroutine(DoDamage());

                timer = 0;
                yield return new WaitForSeconds(AnimationController.GetAnimationLenth(m_animator, "Attack" + 1));
                //Debug.Log(Vector3.Distance(attr.transform.position, ((EnemyStateController)stateCtrl).PlayerAround().position));
                if (Vector3.Distance(attr.transform.position, ((EnemyStateController)stateCtrl).PlayerAround().position) > ((EnemyAttr)attr).AttackDistance)
                {
                    fsm.PerformTransition(Transition.IsIdle);/*Debug.Log("超出范围");*/
                    m_animator.SetInteger("AttackNum", 0);
                    break;
                }
                m_animator.SetInteger("AttackNum", 0);
            }
            timer += Time.deltaTime;
            yield return new WaitForSeconds(0);
        }
        yield return null;
    }

    public override IEnumerator DoDamage()
    {
        yield return new WaitForSeconds(AnimationController.GetAnimationLenth(m_animator, "Attack" + 1) * 0.6f);
        //Debug.Log("击打");
    }



}
