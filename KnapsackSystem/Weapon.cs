using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {
    public int m_Damage { get; set; }
    public WeaponType m_WeaponType { get; set; }
    public string m_Model{ get; set; }
    public float m_AttackDistance{ get; set; }

    public Weapon(int id, string name, ItemType type, Quality quality, string descrption, int capacity, int buyPrice, int sellPrice, string spritePath
        , int damage,string model,float attackDistance,WeaponType weaponType) : base(id, name, type, quality, descrption, capacity, buyPrice, sellPrice, spritePath)
    {
        this.m_Damage = damage;
        m_Model = model;
        m_AttackDistance = attackDistance;
        m_WeaponType = weaponType;
    }


    public enum WeaponType {
        None,
        OffHand,
        MainHand,
    }

    public enum MainHandWeaponType {
        None,
        RangedMagicweapon,//远程魔法武器
        Shortrangedweapon,//近战武器（默认为所有都是物理攻击）
        RangePhysicalWeapon,//远程物理攻击
    }




}
