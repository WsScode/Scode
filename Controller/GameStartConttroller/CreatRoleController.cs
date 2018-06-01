using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CreatRoleController : MonoBehaviour {

   
    private Camera m_Camera;
    private bool m_Change = false;
    private Vector3 m_Targetpos;//相机结束位置
    private string m_TargetRole;

    private CreateRoleUI m_CRUI;
    private GameObject[] m_RolesGO ;//保存所有的角色模型


    private void Start()
    {
        m_Camera = Camera.main;
        m_Targetpos = new Vector3(-113.342f, 20.065f, 39.693f);
        m_CRUI = GameObject.Find("Canvas").GetComponent<CreateRoleUI>();
        m_RolesGO = GameObject.FindGameObjectsWithTag(Tags.Player);
        for (int i = 0; i < m_RolesGO.Length; i++)
        {
            m_RolesGO[i].SetActive(false);
        }
    }

    

    private void Update()
    {
        if (Vector3.Distance(m_Targetpos, m_Camera.transform.position) > 0.1f)
        {
            m_Camera.transform.position = Vector3.Lerp(m_Camera.transform.position, m_Targetpos, Time.deltaTime * 3); 
        }

    }


    public void ChangeRole(string roleName)
    {

        foreach (GameObject go in m_RolesGO)
        {
            if (go.name == roleName)
            {
                ShowRole(go.name);
            }
        }
        
    }


    public void ShowRole(string g_name)
    {
        foreach (GameObject go in m_RolesGO)
        {
            if (go.name == g_name)
            {
                go.SetActive(true);
                if (!go.name.Contains("Fe"))
                {
                    go.GetComponent<Animator>().SetBool("IsMale",true);
                }
            }
            else
            {
                go.SetActive(false);
            }
        }

        if (g_name == m_TargetRole) return;
        if (g_name == RoleType.ArcherFemale.ToString() || g_name == RoleType.ArcherMale.ToString())
        {
            m_Targetpos = new Vector3(-113.342f, 20.065f, 39.693f);
        }
        else if (g_name == RoleType.MageFemale.ToString() || g_name == RoleType.MageMale.ToString())
        {
            m_Targetpos = new Vector3(-124.46f, 20.065f, 37.45f);
        }
        else if (g_name == RoleType.WarriorFemale.ToString() || g_name == RoleType.WarriorMale.ToString())
        {
            m_Targetpos = new Vector3(-102.48f, 20.065f, 35.3f);
        }
    }

    /// <summary>
    /// 确定角色 
    /// 生成 playinfo配置信息并保存 
    /// 下一个场景 加载到玩家角色身上
    /// </summary>
    public void ConfirmRole()
    {

    }






}
