using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISwitchClick : MonoBehaviour {



    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Onclick);
    }

    /// <summary>
    /// 点击panel显示按钮
    /// </summary>
    public  void  Onclick() {
        //前提是必须和json文件中panelTypeString的命名一样  后缀也必须是button
        string str_name = gameObject.name;
        string str_type = str_name.Remove(str_name.Length - 6,6);
        UIPanelType type = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), str_type);
        UIController._instance.ShowPanel(type);
    }
}
