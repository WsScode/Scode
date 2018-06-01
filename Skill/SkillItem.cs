using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SkillItem : MonoBehaviour {

    private Image m_SkillIcon;
    private Text m_SkillName;
    private Text m_SkillLevel;
    private Button m_AddLevel;
    private Button m_ReduceLevel;
    private Button m_ClickBox;
    private Image m_ClickBoxIMG;

    private GameObject skillpanel;
    private SkillPanel m_SkillPanel;
    private SkillsInfo m_SkillsInfo;
    private SkillInfo m_Skill;


    private void Awake()
    {
        m_SkillIcon = transform.Find("SkillIcon").GetComponent<Image>();
        m_SkillName = transform.Find("SkillName").GetComponent<Text>();
        m_SkillLevel = transform.Find("SkillLevel").GetComponent<Text>();
        m_AddLevel = transform.Find("AddButton").GetComponent<Button>();
        m_ClickBox = transform.Find("ClickBox").GetComponent<Button>();
        m_ReduceLevel = transform.Find("ReduceButton").GetComponent<Button>();
        m_ClickBox = transform.Find("ClickBox").GetComponent<Button>();
        m_ClickBoxIMG = transform.Find("ClickBox").GetComponent<Image>();

        skillpanel = transform.parent.parent.parent.parent.gameObject;
        m_SkillsInfo = skillpanel.GetComponent<SkillsInfo>();
        m_SkillPanel = skillpanel.GetComponent<SkillPanel>();
        m_ClickBox.onClick.AddListener(UpdateShowDesc);

    }

    public void UpdateShow(SkillInfo skill)
    {
        if (skill == null) return;
        m_Skill = skill;
        //加载图集的图片
        Object[] os = Resources.LoadAll("Sprites/Skill/Skill_GongShou");
        for (int i = 0; i < os.Length; i++)
        {
            if (os[i].GetType() == typeof(Sprite) && os[i].name == skill.M_Icon)
            {
                m_SkillIcon.sprite = (Sprite)os[i];
            }
        }
        //m_SkillIcon.sprite = Resources.Load<Sprite>(skill.M_Icon);print(AssetDatabase.LoadAssetAtPath<Sprite>(skill.M_Icon));
        m_SkillName.text = skill.M_Name;
        m_SkillLevel.text = "LV" + skill.M_LearnedLevel;
        m_AddLevel.onClick.AddListener(LearnSkill);
        m_ReduceLevel.onClick.AddListener(ReduceSkill);
    }

    public void UpdateShowDesc()
    {
        m_SkillPanel.UpdateShowSkillDesc(m_Skill);
       
    }

    private void LearnSkill()
    {
        m_SkillsInfo.LearnSkill(m_Skill.M_Id);
    }
    private void ReduceSkill()
    {
        m_SkillsInfo.ReduceSkill(m_Skill.M_Id);
    }
}
