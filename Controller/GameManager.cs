using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class GameManager : MonoBehaviour {
    public static GameManager GM;
    Ray m_Ray;
    RaycastHit m_Hitinfo;

    public void Awake()
    {
        GM = this;
        DontDestroyOnLoad(this);
    }


    public void SetCursorNormal()
    {
        Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
    }



    private void Update()
    {
        m_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(m_Ray, out m_Hitinfo))
        {
            if (m_Hitinfo.collider.tag == Tags.NPC)
            {
                Cursor.SetCursor(Resources.Load<Texture2D>("Sprites/Items/steel_gloves"), new Vector2(0, 0), CursorMode.Auto);
                if (Input.GetMouseButtonDown(0))
                {
                    //改变相机位置 显示选择界面（选择任务 或者npc的功能（商店 ，锻造等））
                    m_Hitinfo.collider.gameObject.GetComponent<NPC>().OnPointerDown();
                }
            }
            else
            {
                SetCursorNormal();
            }
             
        }
    }
}
