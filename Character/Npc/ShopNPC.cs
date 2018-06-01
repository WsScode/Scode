using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopNPC : NPC {
    private Shop m_Shop;


    public override void Awake()
    {
        base.Awake();
        m_ID = 1;
        m_Shop = GetComponent<Shop>();
        print(Shop.Instance);

    }

    private void Start()
    {
        m_ID = 1;
        m_Type = NPCType.Shop;
        //m_Shop = gameObject.AddComponent<Shop>();
        //m_Data = GetData(m_ID);
        //Debug.Log(m_Data.M_Name);
    }

    public override void OnPointerDown()
    {
        base.OnPointerDown();
    }

    /// <summary>
    /// 设置商店的物品
    /// </summary>
    public void SetShopItem(int[] itemsID)
    {
        if (itemsID.Length == 0) return;
        m_Shop.SetShopItem(itemsID);

    }


}
