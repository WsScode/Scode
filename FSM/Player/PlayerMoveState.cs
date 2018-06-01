using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
using System;

public class PlayerMoveState : HumanState {

    private PlayerMove m_PlayaerMove;
    private float speed = 1;
    public float Speed { get; set; }

    //private ETCJoystick m_MyJoystick;

    private Vector3 m_PlayerPos;

    public PlayerMoveState() {
        stateID = StateID.Move;
    }
    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        Debug.Log("Enter MoveState");
        attr.myJoystick.tmSpeed = 10;
        //attr.GetComponent<CharacterController>().enabled = true;


    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
        attr.myJoystick.tmSpeed = 0;
        //attr.GetComponent<CharacterController>().enabled = false;
        m_animator.SetFloat("MoveSpeed", 0);

    }

    public override IEnumerator DoLogic()
    {
        Rigidbody r = attr.GetComponent<Rigidbody>();

        while (!isExitState)
        {
            if (stateCtrl.GetNextState() != null && stateCtrl.GetNextState() != fsm.CurrentState)
            {
                fsm.PerformState(stateCtrl.GetNextState());
                stateCtrl.SetNextState(this.ID);
                break;
            }
            //if (attr.m_SkillManager.GetActiveSkill() != null || stateCtrl.GetNextState() != null)
            //{
            //    fsm.PerformState(stateCtrl.GetNextState());Debug.Log("here");
            //    break;
            //}
            
            if ((Mathf.Abs(attr.myJoystick.axisX.axisValue) < 0.1f && Mathf.Abs(attr.myJoystick.axisY.axisValue) < 0.1f) && stateCtrl.GetNextState() == null)
            {
                SetIdle(); 
                break;
            }
           
            m_animator = attr.GetComponent<Animator>();
            m_animator.SetFloat("MoveSpeed", 1);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(attr.myJoystick.axisX.axisValue, 0, attr.myJoystick.axisY.axisValue), Vector3.up);
            attr.transform.rotation = Quaternion.Lerp(attr.transform.rotation, newRotation, Time.deltaTime * 7);

            //r.MovePosition(attr.transform.position + (new Vector3(attr.myJoystick.axisX.axisValue, 0, attr.myJoystick.axisY.axisValue) * 20));
            Vector3 v = attr.transform.forward * Time.deltaTime * 5;

            v.y -= 10;
            attr.GetComponent<CharacterController>().Move(v);
            //attr.GetComponent<CharacterController>().SimpleMove(new Vector3(attr.myJoystick.axisX.axisValue,0, attr.myJoystick.axisY.axisValue) * attr.m_PlayerInfo.M_MoveSpeed *0.2f);
            //attr.transform.position.y -= 1;
            //attr.transform.Translate(new Vector3(attr.myJoystick.axisX.axisValue, 0, attr.myJoystick.axisY.axisValue) * 0.5f * Time.deltaTime, Space.World);
            //r.AddForce(new Vector3(attr.myJoystick.axisX.axisValue, 0, attr.myJoystick.axisY.axisValue) * 50);
            //Debug.Log("位置：" + attr.transform.position + (new Vector3(attr.myJoystick.axisX.axisValue, 0, attr.myJoystick.axisY.axisValue) * 10));
            //Debug.Log("attr.myJoystick.axisX.axisValue: " + attr.myJoystick.axisX.axisValue + "attr.myJoystick.axisY.axisValue: " + attr.myJoystick.axisY.axisValue);


            yield return null;
        }
       
    }



    public override void RunLogic()
    {
        while (!isExitState)
        {
            m_PlayerPos = m_Player.transform.position;
            m_animator.SetFloat("MoveSpeed", 1);

            if ((Mathf.Abs(m_MyJoystick.axisX.axisValue) < 0.1f && Mathf.Abs(m_MyJoystick.axisY.axisValue) < 0.1f) || stateCtrl.IsFight)
            {
                
                SetIdle();
                m_animator.SetFloat("MoveSpeed", 0);
                break;
            }

            Quaternion newRotation = Quaternion.LookRotation(new Vector3(m_MyJoystick.axisX.axisValue, 0, m_MyJoystick.axisY.axisValue), Vector3.up);
            m_Player.transform.rotation = Quaternion.Lerp(m_Player.transform.rotation, newRotation, Time.deltaTime * 5);
            m_Player.GetComponent<Rigidbody>().MovePosition(m_Player.transform.position + new Vector3(m_MyJoystick.axisX.axisValue, 0, m_MyJoystick.axisY.axisValue) * 5 * Time.fixedDeltaTime);
        }
    }









}
