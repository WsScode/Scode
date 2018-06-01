using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





/// <summary>
/// 管理 动画 信息  行为 的总脚本
/// </summary>
public class Player : MonoBehaviour {

   

//    private float speed; 
//    private bool isMove = false;
//    private bool isDeath = false;
//    private bool isAttack = false;
//    private bool isRiding = false;//是否在坐骑上
//    private PlayerMove m_playerMove;
//    private PlayerInfo m_playerInfo;
//    private PlayerAnimation m_playerAnimation;
//    private Animator m_animator;
//    //public Transform animalPos;


//    private void Awake()
//    {
        
//        m_playerMove = GetComponent<PlayerMove>();
//        m_playerInfo = GetComponent<PlayerInfo>();
//        m_playerAnimation = GetComponent<PlayerAnimation>();
//        m_animator = GetComponent<Animator>();
      
//;
//    }

//    private void Start()
//    {
//        speed = m_playerInfo.M_MoveSpeed;
//        //if (animalPos.childCount != 0) { transform.position = transform.position + new Vector3(0, 1.6f, 0); }
        
//    }


//    public void FixedUpdate()
//    {

//        if (Input.GetMouseButtonDown(0)) {
//            m_animator.SetBool("Fight", false);
//            m_animator.SetFloat("MoveSpeed",1);
//            //m_animator.SetTrigger("Pick");

//        }
//        if (isDeath) return;
//        //isMove = m_playerMove.MoveByButton(speed);
//        isMove = m_playerMove.MoveByMouse(speed);//移动
        
//    }
}
