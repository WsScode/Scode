using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// 由ShopPanel管理
/// </summary>
public class ShopGrid : Grid {



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
        if (grid_item == null) return;
        //点击就更新购买的相关UI
        transform.parent.parent.GetComponent<ShopPanel>().UpdateShowBuyUI(grid_item);

    }
}
