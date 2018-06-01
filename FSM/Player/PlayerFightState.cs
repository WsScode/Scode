using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 此状态 为准备攻击状态和基础攻击
/// </summary>
public class PlayerFightState : HumanState {

    //private Button m_NormalAttack;
    private bool shoot = false;

    private Transform targetEnemy;
    //private List<Enemy> enemyList;//周围攻击范围内的敌人
    BulletPool bp;
    PlayerAnimation pa;



    public PlayerFightState() {
        stateID = StateID.Fight;
       
    }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        MyEventSystem.m_MyEventSystem.RegisterEvent(MyEvent.MyEventType.BaseAttack, DoEvent);
        m_MyJoystick = ETCInput.GetControlJoystick("MyJoystick");
        m_animator.SetBool("Fight",true);
        bp = BulletPool.Instance;
        //bp.InitArrowPool();//初始化物品池
        Debug.Log("Enter FightState");

    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
        MyEventSystem.m_MyEventSystem.DropEvent(MyEvent.MyEventType.BaseAttack, DoEvent);

    }

    public override IEnumerator DoLogic()
    {
        while (!isExitState)
        {
            if (stateCtrl.GetNextState() != null && stateCtrl.GetNextState() != fsm.CurrentState)
            {
                fsm.PerformState(stateCtrl.GetNextState());Debug.Log("NEXTSTATE");
                stateCtrl.SetNextState(this.ID);
                break;
            }
            if (Mathf.Abs(attr.myJoystick.axisX.axisValue) > 0.1f || Mathf.Abs(attr.myJoystick.axisY.axisValue) > 0.1f)
            {
                //SetIdle();
                fsm.PerformTransition(Transition.IsMove);
                break;
            }
            if (shoot)
            {
                //AttackTarget(attr.m_PlayerInfo.M_AttackDistance);
                //AttackForword();

                //if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
                //{  
                //    //只适用于当前职业 还需扩展
                //    //播放射击动画
                //    m_animator.speed = 1.5f;
                //    m_animator.SetInteger("AttackNum", 1);
                //    m_animator.SetBool("Attack", true);
                //    //yield return new WaitForSeconds(0.5f);

                //    //加载子弹
                //    yield return new WaitForSeconds(0.4f);

                //    GameObject go = bp.GetArrow();
                //    go.SetActive(true);
                //    go.transform.position = attr.transform.position + new Vector3(0, 1f, 0);
                //    go.transform.forward = attr.transform.forward;
                //    go.transform.rotation = attr.transform.rotation;
                //    shoot = false;
                //    yield return new WaitForSeconds(0.2f);
                //    //Debug.Log(attr.m_PlayerInfo.M_AttackDistance);
                //    m_animator.SetInteger("AttackNum", 0);
                //    m_animator.speed = 1f;

                //}


                if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
                {

                    Collider[] cols = Physics.OverlapSphere(attr.transform.position, 5);
                    if (cols.Length > 0)
                    {
                        foreach (Collider c in cols)
                        {
                            if (c.tag == "Enemy")
                            {
                                Debug.Log("Enemy: " + c.name);
                                targetEnemy = c.transform;
                            }
                        }
                    }
                    Quaternion newRotation = new Quaternion(0, 0, 0, 0);
                    if (targetEnemy == null)
                    {
                        newRotation = attr.transform.rotation;
                    }
                    else
                    {
                        newRotation = Quaternion.LookRotation(targetEnemy.position - attr.transform.position);
                        attr.TargetRotation(newRotation);//修改朝向 
                    }
                    //attr.transform.rotation = Quaternion.Lerp(attr.transform.rotation, newRotation, Time.deltaTime * 5);

                    
                    m_animator.speed = 1.5f;
                    m_animator.SetInteger("AttackNum", 1);
                    m_animator.SetBool("Attack", true);
                    yield return new WaitForSeconds(0.4f);
                    GameObject go = bp.GetArrow();//加载子弹
                    go.SetActive(true);
                    go.transform.position = attr.transform.position + new Vector3(0, 1f, 0);
                    go.transform.forward = attr.transform.forward;
                    go.transform.rotation = attr.transform.rotation;
                    shoot = false;
                    yield return new WaitForSeconds(0.2f);
                    //Debug.Log(attr.m_PlayerInfo.M_AttackDistance);
                    m_animator.SetInteger("AttackNum", 0);
                    m_animator.speed = 1f;
                    attr.StopRotation();
                }


            }

            yield return null;
        }
    }


    /// <summary>
    /// 普通攻击事件
    /// </summary>
    /// <param name="evt"></param>
    public override void DoEvent(MyEvent evt)
    {
        int attack = attr.m_PlayerInfo.M_Attack;
        Debug.Log("NormalAttack");
        NormalAttack();
        
    }



    public void NormalAttack()
    {
        shoot = true;
    }


    /// <summary>
    /// 得到普攻对象并转向目标 
    /// 这个攻击方式 会让玩家自动转向目标
    /// </summary>
    private void AttackTarget(int attackDistance)
    {
        if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            Collider[] cols = Physics.OverlapSphere(attr.transform.position, attackDistance);
            if (cols.Length > 0)
            {
                foreach (Collider c in cols)
                {
                    if (c.tag == "Enemy")
                    {
                        Debug.Log("Enemy: " + c.name);
                        targetEnemy = c.transform;
                    }
                }
            }
            Quaternion newRotation = new Quaternion(0, 0, 0, 0);
            if (targetEnemy == null)
            {
                newRotation = attr.transform.rotation;
            }
            else
            {
                newRotation = Quaternion.LookRotation(targetEnemy.position - attr.transform.position);
            }
            //attr.transform.rotation = Quaternion.Lerp(attr.transform.rotation, newRotation, Time.deltaTime * 5);

            attr.TargetRotation(newRotation);//修改朝向
            m_animator.SetBool("Attack", true);
            GameObject go = BulletPool.Instance.GetArrow();//加载子弹
            go.transform.position = attr.transform.position + new Vector3(0, 1f, 0);

            go.transform.rotation = attr.transform.rotation;
            go.SetActive(true);
            m_animator.speed = 1.5f;
            m_animator.SetInteger("AttackNum", 1);
        }
       

    }

    /// <summary>
    /// 朝玩家当前 前方攻击
    /// </summary>
    public void AttackForword()
    {
        if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {

        }
        //播放射击动画
        m_animator.speed = 1.5f;
        m_animator.SetInteger("AttackNum", 1);
        m_animator.SetBool("Attack", true);
        //加载子弹
        GameObject go = BulletPool.Instance.GetArrow();
        go.transform.position = attr.transform.position + new Vector3(0, 1f, 0);
        go.transform.forward = attr.transform.forward;
        //go.transform.rotation = attr.transform.rotation;
        go.SetActive(true);
      
        

    }

    /// <summary>
    /// 攻击前方扇形区域
    /// 测试120度扇形范围
    /// </summary>
    public void AttackForwardRange()
    {
        if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            //Collider[] cs = Physics;
        }
    }





}
