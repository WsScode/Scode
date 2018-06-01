using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
using UnityEngine.EventSystems;

public class NPC : MonoBehaviour {

    /// <summary>
    /// 和npc的功能面板（如：商店面板 强化装备面板。。。） UIPanelType一致
    /// </summary>
    public enum NPCType
    {
        Normal,//都能发布任务 Noamal只有任务功能
        Shop,
        EquipMent,//强化装备的NPC
    }


    Gesture m_CurrentGesture;

    private Transform m_Camera;
    private Vector3 m_Offset;//相机相对于NPC的偏移值
    public Texture2D t2;

    private Task m_Task;//发布的任务 

    public NPCType m_NPCType;
    //继承此类需要初始化下面的字段值
    protected int m_ID;
    protected NPC.NPCType m_Type;
    protected NPCData m_Data;


    protected NPCInfo m_NPCInfo;

    
    public virtual void Awake()
    {
        m_NPCInfo = transform.parent.GetComponent<NPCInfo>();
        m_Camera = GameObject.Find("Cameras").transform.Find("Camera1");

        m_Offset.y = transform.GetComponent<CapsuleCollider>().bounds.size.y * 0.8f;
    }

    private void Start()
    {
        //m_Camera = GameObject.Find("Cameras").transform.Find("Camera1");

        //m_Offset.y = transform.GetComponent<CapsuleCollider>().bounds.size.y * 0.8f;
        //Resources.Load<Texture2D>("Sprites/Items/steel_gloves")
        //Cursor.SetCursor(LoadSprite.LoadTexture2DByName("Sprites/CommonUI/RestaurantAtlas", "RestaurantAtlas_0"), new Vector2(0,0),CursorMode.ForceSoftware);
        ////LoadSprite.LoadTexture2DByName("Sprites/CommonUI/RestaurantAtlas", "")
    }



    private void Update()
    {
        MyEvent evt =  new MyEvent(MyEvent.MyEventType.MessageBox);
        evt.m_StringPara = "哟哟切克闹";
        if (Input.GetMouseButtonDown(0)) { MyEventSystem.m_MyEventSystem.PushEvent(evt); }
        
    }

    public virtual void OnPointerDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) {return; } 
        m_Data = GetData(m_ID);
        ShowguidePanel();       
    }


    /// <summary>
    /// 显示NPC的面板
    /// </summary>
    public void ShowguidePanel()
    {
        //设置相机位置
        m_Camera.position = transform.localPosition + m_Offset + transform.forward * 1.5f;
        m_Camera.rotation = transform.rotation;
        m_Camera.Rotate(Vector3.up, m_Camera.rotation.y + 180);
        m_Camera.gameObject.SetActive(true);
        //显示面板
        UIController._instance.ShowPanel(UIPanelType.NPC);
        //设置面板信息
        NPCPanel npcPanel = UIController._instance.GetPanelByType(UIPanelType.NPC).GetComponent<BasePanel>() as NPCPanel;
        npcPanel.SetPanelInfo(m_Data);
    }


    public NPCData GetData(int id)
    {
        return m_NPCInfo.GetNPCInfoByID(id);
    }


}
