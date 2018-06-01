using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class ItemManager : MonoBehaviour {

	public List<Item> M_AllItem { get; private set; }

    public static ItemManager Instance;

    

    private void Awake()
    {
        ParseItemInfo();
        Instance = this;
        
    }

    /// <summary>
    /// 解析item的JSON文件
    /// </summary>
    private void ParseItemInfo()
    {
        TextAsset ta = Resources.Load<TextAsset>("ItemJ");
        JsonData items = JsonMapper.ToObject(ta.text);

        M_AllItem = new List<Item>();
        Item m_item = null;//保存物品
        if (items == null) { Debug.Log("没有解析到物品"); return; }
        foreach (JsonData item in items)
        {
            int id = (int)item["id"];
            string name = item["name"].ToString();
            ItemType itemType = (ItemType)System.Enum.Parse(typeof(ItemType), item["type"].ToString());
            Item.Quality quality = (Item.Quality)System.Enum.Parse(typeof(Item.Quality), item["quality"].ToString());

            string description = item["description"].ToString();
            int capacity = (int)item["capacity"];
            int buyprice = (int)item["buyprice"];
            int sellprice = (int)item["sellprice"];
            string spritePath = item["sprite"].ToString();//图片地址
            int hp, mp = 0;//hp和mp为Consumable和Equipment共有属性
            //根据typee来区分不同类型装备的特有属性
            switch (itemType)
            {
                case ItemType.Consumable:
                    hp = (int)item["hp"];
                    mp = (int)item["mp"];
                    m_item = new Consumable(id, name, itemType, quality, description, capacity, buyprice, sellprice, spritePath, hp, mp);
                    break;
                case ItemType.Equipment:
                    int strength = (int)item["strength"];//力量
                    int intellect = (int)item["intellect"];//智力
                    int agility = (int)item["agility"];//敏捷
                    int stamina = (int)item["stamina"];//精力
                    hp = (int)item["hp"];
                    mp = (int)item["mp"];
                    Equipment.EquipmentType equipmentType = (Equipment.EquipmentType)System.Enum.Parse(typeof(Equipment.EquipmentType), item["EquipmentType"].ToString());
                    m_item = new Equipment(id, name, itemType, quality, description, capacity, buyprice, sellprice, spritePath, strength, intellect, agility, stamina, mp, hp, equipmentType);
                    break;
                case ItemType.Weapon:
                    Weapon.WeaponType weaponType = (Weapon.WeaponType)System.Enum.Parse(typeof(Weapon.WeaponType), item["weaponType"].ToString());
                    int damage = (int)item["damage"];
                    string weaponModel = item["weaponModel"] + "";//武器模型位置
                    int attackDistance = (int)item["attackDistance"];
                    //Debug.Log(weaponModel);
                    m_item = new Weapon(id, name, itemType, quality, description, capacity, buyprice, sellprice, spritePath, damage, weaponModel, attackDistance, weaponType);
                    break;
                case ItemType.Materials:
                    m_item = new Materials(id, name, itemType, quality, description, capacity, buyprice, sellprice, spritePath);
                    break;
            }
            M_AllItem.Add(m_item);

        }


    }


}
