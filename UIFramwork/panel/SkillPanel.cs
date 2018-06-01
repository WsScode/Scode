using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : BasePanel {

    private Transform m_Parent;//放置skilliem的父物体
    private SkillsInfo m_SkillsInfo;

    private Text m_SkillDesc;
    private Button m_ResetPointButton;
    

    public override void Awake()
    {
        m_showUIType = ShowUIType.Normal; 
        string root = "SkillItemsPanel/Panel/SkillList";
        m_Parent = transform.Find(root);
        m_SkillDesc = transform.Find("SkillInfo/SkillDesc").GetComponent<Text>();
        m_ResetPointButton = transform.Find("ResetAllPointButton").GetComponent<Button>();
    }


    private void Start()
    {
        m_SkillsInfo = SkillsInfo.Instance;
        m_ResetPointButton.onClick.AddListener(m_SkillsInfo.ResetAllPoint);
    }

    public void UpdateShowSkillDesc(SkillInfo skill)
    {
        m_SkillDesc.text = string.Format("{0}\n消耗:{1}MP\n伤害:{2}%\n下一等级伤害:{3}%",skill.M_Desc,skill.M_Mp,skill.M_ApplyValue,skill.M_ApplyValue + 50);
    }


    public void SetSkillItem(SkillInfo skill)
    {
        SkillItem skillitem = Instantiate(Resources.Load<GameObject>("UIPanel/SkillItem"),m_Parent).GetComponent<SkillItem>();
        skillitem.UpdateShow(skill);
        
    }


    public override void OnEnter(UIController uIController)
    {
        base.OnEnter(uIController);
        InitPanel();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;//检测点击
    }



    public override void OnPause()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;//检测点击
    }


     /// <summary>
    /// 初始化技能面板
    /// 如果没有学习技能  那么直接根据原始数据初始化
    /// 如果学习的有技能  得到学习技能的信息
    /// </summary>
    public void InitPanel() {

        Dictionary<int, SkillInfo> learnedSkills = SkillsInfo.Instance.GetLearnedSkill();
        Dictionary<int, SkillInfo> skillsDic = SkillsInfo.Instance.GetAllSkill();
        if (learnedSkills == null)
        {
            learnedSkills = new Dictionary<int, SkillInfo>();
        }
        SkillInfo skill = null;
        if (skillsDic.Count == 0)
        {
            Debug.LogError("未得到技能信息");
            return;
        }

        foreach (int id in skillsDic.Keys)
        {
            if (learnedSkills.Count > 0)
            {
                foreach (int l_id in learnedSkills.Keys)
                {
                    if (id == l_id)
                    {
                        skill = learnedSkills.TryGetTool(l_id);
                    }
                    else
                    {
                        skill = skillsDic.TryGetTool(id);
                    }
                }
            }
            else
            {
                skill = skillsDic.TryGetTool(1);
            }
            skill = skillsDic.TryGetTool(id);
            SetSkillItem(skill);
            UpdateShowSkillDesc(skill);
        }

    }



}
