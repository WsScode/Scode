using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkillLayoutConfiger : MonoBehaviour {

    [Tooltip("粒子")]
    public GameObject m_Particle;

    [Tooltip("粒子生成延迟时间")]
    public float m_DelayTime = 0;

    [Tooltip("粒子位置")]
    public Vector3 m_Position;

    [Tooltip("绑定的骨骼名称")]
    public string m_BoneName = "";

}
