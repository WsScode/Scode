using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerEquipmentGrid : Grid {
    public static PlayerEquipmentGrid Instance;

    public Equipment.EquipmentType m_EquipmentType;
    public Weapon.WeaponType m_WeaponType; 
    //public Equipment.EquipmentType M_EquipmentType { get { return m_equipmentType; } }//当前格子的装备属性
    //public Weapon.WeaponType M_WeaponType { get { return m_WeaponType; } }//武器格子属性

    private Image e_itemImgae;
    private PlayerInfoPanel m_PlayerInfoPanel;
    private Item m_DressedItem;

    public override void Awake() 
    {
        
        Instance = this;
        e_itemImgae = GetComponent<Image>();
    }

    private void Start()
    {
        isFull = ISGridFull();
        isEmpty = ISGridEmpty();
        m_PlayerInfoPanel = transform.parent.parent.GetComponent<PlayerInfoPanel>();
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
       
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

    }
    public override void OnPointerDown(PointerEventData eventData)
    {
       
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            //右键脱下装备
            if (grid_item == null){return;}
            if (Inventory.Instance.PutItemByID(grid_item.m_ID))
            {
                m_PlayerInfoPanel.PutOff(grid_item);
                ResetGrid();
            }
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory.Instance.isPicked)
            {
                if (IsRightGrid(Inventory.Instance.pickedItem.picked_Item))
                {
                    if (grid_item == null)
                    {
                        m_PlayerInfoPanel.PutOn(Inventory.Instance.pickedItem.picked_Item, this);
                        //StoreItem(Inventory.Instance.pickedItem.picked_Item);//格子预制体搞错了 此方法不能用
                        SetItem(Inventory.Instance.pickedItem.picked_Item);
                        Inventory.Instance.pickedItem.ResetItem();
                    }
                    else
                    {
                        m_DressedItem = grid_item;
                        m_PlayerInfoPanel.PutOn(Inventory.Instance.pickedItem.picked_Item, this);
                        SetItem(Inventory.Instance.pickedItem.picked_Item);
                        Inventory.Instance.pickedItem.ResetItem();
                    }
                }
            }
            else
            {
                if (grid_item != null)
                {
                    m_PlayerInfoPanel.PutOff(grid_item);
                    Inventory.Instance.pickedItem.SetItem(grid_item,current_count);
                    ResetGrid();
                }
            }
        }
    }


    /// <summary>
    /// 判断当前装备格子是否正确
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool IsRightGrid(Item item) {
        
        if ( (item is Equipment && ((Equipment)item).m_EquipmentType == m_EquipmentType) ||
            (item is Weapon && ((Weapon)item).m_WeaponType == m_WeaponType) )
        {
            return true;
        }
        return false;
    }

    public void SetItem(Item item) {
        grid_item = item;
        e_itemImgae.sprite = Resources.Load<Sprite>(item.m_Sprite);
        current_count = 1;//装备默认每个格子只能放一个
    }

    public override  void ResetGrid()
    {
        e_itemImgae.sprite = Resources.Load<Sprite>("Sprites/Items/bg_道具");
        current_count = 0;
        grid_item = null;
        isEmpty = true;
    }
}
