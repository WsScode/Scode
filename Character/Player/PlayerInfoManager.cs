using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 1、管理战斗力的计算和显示 
/// 2、属性的转换
/// </summary>
public class PlayerInfoManager : MonoBehaviour {
    private PlayerInfo m_PlayerInfo;

    private void Start()
    {
        m_PlayerInfo = PlayerInfo.Instance;
    }

    /// <summary>
    /// 返回根据当前属性计算的战斗力 （自定）
    /// 四维 精E（MP 魔防） 力S（物攻） 智I（魔攻） 敏A（闪避）
    /// </summary>
    /// <returns></returns>
    public int calculatePower() {
       return  (int)(m_PlayerInfo.M_Hp * 0.2f
        + m_PlayerInfo.M_Mp * 0.05f
        + m_PlayerInfo.M_Agility * 0.25f
        + m_PlayerInfo.M_Attack * 0.4f
        + m_PlayerInfo.M_Stamina * 0.3f
        + m_PlayerInfo.M_Intellect * 0.1f //TODO 还需要判断当前人物的攻击属性
        + m_PlayerInfo.M_Level * 500
        + m_PlayerInfo.M_Strength * 0.4f
        + m_PlayerInfo.M_MoveSpeed * 0.2f);
    }

    public void UpdateFightingPower() {

    }

    public  void  UpdateShow() {

    }

    /// <summary>
    /// 面板打开时 读取属性更新面板
    /// </summary>
    public void InitInfo() {

    }
}
