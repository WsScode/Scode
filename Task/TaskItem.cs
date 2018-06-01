using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour {

    private Text m_TaskTitle;
    private Image m_TaskNPCHead;
    private Image m_FinishLogo;
    private Text m_AcceptLogo;
    private Image m_ReachLogo;
    private Transform m_TaskInfo;

    public Task m_Task;

    private TaskPanel tp;

    private void Awake()
    {
        m_TaskTitle = transform.Find("TaskTitle").GetComponent<Text>();
        m_TaskNPCHead = transform.Find("NPCHead").GetComponent<Image>();
        m_FinishLogo = transform.Find("FinishLogo").GetComponent<Image>();
        m_FinishLogo.gameObject.SetActive(false);
        m_AcceptLogo = transform.Find("AcceptLogo").GetComponent<Text>();
        m_AcceptLogo.gameObject.SetActive(false);
        m_ReachLogo = transform.Find("ReachLogo").GetComponent<Image>();
        //UPdateHintBox();

        GetComponent<Button>().onClick.AddListener(delegate () { Onclick(); });
        RegisterEvent();
    }

    void Onclick()
    {
        if (m_Task == null) return;
        tp.UpdateTaskInfoPanel(m_Task);
    }

    public void Show(Task task, TaskPanel _tp)
    {
        tp = _tp;
        if (m_Task != null) { gameObject.SetActive(true); return; }
        if (task == null) return;
        m_Task = task;
        Object[] os = Resources.LoadAll("NPCHead/NPC");
        for (int i = 0; i < os.Length; i++)
        {
            if (os[i].GetType() == typeof(Sprite) && os[i].name == task.M_NPCHead)
            {
                m_TaskNPCHead.sprite = (Sprite)os[i];
            }
        }
        m_TaskTitle.text = task.M_TaskTitle;
        if (task.M_TaskState == Task.TaskState.Finished)
        {
            m_FinishLogo.gameObject.SetActive(true);
            m_AcceptLogo.gameObject.SetActive(false);
        }
        if (task.M_TaskState == Task.TaskState.Accepted)
        {
            m_AcceptLogo.gameObject.SetActive(true);
        }
        if (task.M_TaskState == Task.TaskState.Reach)
        {
            m_ReachLogo.gameObject.SetActive(true);
        }

    }


    public void Hide() { gameObject.SetActive(false); }


    public void RegisterEvent()
    {
        List<MyEvent.MyEventType> evtype = new List<MyEvent.MyEventType>() { MyEvent.MyEventType.TaskAccept, MyEvent.MyEventType.TaskAbort, MyEvent.MyEventType.TaskFinish };
        foreach (MyEvent.MyEventType t in evtype)
        {
            MyEventSystem.m_MyEventSystem.RegisterEvent(t, UPdateHintBox);
        }
    }
    /// <summary>
    /// 更新任务的提示小框 进行中/已完成 
    /// 未接受不显示
    /// </summary>
    public void UPdateHintBox(MyEvent evt)
    {
        if (m_Task.M_ID != evt.m_Task.M_ID) return;
        if (evt.m_MyEventType == MyEvent.MyEventType.TaskAccept)
        {
            m_AcceptLogo.gameObject.SetActive(true);
        }
        else if (evt.m_MyEventType == MyEvent.MyEventType.TaskAbort)
        {
            m_AcceptLogo.gameObject.SetActive(false);
        }
        else if (evt.m_MyEventType == MyEvent.MyEventType.TaskFinish)
        {
            m_FinishLogo.gameObject.SetActive(true);
            m_AcceptLogo.gameObject.SetActive(false);
        } else if (evt.m_MyEventType == MyEvent.MyEventType.TaskReach)
        {
            m_ReachLogo.gameObject.SetActive(true);
            m_AcceptLogo.gameObject.SetActive(false);
        }
        else
        {
            m_FinishLogo.gameObject.SetActive(false);
            m_AcceptLogo.gameObject.SetActive(false);
            m_ReachLogo.gameObject.SetActive(false);
        } 
    }

}
