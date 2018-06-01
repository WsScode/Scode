using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class NPCData
{
    public int M_ID { get; set; }
    public string M_Name { get; set; }
    public NPC.NPCType M_NpcType { get; set; }
    public string M_TypeText { get; set; }//对应的功能名称 （商店、锻造等）
    public string M_Dialogue { get; set; }
    public int[] M_ItemIDs { get; set; }

}


/// <summary>
/// npc信息管理
/// </summary>
public class NPCInfo : MonoBehaviour {

    private Dictionary<int, NPCData> m_NPCs;

    private void Awake()
    {
        ParseInfo();
    }

    void ParseInfo()
    {
        string jd = Resources.Load<TextAsset>("NPCInfo").text;
        JsonData infos = JsonMapper.ToObject(jd);

        foreach (JsonData info in infos) 
        {
            NPCData npc = new NPCData();
            npc.M_ID = (int)info["id"];
            npc.M_Name = info["name"] + "";
            npc.M_NpcType = (NPC.NPCType)System.Enum.Parse(typeof(NPC.NPCType),info["type"] + "");
            npc.M_TypeText = info["typeText"] + "";
            npc.M_Dialogue = info["baseDialogue"] + "";
            //npc.M_ItemIDs = 
            string[] ss = (info["itemIDs"] + "").Split(',');
            //npc持有的物品id 
            npc.M_ItemIDs = new int[ss.Length];
            for (int i = 0; i < ss.Length; i++)
            {
                if (!int.TryParse(ss[i], out npc.M_ItemIDs[i])){continue;}
            }
            if (m_NPCs == null) m_NPCs = new Dictionary<int, NPCData>();
            m_NPCs.Add(npc.M_ID,npc);
        }

    }

    public NPCData GetNPCInfoByID(int id)
    {
        NPCData npc = m_NPCs.TryGetTool (id);
        return npc;
    }


	
}
