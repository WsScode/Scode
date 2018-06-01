using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : Item {


    public Materials(int id, string name, ItemType type, Quality quality, string descrption, int capacity, int buyPrice, int sellPrice
       ,string spritePath ): base(id, name, type, quality, descrption, capacity, buyPrice, sellPrice,spritePath)
    {

    }


    public override string GetItemDesc()
    {
        string s = base.GetItemDesc();
        s += string.Format("{0}",m_Descrption);
        return s;
    }
}
