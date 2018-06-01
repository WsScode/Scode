using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoleUI : MonoBehaviour {
     
    //顺序和 枚举RoleType对应
    private Button m_MageFemale;//0 女法
    private Button m_Magemale;//1 男法
    private Button m_ArcherFemale;//2  女弓
    private Button m_Archermale;//3  男弓
    private Button m_WarriorFemale;//4 女剑客
    private Button m_Warriormale;//5  男剑客

    private Text m_RoleInfo;

    private InputField m_PlayerName;
    private Button m_ConfirmBtn;

    private CreatRoleController m_CreateRoleCtrl;

    private Dictionary<int, RoleType> m_Role = new Dictionary<int, RoleType>();

    private void Awake()
    {
        m_CreateRoleCtrl = GameObject.Find("ControllerHandler").GetComponent<CreatRoleController>();
        InitUI();
    }

    private void InitUI()
    {
        int i = 0;
        foreach (RoleType rt in Enum.GetValues(typeof(RoleType)))
        {
            m_Role.Add(i, rt);
            i++;
        }

        Transform go = transform.Find("RoleChoice").GetComponent<Transform>();
        m_MageFemale = go.Find("MageFemale").GetComponent<Button>();
        m_MageFemale.onClick.AddListener(delegate () { OnClick(m_MageFemale.name); });

        m_Magemale = go.Find("MageMale").GetComponent<Button>();
        m_Magemale.onClick.AddListener(delegate () { OnClick(m_Magemale.name); });

        m_ArcherFemale = go.Find("ArcherFemale").GetComponent<Button>();
        m_ArcherFemale.onClick.AddListener(delegate () { OnClick(m_ArcherFemale.name); });

        m_Archermale = go.Find("ArcherMale").GetComponent<Button>();
        m_Archermale.onClick.AddListener(delegate () { OnClick(m_Archermale.name); });

        m_WarriorFemale = go.Find("WarriorFemale").GetComponent<Button>();
        m_WarriorFemale.onClick.AddListener(delegate () { OnClick(m_WarriorFemale.name); });

        m_Warriormale = go.Find("WarriorMale").GetComponent<Button>();
        m_Warriormale.onClick.AddListener(delegate () { OnClick(m_Warriormale.name); });

        m_RoleInfo = transform.Find("RoleInfo").GetComponent<Text>();
        m_PlayerName = transform.Find("PlayerNameInput").GetComponent<InputField>();
        m_ConfirmBtn = transform.Find("ConfirmButton").GetComponent<Button>();
        m_ConfirmBtn.onClick.AddListener(m_CreateRoleCtrl.ConfirmRole);


    }


    private void Update()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public void OnClick(string roleName)
    {
        if (roleName == null) return;
        m_CreateRoleCtrl.ChangeRole(roleName);
    }

    public Dictionary<int, RoleType> GetRoles()
    {
        return m_Role;

    }

}
