using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Transition
{
    NullTransition = 0, // Use this transition to represent a non-existing transition in your system
  IsIdle,
  IsMove,
  IsRiding,
  IsChatStart,
  IsChatEnd,
  IsFight,
  IsCast_Skill,
  IsVigilance,
  IsDeath,

}


public enum StateID
{

    NullStateID = 0, // 默认状态 无状态  Use this ID to represent a non-existing State in your system	
    Idle,//共有
    Move,//共有
    Riding,
    ChatStart, //和NPC的对话状态
    ChatEnd,
    Fight,//共有
    Cast_Skill,
    Vigilance,//共有
    Death,//共有
}

/// <summary>
/// 状态类
/// </summary>
public abstract class FSMState
{
    protected FSMSystem fsm;//保存状态控制器  便于调用里面的方法进行切换等 操作
    public FSMSystem FSM { set { fsm = value; } }
    protected AIBase stateCtrl;
    public AIBase M_CTRL { set { stateCtrl = value; } }

    protected Dictionary<Transition, StateID> stateMap = new Dictionary<Transition, StateID>();
    protected StateID stateID;
    public StateID ID { get { return stateID; } }

    protected Animator m_animator;

    protected bool isExitState;

    public ETCJoystick m_MyJoystick;

    protected bool isPlayAnim = false;//当前是否在播放动画

    public AttrController attr { get; set; }

    public Transition m_Transition { get; private set; }



    public void AddTransition(Transition trans, StateID id)
    {
        // Check if anyone of the args is invalid
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed for a real transition");
            return;
        }

        if (id == StateID.NullStateID)
        {
            Debug.LogError("FSMState ERROR: NullStateID is not allowed for a real ID");
            return;
        }

        // Since this is a Deterministic FSM,
        //   check if the current transition was already inside the stateMap
        if (stateMap.ContainsKey(trans))
        {
            Debug.LogError("FSMState ERROR: State " + stateID.ToString() + " already has transition " + trans.ToString() +
                           "Impossible to assign to another state");
            return;
        }
        
        stateMap.Add(trans, id);
        //Debug.Log(trans);
    }


    public void DeleteTransition(Transition trans)
    {
        // Check for NullTransition
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return;
        }

        // Check if the pair is inside the stateMap before deleting
        if (stateMap.ContainsKey(trans))
        {
            stateMap.Remove(trans);
            return;
        }
        Debug.LogError("FSMState ERROR: Transition " + trans.ToString() + " passed to " + stateID.ToString() +
                       " was not on the state's transition list");
    }


    public StateID GetOutputState(Transition trans)
    {
        // Check if the stateMap has this transition
        if (stateMap.ContainsKey(trans))
        {
            return stateMap[trans];
        }
        return StateID.NullStateID;
    }

    /// <summary>
    /// 是否能切换到下一个状态
    /// </summary>
    /// <returns></returns>
    protected bool CanNextState(Transition nextTransition)
    {
        //if (nextState != this && stateMap.ContainsValue(nextState.ID))
        //{
        //    return true;
        //}
        if (!stateMap.ContainsKey(nextTransition) || stateMap[nextTransition] != this.stateID)
        {
            return true;
        }
        return false;
    }


    public virtual void DoBeforeEntering() {isExitState = false;}


    public virtual void DoBeforeLeaving() { isExitState = true; }


    public virtual void Reason(GameObject player, GameObject npc) { }


    public virtual void Act(GameObject player, GameObject npc) { }


    public virtual void RunLogic() {
       
    }

    /// <summary>
    /// 执行当前状态逻辑的方法
    /// </summary>
    /// <returns></returns>
    public  virtual IEnumerator DoLogic() {
        while (!isExitState) {

        }
        return null;
    }

    
    public virtual void DoEvent(MyEvent evt) {

    }







} // class FSMState
