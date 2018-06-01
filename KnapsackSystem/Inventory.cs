using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


/// <summary>
/// 背包物品的添加和删除
/// </summary>
public class Inventory : MonoBehaviour
{

    protected List<Item> itemList;//所有的物品
    protected List<Grid> gridList;//管理所有的格子
    protected List<Grid> emptyGridList;
    /// <summary>
    /// 钱币
    /// </summary>
    public  int M_COIN { get; private set; }
    private Canvas canvas;

    [HideInInspector]public Tooltip tooltip;
    [HideInInspector]public PickedItem pickedItem;
    private bool isTooltipShow = false;

    #region 单例模式
    private static Inventory _instance;
    public static Inventory Instance;
    //public static Inventory Instance {
    //    get {
    //        if (_instance == null) {
    //            _instance = GameObject.FindGameObjectWithTag("Knapsack").GetComponent<Inventory>();
    //        }
    //        return _instance;
    //    }
    //}
    #endregion

    [HideInInspector]public bool isPicked = false;//表示当前鼠标是否是抓取物品状态

    private KnapsackPanel m_KnapsackPanel;


    
    protected virtual void Awake()
    {
        m_KnapsackPanel = GameObject.FindGameObjectWithTag(Tags.Knapsack).GetComponent<KnapsackPanel>();
        M_COIN = 1000000;
        itemList = ItemManager.Instance.M_AllItem;
        GetAllGrid();
        tooltip = GameObject.FindGameObjectWithTag("AssistUI").transform.Find("TooltipPanel").GetComponent<Tooltip>();
        pickedItem = GameObject.FindGameObjectWithTag("AssistUI").transform.Find("PickedItemPanel").GetComponent<PickedItem>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

        m_KnapsackPanel.UpdateShowCoin(M_COIN);
    }

    protected virtual void Start()
    {
       
        LoadItemBySave();
        Instance = this;
        
    }
    /// <summary>
    /// 只用于测试用
    /// </summary>
    private void SetItem()
    {
        foreach(Item item in itemList) {
            PutItemByID(item.m_ID);
        }
    }

    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            PutItemByID(1, 5);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            PutItemByID(10, 1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PutItemByID(9, 1);
        }

        if (isPicked) {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,Input.mousePosition,null,out pos);
            pickedItem.SetLocalPosition(pos + new Vector2(40,-30));
        }
        if (isPicked == false && isTooltipShow) {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out pos);
            tooltip.SetLocalPosition(pos + new Vector2(50, -50));
        }
    }



    

    /// <summary>
    /// 得到解析出来的装备物品列表
    /// </summary>
    /// <returns></returns>
    public List<Item> GetItemList() {
        return itemList;
    }

    public Item GetItemByID(int id) {
        foreach (Item item in itemList) {
            if (item.m_ID == id) {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// 得到所有的格子
    /// </summary>
    private void GetAllGrid() {
        gridList = new List<Grid>();
        foreach (Transform child in transform) {
            if (child.name == "GridManager") {
                foreach (Transform t in child) {
                    //if (child.GetComponent<Grid>() == null) { continue; }
                    gridList.Add(t.GetComponent<Grid>());
                }
            }
            
        }
    }


    /// <summary>
    /// 得到相同且未满的格子
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private Grid TheSameItemGrid(int id)
    {
        foreach (Grid grid in gridList) {
            if (grid.grid_item != null && grid.grid_item.m_ID == id && grid.isFull == false) {
                return grid;
            }
        }
        return null;
    }
    /// <summary>
    /// 获得一个空格子
    /// </summary>
    /// <returns></returns>
    private Grid GetEmptyGrid() {
        //Debug.Log(gridList[0]);
        foreach (Grid grid in gridList) {
            
            if (grid.isEmpty) {
                return grid;
            }
        }
        return null;
    }

    int GetEmptyGridCount()
    {
        int count = 0;
        foreach (Grid grid in gridList)
        {
            if (grid.isEmpty)
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// 获得当前剩下的所有空格
    /// </summary>
    /// <returns></returns>
    List<Grid> GetAllEmpty()
    {
        if (emptyGridList == null) emptyGridList = new List<Grid>();
        foreach (Grid grid in gridList)
        {
            if (grid.isEmpty)
            {
                emptyGridList.Add(grid);
            }
        }


        return emptyGridList;
    }


    /// <summary>
    /// 根据需要的数量 查找空格
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    private List<Grid> GetEmptyGridByCount( int count) {
        List<Grid> list = null;
        int i = 0;
        foreach (Grid grid in gridList)
        {
            if (grid.isEmpty)
            {
                i++;
                list.Add(grid);
                if (i == count) break;
            }
        }
        return list;
        
    }

    /// <summary>
    /// 根据物品ID将物品放入格子 一个格子放多少由物品的capacity决定
    /// 逻辑：
    /// 如果是需要占用一格的物品 直接找空格子 
    /// 一格可以放多个的物品 先找相同的还未满的格子 有多的就找空格子
    /// capacity为1（装备）的物品只能一个一个保存 大于一（材料 消耗品）的可以一次保存多个
    /// </summary>
    public bool PutItemByID(int id,int count = 1)
    {
        //int remainCount = 0;
        Item item = GetItemByID(id);
        
        if (item == null || count ==0) { Debug.LogError("放置的物品不能为空");return false; }

        Grid empty_Grid = GetEmptyGrid();
        Grid same_grid = TheSameItemGrid(id);
        if (empty_Grid == null && same_grid == null)
        {
            Debug.Log("物品已满，请清理" + "id: " + id + " count: " + count);
            return false;
        }
        else
        {
            if (item.m_Capacity == 1)
            {
                if (count > 1)
                {
                    if (GetEmptyGridCount() < count)
                    {
                        Debug.Log("存取物品数量大于格子数");
                        return false;
                    }
                    for (int i = 0; i < count; i++)
                    {
                        print(GetAllEmpty()[i]);
                        emptyGridList[i].StoreItem(item);
                    }
                    return true;
                }
                else
                {
                    if (empty_Grid != null)
                    {
                        empty_Grid.StoreItem(item);
                        return true;
                    }
                    else
                    {
                        //TODO
                        Debug.Log("没有空格 无法保存");
                        return false;
                    }

                }   
            }
            else
            {
                //capacity大于一的 先找相同未满的格子 填满 那么下一步 肯定是找空格 
                if (same_grid != null)
                {
                    //Debug.Log(" same_grid " + item.m_ID + "和" + item.m_Sprite + "空格状态 " + empty_Grid.isEmpty);
                    int remain_count = item.m_Capacity - same_grid.Current_Count;
                    //Debug.Log(remain_count );
                    if (remain_count >= count)
                    {
                        same_grid.StoreItem(item, count);
                        return true;
                    }
                    else
                    {
                        //先将相同格子填满 还有未保存的 找空格
                        same_grid.StoreItem(item, remain_count);
                        if (empty_Grid == null)
                        {
                            //TODO 物品栏不足的信息 
                            Debug.Log("物品栏不足");
                            return false;
                        }
                        else
                        {
                            empty_Grid.StoreItem(item, count - remain_count);
                            return true;
                        }
                    }
                }
                else
                {
                    if (empty_Grid == null)
                    {
                        //TODO 物品栏不足的信息 
                        Debug.Log("物品栏不足");
                        return false;
                    }
                    else
                    {
                        empty_Grid.StoreItem(item, count);
                        return true;
                    }
                }
            }

        }
        
        
    }



    /// <summary>
    /// 设置抓取格子中的物品 （出售，交换位置，放置快捷栏）
    /// </summary>
    public void SetPickedItem(Item item,int count) {
        isPicked = true;
        pickedItem.SetItem(item,count);
    }

    public void ResetPickedItem() {
        isPicked = false;
        pickedItem.ResetItem();
    }

    /// <summary>
    /// 装备信息显示
    /// </summary>
    public void ShowTooltip(string info) {
        tooltip.Show(info);
        isTooltipShow = true;
    }

    public void HideTooltip() {
        tooltip.Hide();
        isTooltipShow = false;
    }


    public List<Grid> GetCurrentGridList()
    {
        if (gridList != null)
        {
            return gridList;
        }
        return null;
    }

    //if (count == 1)
    //{
    //    //一次保存1个capacity为1的物品
    //    empty_Grid = GetEmptyGrid();
    //    empty_Grid.StoreItem(item, count);
    //}
    //else
    //{
    //    //一次保存count个capacity为1的物品
    //    //剩余空格数量小于count
    //    if (GetEmptyGridByCount(count).Count < count)
    //    {
    //        //TODO 显示格子剩余不足的信息 count - GetEmptyGridByCount(count).Count个
    //        Debug.LogError("空物品栏剩余不足 剩余:" + (count - GetEmptyGridByCount(count).Count) + " 个");
    //    }
    //    else {
    //        foreach (Grid grid in GetEmptyGridByCount(count)) {
    //            grid.StoreItem(item, GetEmptyGridByCount(count).Count);
    //        }
    //    }

    //}

    /// <summary>
    /// 由于没有给Grid设置id 所以并不能完全按位置保存 
    /// </summary>
    private void LoadItemBySave()
    {
        JsonData inventory =  SaveHelper.LoadSaveJson("GameSave");
        //Debug.Log(inventory["m_Inventoy"]);
        int count = 0;
        foreach (JsonData item in inventory["m_GridID"])
        {
            count++;
        }
        for (int i = 0; i < count; i++)
        {
            if ((int)inventory["m_GridItemID"][i] == -1 || (int)inventory["m_GridItemCount"][i] == -1) continue;
            PutItemByID((int)inventory["m_GridItemID"][i], (int)inventory["m_GridItemCount"][i]);
            //print(count +  "here" + gameObject.name);
        }

    }

    void AddCoin(int count)
    {
        M_COIN += count;
        m_KnapsackPanel.UpdateShowCoin(M_COIN);

    }

    public bool ReduceCoin(int count)
    {
        if (M_COIN == 0 || M_COIN < count) return false;
        M_COIN -= count;print(M_COIN);
        m_KnapsackPanel.UpdateShowCoin(M_COIN);
        return true;
    }




}
