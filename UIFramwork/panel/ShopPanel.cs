using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopPanel : BasePanel {

    private Button m_EquipmentButton;
    private Button m_MaterialsButton;
    private Button m_ConsumablesButton;
    private Text m_ItemName;
    private InputField m_ItemCount;
    private Text m_TotalPrice;
    private Button m_Buy;//购买确定键

    private Shop m_Shop;

    private Item m_Item;



    public override void Awake()
    {
        base.Awake();
        m_showUIType = ShowUIType.Normal;//TODO 应该为ShowUIType.HideOther
        m_EquipmentButton = transform.Find("EquipmentButton").GetComponent<Button>();
        m_MaterialsButton = transform.Find("MaterialsButton").GetComponent<Button>();
        m_ConsumablesButton = transform.Find("ConsumablesButton").GetComponent<Button>();
        m_ItemName = transform.Find("ItemName").GetComponent<Text>();
        m_ItemCount = transform.Find("ItemCount").GetComponent<InputField>();
        m_ItemCount.onValueChanged.AddListener(delegate { UpdateTotalPrice(); });
        m_TotalPrice = transform.Find("TotalPrice").GetComponent<Text>();
        m_Buy = transform.Find("Buy").GetComponent<Button>();

        m_Shop = GetComponent<Shop>();
        m_Buy.onClick.AddListener(delegate() { Buy(); });
    }



    public override void OnEnter(UIController uIController)
    {
        base.OnEnter(uIController);
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;//检测点击
    }




    public override void OnPause()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;//检测点击
    }

    /// <summary>
    /// 更新购买信息UI
    /// </summary>
    /// <param name="item"></param>
    public void UpdateShowBuyUI(Item item)
    {
        
        m_Item = item;
        m_ItemName.text = item.m_Name + "";
        m_ItemCount.text = "1";
        m_TotalPrice.text = item.m_BuyPrice + "";
    }

    /// <summary>
    /// 确定购买
    /// </summary>
    public void Buy()
    {
        int count;
        int.TryParse(m_ItemCount.text,out count);
        m_Shop.BuyItem(m_Item, count);
    }


    void UpdateTotalPrice()
    {
        
    }


}
