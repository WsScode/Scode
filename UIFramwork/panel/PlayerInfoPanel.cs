using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoPanel : BasePanel {
    #region 人物信息UI  还需要完善
    private Image m_ExpImage;
    private Text m_ExpText;
    private Button m_KnapsackButton;
    private Button m_IntensifyButton;//强化 TODO 
    private Button m_SkillButton;//技能 TODO
    private Text m_LevelText;
    private Text m_HPText;
    private Text m_MPText;
    private Text m_StrengthText;
    private Text m_IntellectText;
    private Text m_AgilityText;
    private Text m_EnergyText;
    private Text m_Attack;
    private Text m_MoveSpeed;
    private Text m_FightingPower;

    private Item dressed_item;//当前格子上穿戴的装备

    private List<PlayerEquipmentGrid> e_grids;

    
    #endregion

    private PlayerInfo m_playerInfo;



    public override void Awake()
    {
        base.Awake();
        showUIType = ShowUIType.Normal;
        m_ExpImage = transform.Find("EXPBar").GetComponent<Image>();
        m_ExpText = transform.Find("EXPText").GetComponent<Text>();
        m_KnapsackButton = transform.Find("KnapsackButton").GetComponent<Button>();
        m_IntensifyButton = transform.Find("IntensifyButton").GetComponent<Button>();
        m_SkillButton = transform.Find("SkillButton").GetComponent<Button>();
        m_LevelText = transform.Find("Level").GetComponent<Text>();
        m_HPText = transform.Find("HP").GetComponent<Text>();
        m_MPText = transform.Find("MP").GetComponent<Text>();
        m_StrengthText = transform.Find("Strength").GetComponent<Text>();
        m_IntellectText = transform.Find("Intellect").GetComponent<Text>();
        m_AgilityText = transform.Find("Agility").GetComponent<Text>();
        m_EnergyText = transform.Find("Energy").GetComponent<Text>();
        m_Attack = transform.Find("Attack").GetComponent<Text>();
        m_MoveSpeed = transform.Find("MoveSpeed").GetComponent<Text>();
        m_FightingPower = transform.Find("FightingPower").GetComponent<Text>();


    }

    private void Start()
    {
        m_playerInfo = PlayerInfo.Instance;
        //MyEventType中0~9是玩家状态UI的相关
        for (int i = 0; i < 10; i++) {
            MyEvent.MyEventType met = (MyEvent.MyEventType)System.Enum.ToObject(typeof(MyEvent.MyEventType), i);
            MyEventSystem.m_MyEventSystem.RegisterEvent(met, OnEvent);//注册事件
        }
        UpdateShow();
    }


    public override void OnEnter(UIController uIController)
    {
        base.OnEnter(uIController);

    }

    protected override void OnEvent(MyEvent _event)
    {
        
        UpdateShow();
    }

    /// <summary>
    /// 更新UI显示
    /// </summary>
    public void UpdateShow() {
        m_playerInfo.isUpdate = true;
        m_ExpImage.fillAmount =(float)(m_playerInfo.M_Exp / m_playerInfo.M_TotalExp);
        m_ExpText.text = "EXP:" + m_playerInfo.M_Exp + "/" + m_playerInfo.M_TotalExp;
        m_LevelText.text = m_playerInfo.M_Level + "";
        m_HPText.text = m_playerInfo.M_Hp + "";
        m_MPText.text = (m_playerInfo.M_Mp + m_playerInfo.M_Level * 500) + "";
        m_StrengthText.text = m_playerInfo.M_Strength + "";
        m_IntellectText.text = m_playerInfo.M_Intellect + "";
        m_AgilityText.text = m_playerInfo.M_Agility + "";
        m_EnergyText.text = m_playerInfo.M_Stamina + "";
        m_Attack.text = (m_playerInfo.M_Attack + m_playerInfo.M_Level*20) + "";//TODO武器的攻击力 + 等级基础攻击力 + 力量攻击力加成
        m_MoveSpeed.text = m_playerInfo.M_MoveSpeed + "";
        m_FightingPower.text = "战斗力" + m_playerInfo.CalculatePower() + "";//TODO
        

    }

    

    /// <summary>
    /// 得到所有的装备栏
    /// </summary>
    private void SetPlayerEquipmentGridList() {
        if (e_grids == null) {
            e_grids = new List<PlayerEquipmentGrid>();
            foreach (Transform child in transform) {
                e_grids.Add(child.GetComponent<PlayerEquipmentGrid>());
            }
        }
    }

    public List<PlayerEquipmentGrid> GetPlayerEquipmentGridList() {
        SetPlayerEquipmentGridList();
        return e_grids;
    }

    /// <summary>
    /// 穿戴装备 
    /// </summary>
    /// <param name="item"></param>
    public void PutOn(Item item,Grid grid)
    {
        PlayerEquipmentGrid[] go = transform.Find("Grids").GetComponentsInChildren<PlayerEquipmentGrid>();
        foreach (PlayerEquipmentGrid child in go) {
            if (child.IsRightGrid(item)) {
                if (child.grid_item != null)
                {
                    dressed_item = child.grid_item;
                    child.SetItem(item);
                    grid.ResetGrid();
                    Inventory.Instance.PutItemByID(dressed_item.m_ID);
                    PutOff(dressed_item);//先扣除脱下装备的属性
                }
                else
                {
                    child.SetItem(item);
                    grid.ResetGrid();
                }
                //将装备属性加到人物上
                MyEvent evt = new MyEvent(MyEvent.MyEventType.PlayerInfo);
                if (item is Equipment)
                {
                    //TODO  
                    PlayerInfo.Instance.M_Agility += ((Equipment)item).m_Agility;
                    PlayerInfo.Instance.M_Stamina += ((Equipment)item).m_Stamina;
                    PlayerInfo.Instance.M_Strength += ((Equipment)item).m_Strength;
                    PlayerInfo.Instance.M_Intellect += ((Equipment)item).m_Intellct;
                    PlayerInfo.Instance.M_Hp += ((Equipment)item).m_Hp;
                    PlayerInfo.Instance.M_Mp += ((Equipment)item).m_Mp;
                    
                    MyEventSystem.m_MyEventSystem.PushEvent(evt);
                    
                }
                else if (item is Weapon)
                {
                    PlayerInfo.Instance.M_Attack += ((Weapon)item).m_Damage;
                    PlayerInfo.Instance.M_AttackDistance = (int)((Weapon)item).m_AttackDistance;//TODO
                    MyEventSystem.m_MyEventSystem.PushEvent(evt);
                }
                break;
            }
        }

    }


    public void PutOff(Item item) {
        MyEvent evt = new MyEvent(MyEvent.MyEventType.PlayerInfo);
        if (item is Equipment)
        {
            PlayerInfo.Instance.M_Agility -= ((Equipment)item).m_Agility;
            PlayerInfo.Instance.M_Stamina -= ((Equipment)item).m_Stamina;
            PlayerInfo.Instance.M_Strength -= ((Equipment)item).m_Strength;
            PlayerInfo.Instance.M_Intellect -= ((Equipment)item).m_Intellct;
            PlayerInfo.Instance.M_Hp -= ((Equipment)item).m_Hp;
            PlayerInfo.Instance.M_Mp -= ((Equipment)item).m_Mp;
            MyEventSystem.m_MyEventSystem.PushEvent(evt);
        }
        else if (item is Weapon)
        {
            PlayerInfo.Instance.M_Attack -= ((Weapon)item).m_Damage;
            MyEventSystem.m_MyEventSystem.PushEvent(evt);
        }
    }
    

}
