using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item {

    public int m_Hp { get; set; }
    public int m_Mp { get; set; }

    public Consumable(int id, string name, ItemType type, Quality quality, string descrption, int capacity, int buyPrice, int sellPrice,string spritePath
        , int hp,int mp):base(id, name, type, quality, descrption, capacity, buyPrice, sellPrice, spritePath)
    {
        this.m_Hp = hp;
        this.m_Mp = mp;
    }

    public override string GetItemDesc()
    {
        string s = base.GetItemDesc();
        s += string.Format("HP +{0}\nMP +{1}\n{2}",m_Hp,m_Mp,m_Descrption);
        return s;
    }
}
