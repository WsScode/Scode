using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightButtonPanel : BasePanel {
    private Button m_NormalAttack;



    private List<SkillPos> m_PosList;

    public override void Awake()
    {
        base.Awake();
        m_showUIType = ShowUIType.Normal;
        m_NormalAttack = transform.Find("NormalAttackButton").GetComponent<Button>();

    }

    private void Start()
    {
        m_NormalAttack.onClick.AddListener(OnNormalAttackClick);
        //SetSkillButton();

    }

    public override void OnEnter(UIController uIController)
    {
        base.OnEnter(uIController);
    }


    /// <summary>
    /// 普通攻击事件
    /// </summary>
    public void OnNormalAttackClick()
    {
        //PlayerStateController.Instance.m_AttackNum = 0;
        //PlayerStateController.Instance.OnSkillAttack();
        MyEvent evt = new MyEvent(MyEvent.MyEventType.BaseAttack);
        evt.m_MyEventType = MyEvent.MyEventType.BaseAttack;
        MyEventSystem.m_MyEventSystem.PushEvent(evt);
    }

    public void SetSkillButton(SkillInfo skill)
    {
        if (m_PosList == null)
        {
            m_PosList = new List<SkillPos>();
        }
        SkillPos pos;
        int i = 1;
        foreach (Transform child in transform)
        {
            if (child.name == "Skill" + i)
            {
                child.gameObject.AddComponent<SkillPos>().m_SkillPosType = (SkillPos.SkillPosType)System.Enum.ToObject(typeof(SkillPos.SkillPosType), i - 1);
                pos = child.GetComponent<SkillPos>();
                pos.SetSkill(skill);//Test
                child.GetComponent<Button>().onClick.AddListener(pos.Onclick);//技能键被点击
                //if (pos == null) continue;
                m_PosList.Add(pos);
                i++;
            }


        }


    }



    public List<SkillPos> GetSkillsPos()
    {
        return m_PosList == null ? null : m_PosList;
    }

    //public SkillPos.SkillPosType GetCurrentOnclickPos()
    //{
    //    return m_PosType;
    //}

    public void SetSkillItem()
    {

    }











}


/// <summary>
/// 管理攻击按钮 技能格子
/// </summary>
public class SkillPos:MonoBehaviour {
    public enum SkillPosType
    {
        SkillPos1,
        SkillPos2,
        SkillPos3,
    }
    public SkillPosType m_SkillPosType;
    public SkillInfo m_skillinfo { get; private set; }
    private Image m_coldImage;
    private bool m_isClick = false;
    private float m_colder;
    private float m_cold;

    private void Start()
    {
        m_coldImage = transform.Find("Cold").GetComponent<Image>();
        m_coldImage.fillAmount = 0;
    }

    /// <summary>
    /// 设置技能栏的技能
    /// </summary>
    /// <param name="skill"></param>
    public void SetSkill(SkillInfo skill)
    {
        //print(skill.M_Icon);
        if (skill == null) return;
        gameObject.GetComponent<Image>().sprite = LoadSprite.LoadSpriteBySkill(skill);
        m_skillinfo = skill;
    }

    private void Update()
    {

        if (m_isClick && m_skillinfo != null)
        {
            
            m_cold += Time.deltaTime;
            m_colder = m_skillinfo.M_ColdTime;
            if (m_cold > m_colder)
            {
                m_cold = 0;
                m_isClick = false;
            }
            else
            {
                m_coldImage.fillAmount = 1 - (m_cold / m_colder);
            }
        }
        
    }
    public void Onclick()
    {
        if (m_skillinfo == null || m_isClick || !AttrController.Instance.Fight()) return;
        m_isClick = true;

        //AttrController.Instance.GetComponent<SkillManager>().SetActiveSkill(m_skillinfo);
        AttrController.Instance.GetComponent<SkillsInfo>().SetActiveSkill(m_skillinfo.M_Id);

    }

    



}
