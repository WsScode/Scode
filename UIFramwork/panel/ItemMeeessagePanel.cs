using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMeeessagePanel : BasePanel {

    public  override void Awake()
    {
        base.Awake();
       
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
  

}
