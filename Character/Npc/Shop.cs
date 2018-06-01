using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 出售物品的NPC 商品管理
/// 功能继承自玩家的背包系统 是否会造成耦合
/// 还是将功能单独列出来  包括玩家 NPC的背包 都继承 
/// </summary>
public class Shop : Inventory {


    public static Shop m_Shop;

    protected override void Awake()
    {
        m_Shop = this;
    }

    protected override void Start()
    {
        //test
        //int[] ids = new int[] { 1, 3, 5, 7, 9 };
        //SetShopItem(ids);
        //Inventory.Instance.PutItemByID(1);
    }

    /// <summary>
    /// 设置商店的物品
    /// </summary>
    /// <param name="IDs"></param>
    public  void SetShopItem(int[] IDs)
    {
        foreach (int id in IDs)
        {
            PutItemByID(id);
        }

    }




    /// <summary>
    /// 购买东西
    /// 1 先保存商品 判断是都一次购买的数量大于背包格子数
    /// 2 扣除金币 
    /// </summary>
    public void BuyItem(Item item, int count)
    {
        bool b1 = GameObject.FindGameObjectWithTag(Tags.Knapsack).GetComponent<Inventory>().PutItemByID(item.m_ID, count);
        bool b2 = ReduceCoin(item.m_BuyPrice * count);
        if (b1 && b2)
        {
            //提示购买成功的消息
            UIController._instance.ShowPanel(UIPanelType.MessageBox);
        }

        //if (!ReduceCoin(item.m_BuyPrice * count) || item == null) return;
        //print("xxxxxxxxxx");
        //ReduceCoin(item.m_BuyPrice * count);

        
    }


}
