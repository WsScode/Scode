using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FSMSystem
{
    public  List<FSMState> states;

    // The only way one can change the state of the FSM is by performing a transition
    // Don't change the CurrentState directly
    private StateID currentStateID;//当前状态ID
    public StateID CurrentStateID { get { return currentStateID; } }
    private FSMState currentState;//当前状态
    public FSMState CurrentState { get { return currentState; } }

    public AttrController attr;

    public FSMSystem()
    {
        states = new List<FSMState>();
    }

    /// <summary>
    /// 设置当前的状态
    /// </summary>
    public void SetCurrentState(FSMState state) {
        currentStateID = state.ID;
        currentState = state;
        state.DoBeforeEntering();//手动触发第一个状态
        attr.StartCoroutine(currentState.DoLogic());
    }

    /// <summary>
    /// 添加状态
    /// </summary>
    public void AddState(FSMState s, AIBase ctrl)
    {
        // Check for Null reference before deleting
        if (s == null)
        {
            Debug.LogError("FSM ERROR: Null reference is not allowed");
        }

        s.FSM = this;//给每个状态的 FSM 赋值

        s.M_CTRL = ctrl;//每个状态得到controller

        // First State inserted is also the Initial state,
        //   the state the machine is in when the simulation begins
        if (states.Count == 0)
        {
            states.Add(s);
            return;
        }

        // Add the state to the List if it's not inside it
        foreach (FSMState state in states)
        {
            if (state.ID == s.ID)
            {
                Debug.LogError("FSM ERROR: Impossible to add state " + s.ID.ToString() +
                               " because state has already been added");
                return;
            }
        }
        states.Add(s);
    }

    /// <summary>
    ///   删除状态
    /// </summary>
    public void DeleteState(StateID id)
    {
        // Check for NullState before deleting
        if (id == StateID.NullStateID)
        {
            Debug.LogError("FSM ERROR: NullStateID is not allowed for a real state");
            return;
        }

        // Search the List and delete the state if it's inside it
        foreach (FSMState state in states)
        {
            if (state.ID == id)
            {
                states.Remove(state);
                return;
            }
        }
        Debug.LogError("FSM ERROR: Impossible to delete state " + id.ToString() +
                       ". It was not on the list of states");
    }


    public void PerformTransition(Transition trans)
    {
        // Check for NullTransition before changing the current state
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("FSM ERROR: NullTransition is not allowed for a real transition");
            return;
        }

        // Check if the currentState has the transition passed as argument
        StateID id = currentState.GetOutputState(trans);
        
        if (id == StateID.NullStateID)
        {
            Debug.LogError("FSM ERROR: State " + currentStateID.ToString() + " does not have a target state " +
                           " for transition " + trans.ToString());
            return;
        }

        // Update the currentStateID and currentState		
        currentStateID = id;
        foreach (FSMState state in states)
        {
            if (state.ID == currentStateID)
            {
                // Do the post processing of the state before setting the new one
                currentState.DoBeforeLeaving();
                //attr.StopCoroutine(currentState.DoLogic());
                //currentState.RunLogic();
                currentState = state;
                // Reset the state to its desired condition before it can reason or act
                currentState.DoBeforeEntering();
                attr.StartCoroutine(currentState.DoLogic()); 
                break;
            }
        }

    } // PerformTransition()

    public void PerformState(FSMState state)
    {
        currentStateID = state.ID;
        foreach (FSMState _state in states)
        {
            if (_state.ID == currentStateID)
            {
                // Do the post processing of the state before setting the new one
                currentState.DoBeforeLeaving();
                //attr.StopCoroutine(currentState.DoLogic());
                //currentState.RunLogic();
                currentState = state;
                // Reset the state to its desired condition before it can reason or act
                currentState.DoBeforeEntering();
                attr.StartCoroutine(currentState.DoLogic());
                break;
            }
        }
    }


    public AttrController GetAttr()
    {
        return attr;
    }


} //class FSMSystem