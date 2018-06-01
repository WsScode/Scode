using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using LitJson;
using System.Security.Cryptography;


/// <summary>
/// 保存游戏类
/// </summary>
public  class Save {

    //public List<Grid> M_CurrentsGrid { get; set; }

    public Save()
    {
        SaveAllAGame();
       
    }

    public  void SaveAllAGame()
    {
        SaveInventoryItem();
        SavePlayerInfo();
        SaveSceneInfo();
    }


    
    //public List<List<int>> m_Inventoy { get; set; }
    public List<int> m_GridID { get; set; }
    public List<int> m_GridItemID { get; set; }
    public List<int> m_GridItemCount { get; set; }

    /// <summary>
    /// 保存背包物品
    /// </summary>
    private void SaveInventoryItem()
    {
        //m_Inventoy = new List<List<int>>();
        m_GridID = new List<int>();
        m_GridItemID = new List<int>();
        m_GridItemCount = new List<int>();
        int grid_ID = 0;
        if (Inventory.Instance.GetCurrentGridList() != null)
        {
            foreach (Grid grid in Inventory.Instance.GetCurrentGridList())
            {
                if (grid.grid_item == null)
                {
                    //空格子
                    m_GridID.Add(grid_ID);//格子ID
                    grid_ID++;
                    m_GridItemID.Add(-1);//标志位空格子 加载的时候跳过此格子
                    m_GridItemCount.Add(-1);
                    continue;
                }
                Item item = grid.grid_item;
                m_GridID.Add(grid_ID);//格子ID
                grid_ID++;
                m_GridItemID.Add(grid.grid_item.m_ID);//物品ID
                m_GridItemCount.Add(grid.Current_Count);//物品数量
               
            }
            //m_Inventoy.Add(m_GridID);
            //m_Inventoy.Add(m_GridItemID);
            //m_Inventoy.Add(m_GridItemCount);
        }

        Debug.Log("Save Inventory");
    }

    /// <summary>
    /// 保存人物信息
    /// </summary>
    private void SavePlayerInfo()
    {

    }


    /// <summary>
    /// 保存场景信息
    /// </summary>
    private void SaveSceneInfo()
    {

    }











}
//Save Class End










/// <summary>
/// 保存背包  保存格子里面物品的id 和数量 加载的时候根据id添加物品和数量
/// </summary>
//public class SaveInventory
//{

//    public List<int> m_Inventoy { get; set; }

//    public SaveInventory() { SaveInventoryItem(); }
//    public void SaveInventoryItem()
//    {
//        m_Inventoy = new List<int>();
//        int grid_ID = 0;
//        if (Inventory.Instance.GetCurrentGridList() != null)
//        {
//            foreach (Grid grid in Inventory.Instance.GetCurrentGridList())
//            {

//                if (grid.grid_item == null)
//                {
//                    //空格子
//                    m_Inventoy.Add(grid_ID);//格子ID
//                    grid_ID++;
//                    m_Inventoy.Add(-1);//标志位空格子 加载的时候跳过此格子
//                    continue;
//                }
//                Item item = grid.grid_item;
//                m_Inventoy.Add(grid_ID);//格子ID
//                grid_ID++;
//                m_Inventoy.Add(grid.grid_item.m_ID);//物品ID
//                m_Inventoy.Add(grid.Current_Count);//物品数量
//            }
           
//            //SaveHelper.SaveByJson(m_Inventoy, "InventoySave");
//            //Debug.Log(SaveHelper.FileIsExist("test2"));
//        }

//    }
//}
