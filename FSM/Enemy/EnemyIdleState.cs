using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyIdleState : HumanState {

    private float distance;



    public EnemyIdleState()
    {
        stateID = StateID.Idle;
    }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        Debug.Log("EnteringEnemyIdle");
    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving(); 
    }

    public override IEnumerator DoLogic()
    {
        
        int i = 0;
        //随机新的方向
        Quaternion newDir = Quaternion.LookRotation(new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Vector3.up);
        //yield return new WaitForSeconds(2);
        while (!isExitState)
        {
            //distance = Vector3.Distance(((EnemyStateController)stateCtrl).PlayerAround().position, attr.transform.position);
            if (((EnemyStateController)stateCtrl).PlayerAround() != null)
            {
                fsm.PerformTransition(Transition.IsMove);
                break;
            }
            //Debug.Log(newDir + "    " + Quaternion.Angle(attr.transform.rotation, newDir));
            if (Quaternion.Angle(attr.transform.rotation, newDir) > 1f)
            {
                i = 0;
                attr.transform.rotation = Quaternion.Lerp(attr.transform.rotation,newDir,Time.deltaTime);
            }
            else
            {
                fsm.PerformTransition(Transition.IsMove);
                i = 1;
            }
            yield return new WaitForSeconds(i);

        }
        yield return null;
    }











}
