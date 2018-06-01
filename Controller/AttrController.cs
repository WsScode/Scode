using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//[RequireComponent(typeof(PlayerBaseAttack))]
/// <summary>
/// 访问玩家的属性都是从这访问
/// </summary>
public class AttrController : MonoBehaviour {

    public static AttrController Instance;

    public PlayerInfo m_PlayerInfo { get; private set; }
    public SkillsInfo m_SkillsInfo { get; private set; }
    public SkillManager m_SkillManager { get; private set; }
    public ArcherSkill m_ArcherSkill { get; private set; }
    public PlayerStateController m_PlayerStateController { get; private set; }
    [HideInInspector]public ETCJoystick myJoystick;
    private Quaternion m_Rotation;
    public bool m_IsDie = false;
    private bool m_canRotate = true;



    private void Awake()
    {
        myJoystick = ETCInput.GetControlJoystick("MyJoystick");
        //myJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<ETCJoystick>();

        //TODO 读取保存的信息 如果为空才新添加
        m_PlayerInfo = transform.gameObject.AddComponent<PlayerInfo>();
        m_SkillsInfo = transform.gameObject.AddComponent<SkillsInfo>();
        m_SkillManager = transform.gameObject.AddComponent<SkillManager>();
        m_ArcherSkill = transform.gameObject.AddComponent<ArcherSkill>();
        m_PlayerStateController = transform.gameObject.AddComponent<PlayerStateController>();
        m_PlayerStateController.attr = this;
      
    }

    private void Start()
    {
        Instance = this;
    }

    
    public void Attack()
    {

    }

    /// <summary>
    /// 玩家普攻逻辑
    /// </summary>
    public void NormalAttack()
    {

    }

    /// <summary>
    ///
    /// </summary>
    public void SkillAttack(SkillPos.SkillPosType pos)
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool Fight()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 5);
        if (cols.Length > 0)
        {
            foreach (Collider c in cols)
            {
                if (c.tag == "Enemy")
                {
                    return true;
                }
            }
        }
        if (SceneManager.GetActiveScene().buildIndex > 2)
        {
            return true;
        }
        return false;
    }

    private void FixedUpdate()
    {
        if (m_canRotate)
        {
            if (transform.rotation != m_Rotation)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, m_Rotation, Time.deltaTime * 7);
            }
        }
        
    }


    public void TargetRotation(Quaternion target)
    {
        m_Rotation = target;
        m_canRotate = true;
    }

    public void StopRotation()
    {
        m_canRotate = false;
    }

    public virtual int MP
    {
        get { return m_PlayerInfo.M_Mp; }
        set { m_PlayerInfo.M_Mp = value; }
    }
    public virtual int HP
    {
        get { return m_PlayerInfo.M_Hp; }
        set { m_PlayerInfo.M_Hp = value; }
    }






}
