using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 被拿起来的物品
/// </summary>
public class PickedItem : MonoBehaviour
{

    private Image itemImage;
    [HideInInspector] public Item picked_Item { get; private set; }
    [HideInInspector] public int PickedCount { get; private set; }

    public void Awake()
    {
        itemImage = transform.Find("Item").GetComponent<Image>();
    }




    public void SetItem(Item item, int count) {
        gameObject.SetActive(true);
        picked_Item = item;
        PickedCount = count;
        Inventory.Instance.isPicked = true;
        itemImage.sprite = Resources.Load<Sprite>(item.m_Sprite);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 将抓取的物品设为空
    /// </summary>
    public void ResetItem() {
        Inventory.Instance.isPicked = false;
        picked_Item = null;
        PickedCount = 0;
        Hide();
    }

    public void SetLocalPosition(Vector2 pos) {
        transform.localPosition = pos;
    }
}
