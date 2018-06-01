using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanel : BasePanel {

    //任务分类列表
    private Transform m_TaskClassList;
    private Button[] m_BTNS;

    //任务列表
    private Transform m_TaskItemList;

    //任务详情页面
    private Transform m_TaskInfo;
    private Text m_TaskTitle;
    private Text m_TaskContent;
    private Transform m_Award;//奖励物品内容 （物品 经验 钱币）
    private Transform m_AwardItem;
    private Text m_AwardEXP;
    private Text m_AwardCoin;

    private Dictionary<Task.TaskType, List<TaskItem> > m_Items;
    private Task.TaskType m_Current;


    public override void Awake()
    {
        base.Awake();
        showUIType = ShowUIType.Normal;

        InitPanel();

    }


    void InitPanel()
    {
        m_TaskClassList = transform.Find("TaskClassList");
        m_BTNS = m_TaskClassList.GetComponentsInChildren<Button>();
        foreach (Button b in m_BTNS)
        {
            b.onClick.AddListener(delegate () { OnClassItemClick(b.name); });
        }

        m_TaskItemList = transform.Find("TaskItemList");

        m_TaskInfo = transform.Find("TaskInfo");
        m_TaskTitle = m_TaskInfo.Find("TaskTitle").GetComponent<Text>();
        m_TaskContent = m_TaskInfo.Find("TaskContent").GetComponent<Text>();
        m_AwardItem = m_TaskInfo.Find("AwardItem");
        m_AwardEXP = m_TaskInfo.Find("AwardContent/EXPAward").GetComponent<Text>();
        m_AwardCoin = m_TaskInfo.Find("AwardContent/CoinAward").GetComponent<Text>();
        m_TaskInfo.gameObject.SetActive(false);
        
    }

    private void Start()
    {
        UpdateTaskListItemPanel(Task.TaskType.MainTask);
    }


    public override void OnEnter(UIController uIController)
    {
        base.OnEnter(uIController);
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;//检测点击
    }

    public override void OnClick()
    {
        
    }

    /// <summary>
    /// 点击任务分类表的UI显示
    /// </summary>
    public void OnClassItemClick(string btnName)
    {
        foreach (Button b in m_BTNS)
        {
            if (b.name == btnName)
            {
                b.transform.Find("Arrow").gameObject.SetActive(true);
                b.GetComponent<Image>().color = new Color(150f / 255f,150f / 255f,150f / 255f);
                UpdateTaskListItemPanel((Task.TaskType)System.Enum.Parse(typeof(Task.TaskType),btnName));
            }
            else
            {
                b.transform.Find("Arrow").gameObject.SetActive(false);
                b.GetComponent<Image>().color = Color.white;
            }
        }
    }


    /// <summary>
    /// 打开任务面板 默认显示主线任务
    /// 点击其他类型任务时 先隐藏上一个任务列表
    /// </summary>
    public void UpdateTaskListItemPanel(Task.TaskType taskType)
    {
        if (m_Items == null) m_Items = new Dictionary<Task.TaskType, List<TaskItem>>();

        //隐藏上一个任务列表 
        if (m_Items.TryGetTool(m_Current) != null)
        {
            foreach (TaskItem ti in m_Items.TryGetTool(m_Current)) { ti.Hide(); }
        }

        List<Task> tList = TaskManager.Instance.GetTaskList(taskType); 
        if (tList.Count == 0) return;
        //显示列表
        List<TaskItem> tiList = new List<TaskItem>();
        if (m_Items.ContainsKey(taskType))
        {
            foreach (TaskItem ti in m_Items.TryGetTool(taskType))
            {
                ti.Show(null, this); 
            }
        }
        else
        {
            foreach (Task task in tList)
            {
                TaskItem ti = Instantiate(Resources.Load<GameObject>("UIPanel/TaskItem")).GetComponent<TaskItem>();
                ti.transform.SetParent(m_TaskItemList);
                ti.Show(task, this);
                tiList.Add(ti);
            }
            m_Items.Add(taskType, tiList);
        }
        m_Current = taskType;//更新当前显示的任务类型
    }



    /// <summary>
    /// 显示并更新任务详情面板
    /// </summary>
    /// <param name="task"></param>
    public void UpdateTaskInfoPanel(Task task)
    {
        if (task == null) return;
        //m_TaskInfo.gameObject.SetActive(true);
        //m_TaskTitle.text = task.M_TaskTitle;
        //m_TaskContent.text = task.M_TaskContent;
        //m_AwardEXP.text = task.M_AwardEXP == 0 ? "" : task.M_AwardEXP + "";
        //m_AwardCoin.text = task.M_AwardCoin + "";
        uIController.ShowPanel(UIPanelType.TaskInfo);
        TaskInfoPanel tip = uIController.GetPanelByType(UIPanelType.TaskInfo).GetComponent<BasePanel>() as TaskInfoPanel ;
        if (tip.M_ShowTask == task) return;//正在显示的不刷新信息
        tip.M_ShowTask = task;
        tip.UpdateShow();

    }





}
