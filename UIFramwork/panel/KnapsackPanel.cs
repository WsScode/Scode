using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnapsackPanel : BasePanel
{

    private Button m_ClearUpButton;//整理
    private Button m_SellButton;//出售
    private Text m_CoinText;

    private Inventory m_Inventory;


    public override void Awake()
    {
        base.Awake();
        showUIType = ShowUIType.Normal;

        m_Inventory = GetComponent<Inventory>();
        m_ClearUpButton = transform.Find("ClearUpButton").GetComponent<Button>();
        m_SellButton = transform.Find("SellButton").GetComponent<Button>();
        m_CoinText = transform.Find("Box/Coin").GetComponent<Text>();


    }

    private void Start()
    {
        //m_CoinText.text = m_Inventory.M_COIN + "";//初始化金币数量UI
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


    public void UpdateShowCoin(int count)
    {
        m_CoinText.text = count + "";
    }


   


}
