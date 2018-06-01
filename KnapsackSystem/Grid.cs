using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 管理格子本身的装备图片显示和数字显示 以及拾取等操作
/// </summary>
public class Grid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Image itemImage;
    private Text itemCountText;
    private bool isDressed = false;
    //private Image border;


    [HideInInspector] public Item grid_item { get; set; }//表示这个格子的物品
    [HideInInspector] public int max_count = 999;//一个格子最大物品数量
    [HideInInspector] protected int current_count = 0;//当前格子里面的物品数量
    [HideInInspector] public int Current_Count { get { return current_count; } private set { } }
    [HideInInspector] public bool isFull = false;
    [HideInInspector] public bool isEmpty = true;
    [HideInInspector] public bool IsDressed { get { return isDressed; } set { isDressed = value; } }


    public virtual void Awake()
    {
        itemImage = transform.Find("Item").GetComponent<Image>();
        itemCountText = transform.Find("ItemCount").GetComponent<Text>();
        isFull = ISGridFull();
        isEmpty = ISGridEmpty();
    }

    /// <summary>
    /// 将物品放进格子并设置UI 其余逻辑不在此方法
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public void StoreItem(Item item,int count = 1)
    {
        if (grid_item == null)
        {
            grid_item = item;
            isEmpty = false;
            current_count = count;
            itemImage.sprite = Resources.Load<Sprite>(item.m_Sprite);

        }
        else
        {
            current_count += count;
        }
        isFull = ISGridFull();
        if (current_count == 1)
        {
            //Debug.Log("这里");
            itemCountText.text = "";//数量为1时不显示
        }
        else
        {
            itemCountText.text = current_count + "";
        }
    }

    public void AddSameItem(int count) {
        current_count += count;
        itemCountText.text = current_count + "";
    }





    /// <summary>
    /// 判断格子是否已满
    /// </summary>
    /// <returns></returns>
    public bool ISGridFull() {
        //if (grid_item == null) { return false; }
        if (grid_item != null && current_count == grid_item.m_Capacity) { return true; }
        return false;
    }

    /// <summary>
    /// 是否是空格子
    /// </summary>
    /// <returns></returns>
    public bool ISGridEmpty() {
        if (grid_item == null) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 鼠标退出
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Inventory.Instance.tooltip.Hide();
        if (gameObject.name != "PickedItemPanel") transform.Find("Border").GetComponent<Image>().gameObject.SetActive(false);
        //Debug.Log("Exit");
    }

    /// <summary>
    /// 鼠标进入
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.name != "PickedItemPanel") transform.Find("Border").GetComponent<Image>().gameObject.SetActive(true);
        if (Inventory.Instance.isPicked == false && grid_item != null) {
            //string s = string.Format("属性:\n{0}:+1111\n{1}:+5555","力量","智力");
            //TODO 得到需要显示的装备信息 放到Item类处理
            Inventory.Instance.ShowTooltip(grid_item.GetItemDesc());
            
        }
    }

    /// <summary>
    /// 鼠标点击 拖拽物品
    /// 按照数量拿取东西TODO
    /// </summary>IPointerDownHandler
    /// <param name="eventData"></param>
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        //点击右键进行装备的穿戴操作
        if (eventData.button == PointerEventData.InputButton.Right) {
            //显示人物面板
            UIController._instance.ShowPanel(UIPanelType.PlayerInfo);
            PlayerInfoPanel playerInfoPanel =  UIController._instance.GetPanelGameObject("PlayerInfoPanel(Clone)").GetComponent<PlayerInfoPanel>();
            //playerInfoPanel.GetPlayerEquipmentGridList();
            if (grid_item != null && (grid_item is Equipment|| grid_item is Weapon)) {

                //先找到合适的格子
                //1、装备格子为空 穿上装备 清空当前个格子 
                //2、装备格子不为空 先把已经穿上的装备脱下（和当前格子互换） 再穿上装备 
                playerInfoPanel.PutOn(grid_item,this);
            }
        }

        if (eventData.button == PointerEventData.InputButton.Left) {
            
            if (Inventory.Instance.isPicked)
            {//鼠标上已经有物品 
                PickedItem picked_Item = Inventory.Instance.pickedItem;
                Item picked_item = picked_Item.picked_Item;
                int picked_count = picked_Item.PickedCount;
                if (grid_item != null)
                {
                    //默认全部拿起 全部交换 
                    Inventory.Instance.SetPickedItem(grid_item, current_count);
                    grid_item = null; 
                    StoreItem(picked_item, picked_count);
                }
                else
                {
                    StoreItem(picked_item, picked_count);
                    Inventory.Instance.isPicked = false;
                    Inventory.Instance.pickedItem.Hide();
                }
            }
            else
            {//鼠标上没有物品 
                if (grid_item != null)
                {
                    //Item picked_Item = Inventory.Instance.pickedItem.picked_Item;
                    Inventory.Instance.SetPickedItem(grid_item, current_count);
                    ResetGrid();
                }
            }
        }
    }

    /// <summary>
    /// 清空当前格子
    /// </summary>
    public virtual void ResetGrid() {
        itemImage.sprite = Resources.Load<Sprite>("Sprites/Items/bg_道具") ;
        itemCountText.text = "";
        current_count = 0;
        grid_item = null;
        isEmpty = true;
    }



    //public void OnDragStart() {
    //    Debug.Log("start");
    //}
    //public void OnDrag()
    //{
    //    Debug.Log("On");
    //}
    //public void OnDragEnd()
    //{
    //    Debug.Log("End");
    //}
}

