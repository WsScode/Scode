using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class SkillInfo  {
    private int id;
    private string name;
    private string icon;
    private string desc;
    private ApplyType applyType;
    private ApplyProperty applyProperty;
    private int applyValue;
    private float applyTime;
    private int mp;
    private int coldTime;
    private ApplicableRole applicableRole;
    private int needlevel;
    private int maxLevel;
    private ReleaseType releaseType;
    private int distance;
    private EffectType effectType;
    private int learnedLevel;
    private string effectName;
    private float effectApplyValue;
    private string skillPosType;//技能默认按键位置
    private SkillType skillType;
    private int upPoint;
    private int animCondition;//对应的动画 触发条件数
    private float animaionTime;
    private string skillDataConfiger;//技能模板配置地址
    

    #region 属性
    public int M_Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string M_Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string M_Icon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

    public string M_Desc
    {
        get
        {
            return desc;
        }

        set
        {
            desc = value;
        }
    }

    public ApplyType M_ApplyType
    {
        get
        {
            return applyType;
        }

        set
        {
            applyType = value;
        }
    }

    public ApplyProperty M_ApplyProperty
    {
        get
        {
            return applyProperty;
        }

        set
        {
            applyProperty = value;
        }
    }

    public int M_ApplyValue
    {
        get
        {
            return applyValue;
        }

        set
        {
            applyValue = value;
        }
    }

    public float M_ApplyTime
    {
        get
        {
            return applyTime;
        }

        set
        {
            applyTime = value;
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

    public int M_ColdTime
    {
        get
        {
            return coldTime;
        }

        set
        {
            coldTime = value;
        }
    }

    public ApplicableRole M_ApplicableRole
    {
        get
        {
            return applicableRole;
        }

        set
        {
            applicableRole = value;
        }
    }

    public int M_Needlevel
    {
        get
        {
            return needlevel;
        }

        set
        {
            needlevel = value;
        }
    }

    public int M_MaxLevel
    {
        get
        {
            return maxLevel;
        }

        set
        {
            maxLevel = value;
        }
    }

    public ReleaseType M_ReleaseType
    {
        get
        {
            return releaseType;
        }

        set
        {
            releaseType = value;
        }
    }

    public int M_Distance
    {
        get
        {
            return distance;
        }

        set
        {
            distance = value;
        }
    }

    public EffectType M_EffectType
    {
        get
        {
            return effectType;
        }

        set
        {
            effectType = value;
        }
    }

    public int M_LearnedLevel
    {
        get
        {
            return learnedLevel;
        }

        set
        {
            learnedLevel = value;
        }
    }

    public string M_EffectName
    {
        get
        {
            return effectName;
        }

        set
        {
            effectName = value;
        }
    }

    public float M_EffectApplyValue
    {
        get
        {
            return effectApplyValue;
        }

        set
        {
            effectApplyValue = value;
        }
    }

    public string M_SkillPosType
    {
        get
        {
            return skillPosType;
        }

        set
        {
            skillPosType = value;
        }
    }

    public SkillType M_SkillType
    {
        get
        {
            return skillType;
        }

        set
        {
            skillType = value;
        }
    }

    public int M_UpPoint
    {
        get
        {
            return upPoint;
        }

        set
        {
            upPoint = value;
        }
    }

    public int M_AnimCondition
    {
        get
        {
            return animCondition;
        }

        set
        {
            animCondition = value;
        }
    }

    public float M_AnimaionTime
    {
        get
        {
            return animaionTime;
        }

        set
        {
            animaionTime = value;
        }
    }

    public string M_SkillDataConfiger
    {
        get
        {
            return skillDataConfiger;
        }

        set
        {
            skillDataConfiger = value;
        }
    }
    #endregion


    public SkillInfo(int skillID = 0,int skillLevel = 0)
    {

    }

}

/// <summary>
/// 技能作用于哪个属性
/// </summary>
public enum ApplyProperty
{
    Attack,HP,MP,Defense,
}

/// <summary>
/// 技能释放类型
/// </summary>
public enum ApplyType
{
   SingleTarget,MultiTarget,Self,
}

public enum ApplicableRole
{
   Archer,Warrior,
}

public enum ReleaseType
{
    Enemy,//指定敌人
    Position,//指定位置 范围
    Self
}

/// <summary>
/// 对敌人造成的效果
/// </summary>
public enum EffectType
{
    Fly,//击飞
    Back,//击退
    Slow,//减速
    Dizzy,//眩晕
}

/// <summary>
/// 主动 或者 被动技能
/// </summary>
public enum SkillType
{
    Passivity,//被动
    Initiative//主动
}


/// <summary>
/// 技能属性管理类
/// 解析技能文件 并初始化技能信息 保存
/// </summary>
public class SkillsInfo : MonoBehaviour {

    public static SkillsInfo Instance;

    private Dictionary<int, SkillInfo> skillsDic;//保存以Skill ID为Key的dictionary
    private Dictionary<int, SkillInfo> learnedSkills;//保存已经学习的技能信息

    private SkillPanel m_SkillPanel;

    public SkillInfo m_ActiveSkill { get; private set; }

    private void Awake()
    {
        ParseSkills();

        //TODO这个获得方式不好
        //m_SkillPanel = GameObject.FindGameObjectWithTag(Tags.UIParent).transform.Find("SkillPanel").GetComponent<SkillPanel>();

        Instance = this;

    }

   

    public void ParseSkills()
    {
        skillsDic = new Dictionary<int, SkillInfo>();

        JsonData skillsinfo = JsonMapper.ToObject(Resources.Load<TextAsset>("SkillJ").text);
       
        


        foreach (JsonData skill in skillsinfo)
        {
            SkillInfo m_skill = new SkillInfo();
            m_skill.M_Id = (int)skill["id"];
            m_skill.M_Name = skill["name"] + "";
            m_skill.M_Icon = skill["icon"] + "";
            m_skill.M_Desc = skill["desc"] + "";
            m_skill.M_ApplyType = (ApplyType)System.Enum.Parse(typeof(ApplyType), skill["applyType"] + "");
            m_skill.M_ApplyProperty = (ApplyProperty)System.Enum.Parse(typeof(ApplyProperty), skill["applyProperty"] + "");
            m_skill.M_ApplicableRole = (ApplicableRole)System.Enum.Parse(typeof(ApplicableRole), skill["applicableRole"] + "");
            m_skill.M_ReleaseType = (ReleaseType)System.Enum.Parse(typeof(ReleaseType), skill["releaseType"] + "");
            m_skill.M_Mp = (int)skill["mp"];
            m_skill.M_Needlevel = (int)skill["needLevel"];
            m_skill.M_ColdTime = (int)skill["coldTime"];
            m_skill.M_MaxLevel = (int)skill["maxLevel"];
            m_skill.M_Distance = (int)skill["distance"];
            m_skill.M_EffectType = (EffectType)System.Enum.Parse(typeof(EffectType), skill["effectType"] + "");
            m_skill.M_ApplyValue = (int)skill["applyValue"];
            m_skill.M_LearnedLevel = 0;
            m_skill.M_EffectApplyValue = (int)skill["effectApplyValue"];//效果参数
            m_skill.M_UpPoint = (int)skill["upPoint"];//升级所需技能点数
            m_skill.M_AnimCondition = (int)skill["animCondition"];//技能动画触发条件
            m_skill.M_AnimaionTime = float.Parse(skill["animationTime"] + "");
            m_skill.M_SkillDataConfiger = skill["skillDataConfiger"] + "";

           
            if (skill["skillPosType"] != null) m_skill.M_SkillPosType = skill["skillPosType"] + "";
            if(skill["effectName"] != null ) m_skill.M_EffectName = skill["effectName"] + "";
            m_skill.M_SkillType = (SkillType)System.Enum.Parse(typeof(SkillType), skill["skillType"] + "");
 

            skillsDic.Add(m_skill.M_Id, m_skill);//添加到技能词典

        }
        

    }

    public Dictionary<int, SkillInfo> GetAllSkill()
    {
        if (skillsDic.Count != 0) { return skillsDic; }
        return null;
    }
     
    public SkillInfo GetSkillByID(int id)
    {
        return skillsDic.TryGetTool(id);
    }

    public Dictionary<int, SkillInfo> GetLearnedSkill()
    {
        if (learnedSkills == null) return null;
        if (learnedSkills.Count > 0) { return learnedSkills; }
        return null;
    }

  

    /// <summary>
    /// 学习技能
    /// </summary>
    /// <param name="id"></param>
    public void LearnSkill(int id)
    {
        print("Learn");//TODO
    }

    /// <summary>
    /// 减少技能级数
    /// </summary>
    public void ReduceSkill(int id)
    {
        print("Reduce");//TODO
    }

    /// <summary>
    /// 重置所有技能点
    /// </summary>
    public void ResetAllPoint()
    {

    }

    /// <summary> 
    /// 根据默认技能位置得到技能
    /// </summary>
    /// <param name="posType"></param>
    public SkillInfo GetSkillByPos(SkillPos.SkillPosType posType)
    {
        foreach (KeyValuePair<int, SkillInfo> kv in skillsDic)
        {
            if ((SkillPos.SkillPosType)System.Enum.Parse(typeof(SkillPos.SkillPosType), kv.Value.M_SkillPosType) == posType)
            {
                return kv.Value;
            }
        }
        return null;
    }


    public void SetActiveSkill(int id)
    {
        m_ActiveSkill = GetSkillByID(id);
        AttrController.Instance.GetComponent<ArcherSkill>().StartCoroutine("Skill",m_ActiveSkill);
        //AttrController.Instance.GetComponent<PlayerStateController>().SetNextState(StateID.Cast_Skill);
    }

    public SkillInfo GetActiveSkill()
    {
        return m_ActiveSkill;
    }


}






