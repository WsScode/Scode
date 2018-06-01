using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : HumanState {

    private Transform m_MoveTargetPos = null;


    public EnemyMoveState(){ stateID = StateID.Move; }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        Debug.Log("EnteringEnemyMoveState");

    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
    }


    public override IEnumerator DoLogic()
    {
        float f = 0; m_MoveTargetPos = ((EnemyStateController)stateCtrl).PlayerAround();
        while (!isExitState)
        {
            if (m_MoveTargetPos != null)
            {
                
                //朝目标（玩家）运动
                //Debug.Log("朝向玩家移动");
                Quaternion newDir = Quaternion.LookRotation(m_MoveTargetPos.position - attr.transform.position,Vector3.up);
                attr.transform.rotation = Quaternion.Lerp(attr.transform.rotation, newDir, Time.deltaTime * 5);
                if (Vector3.Distance(m_MoveTargetPos.position, attr.transform.position) > ((EnemyAttr)attr).AttackDistance)
                {
                    attr.transform.Translate(Vector3.forward * Time.deltaTime * 2, Space.Self);
                    m_animator.SetFloat("MoveSpeed", 1);
                }
                else
                {
                    fsm.PerformTransition(Transition.IsFight);
                    break;
                }

            }
            else
            {
                //当前朝向方向运动
                if (f > 3 || CheckWall()) { f = 0; fsm.PerformTransition(Transition.IsIdle); break; }
                //Debug.Log("自由移动");
                attr.transform.Translate(Vector3.forward * Time.deltaTime * 2, Space.Self);
                m_animator.SetFloat("MoveSpeed", 1);
                f += Time.deltaTime;
            }

            m_animator.SetFloat("MoveSpeed",0);
            yield return new WaitForSeconds(0);
        }
       




        yield return null;
    }


    /// <summary>
    /// 是否移动到墙边
    /// </summary>
    public bool CheckWall()
    {
        RaycastHit hitInfo;
        bool isColl = Physics.Raycast(attr.transform.position, attr.transform.forward, out hitInfo, 1);
        if (isColl && hitInfo.collider.tag == Tags.Wall)
        {
            return true;
        }
        return false;
    }
}
