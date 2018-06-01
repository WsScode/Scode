using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// 物品基类 定义物品属性 不用继承monobehaviour
/// </summary>

public class Item {

    public int m_ID { get; set; }
    public string m_Name { get; set; }
    public ItemType m_ItemType { get; set; }
    public Quality m_Quality { get; set; }
    public string m_Descrption { get; set; }
    public int m_Capacity{ get; set; }
    public int m_BuyPrice{ get; set; }
    public int m_SellPrice{ get; set; }
    public string m_Sprite { get; set; }

    //公共属性用于构造
    public Item(int id,string name,ItemType type, Quality quality,string descrption,int capacity,int buyPrice,int sellPrice,string spritePath) {
        this.m_ID = id;
        this.m_Name = name;
        this.m_ItemType = type;
        this.m_Quality = quality;
        this.m_Descrption = descrption;
        this.m_Capacity = capacity;
        this.m_BuyPrice = buyPrice;
        this.m_SellPrice = sellPrice;
        this.m_Sprite = spritePath;
    }

    public enum Quality
    {
        Common, Unmmon, Rare, Epic, Leggendary, Artifact
    }


    public virtual string  GetItemDesc() {
        string color = "white";
        switch (m_Quality)
        {
            case Quality.Common:
                break;
            case Quality.Artifact:
                color = "blue";
                break;
            case Quality.Leggendary:
                color = "#FF45FFFF";//紫色
                break;
            case Quality.Epic:
                color = "yellow";
                break;
        }
        return string.Format("<color={0}>{1}</color>\n出售价格:{2}\n",color,m_Name,m_SellPrice);
    }


}





public enum ItemType {
    Consumable,//0
    Equipment,//1
    Weapon,//2
    Materials,//3

}


