using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item {

    public int m_Strength{get;set;}
    public int m_Intellct{get;set;}
    public int m_Agility{get;set;}
    public int m_Stamina{get;set; }
    public int m_Hp{get;set; }
    public int m_Mp{get;set; }
    public EquipmentType m_EquipmentType {get;set; }
    public bool m_IsDressed { get; set; }


    public Equipment(int id, string name, ItemType type, Quality quality, string descrption, int capacity, int buyPrice, int sellPrice,string spritePath
        , int strength,int intellect,int agility,int stamina,int hp,int mp,EquipmentType equipmentType) : base(id, name, type, quality, descrption, capacity, buyPrice, sellPrice, spritePath)
    {
        this.m_Strength = strength;//力量
        this.m_Intellct = intellect;//智力
        this.m_Agility = agility;//敏捷
        this.m_Stamina = stamina;//精力
        this.m_Hp = hp;
        this.m_Mp = mp;
        m_EquipmentType = equipmentType;
    }


    public enum EquipmentType {
        None,
        Weapon,
        Head,
        Neck,//脖子
        Chest,//胸部
        Ring,
        Leg,//大腿
        Bracer,//护腕.
        Boots,//靴子.
        Shoulder,//肩膀
        Belt,//腰带.
        OffHand//副手
    }


    public override string GetItemDesc()
    {
        string s = base.GetItemDesc();
        s += string.Format("HP +{0}\nMP +{1}\n力量 +{2}\n 智力 +{3}\n敏捷 +{4}\n精力 +{5}\n{6}", m_Hp, m_Mp,m_Strength,m_Intellct,m_Agility,m_Stamina ,m_Descrption);
        return s;
    }





}

