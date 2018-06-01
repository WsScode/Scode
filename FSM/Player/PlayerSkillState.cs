using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : HumanState {
    private SkillManager sm;
    private SkillInfo m_activeSkill;
    private SkillMachine m_sm;
    

    public PlayerSkillState() {
        stateID = StateID.Cast_Skill;
    }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        Debug.Log("Enter SKillState");
        sm = attr.GetComponent<SkillManager>();
        m_animator.SetBool("Attack",true);
        
        m_activeSkill = attr.GetComponent<SkillsInfo>().GetActiveSkill();
        //启动技能状态机
        m_sm = SkillLogic.CreateSkillMechine(m_activeSkill,attr.transform.position,attr.gameObject);

       

    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
    }

    public override IEnumerator DoLogic()
    {

        //SkillInfo activeSkill = sm.GetActiveSkill();
        //MP不足提示信息 TODO
        if (attr.MP < m_activeSkill.M_Mp || sm.m_ISkill) attr.StopCoroutine(DoLogic()) ;
        yield return 0;
        //MyEvent evt = new MyEvent(MyEvent.MyEventType.SkillTrigger);
        //MyEventSystem.m_MyEventSystem.PushEvent(evt);


        //sm.m_ISkill = true;
        m_animator.SetInteger("AttackNum", m_activeSkill.M_AnimCondition);
        string animaName = "Attack" + m_activeSkill.M_AnimCondition;Debug.Log(animaName + "   " + (AnimationController.GetAnimationLenth(m_animator, animaName) / m_animator.speed));
        float timer = 0;
        while (!isExitState)
        {
            if (timer > (AnimationController.GetAnimationLenth(m_animator, animaName) / m_animator.speed))
            {
              
                break;
            }
            timer += Time.deltaTime; 
            yield return null;
        }
        MyEventSystem.m_MyEventSystem.PushEvent(new MyEvent(MyEvent.MyEventType.AnimationOver));
        m_animator.SetInteger("AttackNum", -m_activeSkill.M_AnimCondition);
        m_sm.Stop();//停止状态机
        stateCtrl.ResetNextState();
        sm.RestActiveSkill();//重置当前技能
        fsm.PerformTransition(Transition.IsFight);



        ////Debug.Log(m_animator.GetCurrentAnimatorClipInfo(0).Length);
        //m_animator.speed = 1f;
        //Debug.Log(GetCurrentAnim().clip.length);

        ////加载技能特效 
        ////if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack" + activeSkill.M_AnimCondition)){ yield return 0; }
        ////技能特效和动画的时间差
        //yield return new WaitForSeconds(1.5f);
        //sm.LoadSkillEffect(m_activeSkill, new Vector3(0,1,0.5f));

        ////等待技能释放完毕 也就是动画结束就可以进入下一个技能释放
        ////yield return new WaitForSeconds(m_animator.GetCurrentAnimatorClipInfo(0).Length);
        //if (m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        //{
        //    sm.m_ISkill = false;
        //    m_animator.SetInteger("AttackNum", -m_activeSkill.M_AnimCondition);9
        //    playerctrl.ResetNextState();
        //    sm.RestActiveSkill();//重置当前技能
        //    fsm.PerformTransition(Transition.IsFight);
        //}














        yield return null;
    }







}
