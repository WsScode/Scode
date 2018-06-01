using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


//角色类型枚举
public enum RoleType { MageFemale , MageMale, ArcherFemale, ArcherMale, WarriorFemale, WarriorMale,  }//创建的角色类型


/// <summary>
/// 玩家信息管理类
/// </summary>
public class PlayerInfo : MonoBehaviour {

    public static PlayerInfo Instance;
    private Dictionary<string, int> playerinfoDic = new Dictionary<string, int>();
    private PlayerInfoManager pim;
    [HideInInspector]public int beforePower = 0;//更新前的战斗力
    [HideInInspector]public bool isUpdate = false;//战斗力是否变化
    private FightPoewrUICtrl fpUICtrl;

    #region 字段
    private int level;
    private int hp;
    private int hp_Max;
    private int mp;
    private int mp_Max;
    private int exp;//当前等级已获得的经验
    private int moveSpeed;
    private int strength;
    private int intellect;
    private int agility;
    private int stamina;
    private int attack;
    private int totalExp;//当前等级的总经验
    private int fightingPower;//战斗力
    private Weapon.WeaponType weaponType;
    private Weapon.MainHandWeaponType mainWeaponType;
    private int skillPoint;//技能点 每级5点
    private int attackDistance;
    #endregion

    #region 属性
    public int M_Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }

    public int M_Hp
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;
        }
    }

    public int M_Exp
    {
        get
        {
            return exp;
        }

        set
        {
            exp = value;
        }
    }

    public int M_MoveSpeed
    {
        get
        {
            return moveSpeed;
        }

        set
        {
            moveSpeed = value;
        }
    }

    public Weapon.WeaponType M_WeaponType
    {
        get
        {
            return weaponType;
        }

        set
        {
            weaponType = value;
        }
    }

    public int M_Mp
    {
        get
        {
            return mp;
        }

        set
        {
            mp = value;
        }
    }

    public Weapon.MainHandWeaponType M_MainWeaponType
    {
        get
        {
            return mainWeaponType;
        }

        set
        {
            mainWeaponType = value;
        }
    }

    public int M_Strength
    {
        get
        {
            return strength;
        }

        set
        {
            strength = value;
        }
    }

    public int M_Intellect
    {
        get
        {
            return intellect;
        }

        set
        {
            intellect = value;
        }
    }

    public int M_Stamina
    {
        get
        {
            return stamina;
        }

        set
        {
            stamina = value;
        }
    }

    public int M_Attack
    {
        get
        {
            return attack;
        }

        set
        {
            attack = value;
        }
    }

    public int M_Agility
    {
        get
        {
            return agility;
        }

        set
        {
            agility = value;
        }
    }

    public int M_TotalExp
    {
        get
        {
            return totalExp;
        }

        set
        {
            totalExp = value;
        }
    }

    public int M_FightingPower
    {
        get
        {
            return fightingPower;
        }

        set
        {
            fightingPower = value;
        }
    }

    public int M_SkillPoint
    {
        get
        {
            return skillPoint;
        }

        set
        {
            skillPoint = value;
        }
    }

    public int M_Hp_Max
    {
        get
        {
            return hp_Max;
        }

        set
        {
            hp_Max = value;
        }
    }

    public int M_Mp_Max
    {
        get
        {
            return mp_Max;
        }

        set
        {
            mp_Max = value;
        }
    }

    public int M_AttackDistance
    {
        get
        {
            return attackDistance;
        }

        set
        {
            attackDistance = value;
        }
    }
    #endregion





    private void Awake()
    {
        fpUICtrl = GameObject.FindGameObjectWithTag("ControllerHandler").GetComponent<FightPoewrUICtrl>();
        MyEventSystem.m_MyEventSystem.RegisterEvent(MyEvent.MyEventType.PlayerInfo, OnPlayerInfoChange);
        InitInfo();
       
        Instance = this;
    }

    private void Start()
    {
        
    }


    public void OnPlayerInfoChange(MyEvent _event)
    {

    }


    private void InitInfo() {
        level = 1;
        hp = 100;
        mp = 100;
        exp = 0;
        moveSpeed = 20;
        strength = 100;
        intellect = 100;
        agility = 100;
        stamina = 100;
        attack = 300;
        totalExp = level * 10000;
        fightingPower = CalculatePower();
        attackDistance = 5;
        weaponType = Weapon.WeaponType.MainHand;
    }

    /// <summary>
    /// 推送事件 更新相关UI和属性
    /// </summary>
    /// <param name="eventType"></param>
    public void PushEvent( MyEvent _event) {
        MyEventSystem.m_MyEventSystem.PushEvent(_event);
        
    }


    /// <summary>
    /// 返回根据当前属性计算的战斗力 （自定）
    /// 四维 精E（MP 魔防） 力S（物攻） 智I（魔攻） 敏A（闪避）
    /// </summary>
    /// <returns></returns>
    public int CalculatePower()
    {
        beforePower = fightingPower;
        fightingPower = (int)(
           M_Hp * 0.2f
         + M_Mp * 0.05f
         + M_Agility * 0.25f
         + M_Attack * 0.4f
         + M_Stamina * 0.3f
         + M_Intellect * 0.1f //TODO 还需要判断当前人物的攻击属性
         + M_Level * 500
         + M_Strength * 0.4f
         + M_MoveSpeed * 0.2f
         );

        if (isUpdate) { fpUICtrl.IsUpdateShow(beforePower,fightingPower); }
        return fightingPower;
    }


    public Dictionary<string, int> GetPlayerinfoDic()
    {
        playerinfoDic.Add("level", level);
        playerinfoDic.Add("hp", hp);
        playerinfoDic.Add("moveSpeed", moveSpeed);
        playerinfoDic.Add("strength", strength);
        playerinfoDic.Add("intellect", intellect);
        playerinfoDic.Add("agility", agility);
        playerinfoDic.Add("energy", stamina);
        playerinfoDic.Add("attack", attack);
        return playerinfoDic;
    }


    public void SetProperty()
    {

    }


    public void GetProperty()
    {

    }

    


}
