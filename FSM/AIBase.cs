using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 挂载在物体上的 管理行为状态 的脚本 
/// </summary>
public class AIBase : MonoBehaviour {

    public bool IsFight { get; private set; }

    public AttrController attr;

    public FSMSystem m_FSM;

    protected virtual void InitStateMachine() { }

    public virtual FSMState GetNextState() { return null; }

    public virtual void ResetNextState() { }

    public virtual void SetNextState(StateID stateID, Transition transition = Transition.IsIdle) { }

    public virtual void DoDamage() { }
}
