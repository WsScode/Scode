using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于大量重复生成的GameObject 例如 平A  短时间简单技能
/// </summary>
public class BulletPool : MonoBehaviour {
    public static BulletPool Instance;


    private List<GameObject> m_BulletPool;//对象池
    public int m_PoolCount = 10;
    private GameObject m_Bullet;
    public string m_GameObjectPath { get; set; }//需要实例化GameObject的地址
    public bool m_IsLockCount = false;//是否锁定对象池大小

    private int m_Currentndex;

    private Transform m_Parent;

    private void Awake()
    {
        InitArrowPool();
        Instance = this;
    }
    private void Start()
    {
        //将实例化的特效 保存到玩家模型下  方便特效跟随
        //m_Parent = GameObject.FindGameObjectWithTag(Tags.Player).transform.Find("Pools") ;
        m_Parent = transform;
    }

    /// <summary>
    /// 初始化箭矢的对象池 弓箭手普攻使用
    /// </summary>
    public void InitArrowPool()
    {
        m_BulletPool = new List<GameObject>(m_PoolCount);
        for (int i = 0; i < m_PoolCount; i++)
        {
            m_Bullet = Instantiate(Resources.Load<GameObject>("SkillEffect/Arrow"), m_Parent);
            m_Bullet.AddComponent<Bullet>();
            //m_Bullet.AddComponent<AutoDestroy>();
            m_Bullet.SetActive(false);
            m_BulletPool.Add(m_Bullet);


        }

    }


    public GameObject GetArrow()
    {
        for (int i = 0;i < m_PoolCount; i++)
        {
            if (!m_BulletPool[i].activeInHierarchy)
            {
                return m_BulletPool[i];
            }
        }
        if (!m_IsLockCount)
        {
            m_Bullet = Instantiate(Resources.Load<GameObject>("SkillEffect/Arrow"), m_Parent);
            m_Bullet.SetActive(false);
            m_BulletPool.Add(m_Bullet); 
            return m_Bullet;
        }
        
        return null;
    }





}


