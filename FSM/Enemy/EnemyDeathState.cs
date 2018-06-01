using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : HumanState {

    public override void DoBeforeEntering()
    {
        
        base.DoBeforeEntering();
        Debug.Log("Enter DeathState");
    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
    }

    public override IEnumerator DoLogic()
    {
        yield return new WaitForSeconds(2);
        GameObject.Destroy(attr.gameObject);
    }


}
