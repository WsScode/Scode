using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation  {

    public Animator m_Animator;
    public float m_EventTime;//事件触发点


    public PlayerAnimation(Animator anim)
    {
        this.m_Animator = anim;
    }


    public void SetAnimationEvent(float time,string functionName)
    {
        AnimationEvent aEvent = new AnimationEvent();
        aEvent.time = time;
        aEvent.functionName = functionName;
        m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.AddEvent(aEvent);
    }

    


}
