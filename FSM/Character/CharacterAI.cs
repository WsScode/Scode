using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 管理人形敌人和人形NPC状态
/// </summary>
public class CharacterAI : MonoBehaviour {

    public FSMSystem m_FsmSystem;//控制状态添加和切换


    private void Awake()
    {
        m_FsmSystem = new FSMSystem();

    }



}
