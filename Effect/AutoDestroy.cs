using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 用于物品的自己销毁或者隐藏
/// </summary>
public class AutoDestroy : MonoBehaviour {

	private float timer = 0.3f;
    private float m_Hidetime = 0;
    private float m_Hidetimer = 3;
    

    private void Awake()
    {
        m_Hidetime = 0;
        if (gameObject.tag != Tags.NoDestory)
        {
            Destroy(this.gameObject, timer);
        }
       


    }

    private  void Update()
    {

        if (gameObject.activeInHierarchy)
        {
           
            m_Hidetime += Time.deltaTime;

        }

        if (m_Hidetime >= m_Hidetimer)
        {
            m_Hidetime = 0;
            gameObject.SetActive(false);
        }
    }

}
