using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 1、玩家角色的管理zhuyaoo
/// 2、玩家人物的状态管理  将每个状态分成单独脚本 继承自fsmstate 
/// 3、每个状态 是否产生事件 事件注册否可以在每个状态里进行 
/// 4、一些辅助逻辑
/// </summary>
public class PlayerStateController : AIBase {

    public static PlayerStateController Instance;

    [HideInInspector] public FSMSystem m_FsmSystem;//控制状态添加和切换
    PlayerIdleState idleState;
    PlayerMoveState moveState;
    PlayerFightState fightState;
    PlayerSkillState skillState;

    //public bool IsFight { get; private set; }
    public int m_AttackNum { get; set; }
    private Transform player;
    public bool m_IsMove;



    private FSMState m_NextState = null;
    private Dictionary<StateID,FSMState> stateDic;

    public ETCJoystick myJoystick;



    private void Awake()
    {
        
    }

    private void Start()
    {

        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Transform>();
        myJoystick = ETCInput.GetControlJoystick("MyJoystick");
        InitStateMachine();

        Instance = this;
    }


    /// <summary>
    /// 初始化状态机 
    /// 状态和转换条件在这统一初始化添加
    /// 默认 除了skill和fight状态 其余状态要切换到其他状态的时候 要先切换到idle状态
    /// </summary>
    protected override void InitStateMachine()
    {
        stateDic = new Dictionary<StateID, FSMState>();
        m_FSM = new FSMSystem();
        idleState = new PlayerIdleState();
        moveState = new PlayerMoveState();
        fightState = new PlayerFightState();
        skillState = new PlayerSkillState();

       
        //attr = player.GetComponent<AttrController>();
        m_FSM.attr = attr;

        m_FSM.AddState(idleState, this);
        stateDic.Add(idleState.ID,idleState);
        idleState.AddTransition(Transition.IsIdle, StateID.Idle);
        idleState.AddTransition(Transition.IsMove, StateID.Move);
        idleState.AddTransition(Transition.IsDeath, StateID.Death);
        idleState.AddTransition(Transition.IsFight, StateID.Fight);
        idleState.AddTransition(Transition.IsCast_Skill, StateID.Cast_Skill);
        idleState.AddTransition(Transition.IsRiding, StateID.Riding);
        idleState.AddTransition(Transition.IsVigilance, StateID.Vigilance);
        idleState.AddTransition(Transition.IsChatStart, StateID.ChatStart);
        idleState.AddTransition(Transition.IsChatEnd, StateID.ChatEnd);


        m_FSM.AddState(moveState, this);
        stateDic.Add(moveState.ID, moveState);
        moveState.AddTransition(Transition.IsIdle, StateID.Idle);

        m_FSM.AddState(fightState, this);
        stateDic.Add(fightState.ID, fightState);
        fightState.AddTransition(Transition.IsIdle, StateID.Idle);
        fightState.AddTransition(Transition.IsCast_Skill, StateID.Cast_Skill);
        fightState.AddTransition(Transition.IsMove, StateID.Move);

        m_FSM.AddState(skillState, this);
        stateDic.Add(skillState.ID, skillState);
        skillState.AddTransition(Transition.IsIdle, StateID.Idle);
        skillState.AddTransition(Transition.IsFight,StateID.Fight);

        //初始状态分主城（Idle）和地下城（Fight）
        //设置初始状态（idle状态）
        
        if (AttrController.Instance.Fight())
        {
            m_FSM.SetCurrentState(fightState);
            m_FSM.CurrentState.m_MyJoystick = ETCInput.GetControlJoystick("MyJoystick");
            //m_FSM.attr.StartCoroutine(fightState.DoLogic());
        }
        else
        {
            m_FSM.SetCurrentState(idleState);
            m_FSM.CurrentState.m_MyJoystick = ETCInput.GetControlJoystick("MyJoystick");
        }
        //m_FSM.attr.StartCoroutine(idleState.DoLogic());
        //idleState.RunLogic();

        //测试战斗状态
        //m_FSM.SetCurrentState(fightState);
        //m_FSM.CurrentState.m_MyJoystick = ETCInput.GetControlJoystick("MyJoystick");
        //m_FSM.attr.StartCoroutine(fightState.DoLogic());

        
    }

    public override void SetNextState(StateID stateID,Transition transition = Transition.IsIdle)
    {
        if (stateID == m_FSM.CurrentState.ID || stateID == StateID.NullStateID) return;
        m_NextState = stateDic.TryGetTool(stateID);
        print(" NextState:" + m_NextState);
    }

    public override FSMState GetNextState()
    {
        return m_NextState;
    }

    public override void ResetNextState()
    {
        m_NextState = null;
    }
    
   


    

   

    /// <summary>
    /// 普攻 和 技能攻击
    /// 1普攻 2、3、4技能
    /// 参数为
    /// </summary>
    public int OnSkillAttack()
    {
        if(m_FSM.CurrentState != fightState)
        {
            return -1;
        }
        return m_AttackNum;

    }

    /// <summary>
    /// 血量变化的时候检查人物是否死亡
    /// </summary>
    public void CheckDeath()
    {

    }

   

}
