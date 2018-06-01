
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class Task
{
    public enum TaskType
    {
        MainTask,//id 1开头
        SideTask,//id 100开头
        DailyTask,//id 1000开头
        RewardTask//悬赏任务 id 10000开头
    }

    public enum TaskState {Normal,Accepted,Reach,Finished }



    //id 任务标题 任务内容 任务奖励 发布npc  任务地点（或者副本）完成后触发的任务id 
    public int M_ID { get; set; }
    public string M_TaskTitle { get; set; }
    public string M_TaskContent { get; set; }
    public string M_NPCName { get; set; }
    public TaskType M_TaskType { get; set; }
    public int[] M_AwardItemsID { get; set; }//任务奖励物品id 
    public int M_AwardEXP { get; set; }
    public int M_AwardCoin { get; set; }
    public string M_NPCHead { get; set; }
    private TaskState m_TaskState = TaskState.Normal;//默认 正常（未接受） 的状态
    public TaskState M_TaskState { get { return m_TaskState; } set { m_TaskState = value; } }




    public bool M_IsFinish { get; private set; }
    public bool m_IsAccept { get; private set; }

    public void Accept() { m_TaskState = TaskState.Accepted; }
    public void GiveUP() { m_TaskState = TaskState.Normal; }
    public void Reach() { m_TaskState = TaskState.Reach; }//达到任务目标 等待玩家完成任务操作
    public void Finish() { m_TaskState = TaskState.Finished; }
}




public class TaskManager : MonoBehaviour {
    private List<Task> m_TaskList;//所有任务
    //private Dictionary<Task.TaskType, List<Task>> m_TaskDic;//所有任务
    private List<int> m_TaskFinished;//已经完成的任务ID
    private List<int> m_TaskAccept;//已经接受的任务


    public static TaskManager Instance;

    List<MyEvent.MyEventType> evtypes = new List<MyEvent.MyEventType>() { MyEvent.MyEventType.TaskAccept,MyEvent.MyEventType.TaskAbort, MyEvent.MyEventType.TaskFinish, MyEvent.MyEventType.TaskReach };

    private void Awake()
    {
        ParseTaskJSON();
        Instance = this;
    }

    void ParseTaskJSON()
    {
        JsonData jd = JsonMapper.ToObject(Resources.Load<TextAsset>("TaskJ").text);

        m_TaskList = new List<Task>();
       

        foreach (JsonData taskinfo in jd)
        {
            Task task = new Task();
            task.M_ID = (int)taskinfo["id"];
            task.M_TaskType = (Task.TaskType)System.Enum.Parse(typeof(Task.TaskType), taskinfo["taskType"] + "");
            task.M_TaskTitle = taskinfo["title"] + "";
            task.M_TaskContent = taskinfo["content"] + "";
            string[] ss = (taskinfo["award"]["itemID"] + "").Split(',');

            for (int i = 0; i < ss.Length; i++)
            {
                int temp = 0;
                if (task.M_AwardItemsID == null) task.M_AwardItemsID = new int[ss.Length];
                if (int.TryParse(ss[i], out temp)) { task.M_AwardItemsID[i] = temp; }
            }
            task.M_AwardEXP = (int)taskinfo["award"]["exp"];
            task.M_AwardCoin = (int)taskinfo["award"]["coin"];
            task.M_NPCName = taskinfo["npc"] + "";
            task.M_NPCHead = taskinfo["npcHead"] + "";
            if (task.M_ID == 2) { task.M_TaskState = Task.TaskState.Reach; }
            m_TaskList.Add(task);
        }

        //foreach (Task t in m_TaskList) { print(t.M_ID); }

    }

    

    public Task GetTaskByID(int id)
    {
        if (id <= 0 || m_TaskList.Count == 0) return null;
        foreach (Task task in m_TaskList)
        {
            if (task.M_ID == id) { return task; }
        }
        return null;
    }


    public List<Task> GetTaskList(Task.TaskType tt)
    {
        List<Task> tList = new List<Task>();
        foreach (Task task in m_TaskList)
        {
            if (task.M_TaskType == tt) tList.Add(task);
        }
        return tList;
    }


    void RegisetrEvent()
    {
        foreach (MyEvent.MyEventType evtype in evtypes)
        {
            MyEventSystem.m_MyEventSystem.RegisterEvent(evtype, OnEvent);
        }
        
    }

    private void OnEvent(MyEvent evt)
    {
        switch (evt.m_MyEventType)
        {
            case MyEvent.MyEventType.TaskAccept:
                AcceptTask(evt.m_Task);
                break;
            case MyEvent.MyEventType.TaskAbort:
                GiveUpTask(evt.m_Task);
                break;
            case MyEvent.MyEventType.TaskReach:
                ReachTask(evt.m_Task);
                break;
            case MyEvent.MyEventType.TaskFinish:
                FinishTask(evt.m_Task);
                break;
        }
    }

    /// <summary>
    /// 接受任务
    /// </summary>
    /// <param name="task"></param>
    public void AcceptTask(Task task)
    {
        if (task == null || task.M_TaskState != Task.TaskState.Normal) return;
        task.Accept();
        if (m_TaskAccept == null) m_TaskAccept = new List<int>();
        m_TaskAccept.Add(task.M_ID);
    }
    /// <summary>
    /// 放弃任务
    /// </summary>
    public void GiveUpTask(Task task)
    {
        if (task == null || task.M_TaskState != Task.TaskState.Accepted || !m_TaskAccept.Contains(task.M_ID)) return;
        task.GiveUP();
        m_TaskAccept.Remove(task.M_ID);
    }
    /// <summary>
    /// 达成任务条件 显示提示UI
    /// </summary>
    public void ReachTask(Task task)
    {

    }
    /// <summary>
    /// 完成任务
    /// 得到奖励物品并保存到背包
    /// </summary>
    public void FinishTask(Task task)
    {
        if (task.M_TaskState != Task.TaskState.Finished) return;
        task.Finish();
    }



}




