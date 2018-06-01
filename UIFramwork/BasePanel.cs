using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ShowUIType {
Normal,//显示多个UI 互不影响
PopMethod,//暂停其他UI 只能操作当前显示的 栈来管理
HideOther,//隐藏其他的 用于整屏显示某个UI时

}

/// <summary>
/// UI基类 
/// </summary>
public class BasePanel : MonoBehaviour,IPointerDownHandler {

    protected CanvasGroup canvasGroup;//用来控制UI的显示和隐藏的
    [HideInInspector] public UIController uIController;
    protected ShowUIType showUIType;
    [HideInInspector] public ShowUIType m_showUIType { get { return showUIType; }  set { showUIType = value; } }


    public virtual void Awake() {

        
    }

    public virtual void OnEnter(UIController uIController) {
        AudioController.Instance.PlayAssistAudio(null, AudioController.AudioClipEnum.open1,1,3);
        this.uIController = uIController;//只有显示页面 才能拿到uicontroller
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public virtual void OnEnter(UIController uIController, string s)
    {
        
    }

    /// <summary>
    /// 暂停某个页面的操作 
    /// </summary>
    public virtual void OnPause() {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;//检测点击
    }

    /// <summary>
    /// 继续某个页面的操作 
    /// </summary>
    public virtual void OnResume() {
       
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;//检测点击
        AudioController.Instance.PlayAssistAudio(null, AudioController.AudioClipEnum.open1, 1,3);
    }

    public virtual void OnClose() {
        //前提是必须和json文件中panelTypeString的命名一样  后缀字数也一直
        string str_name = gameObject.name;
        string str_type = str_name.Remove(str_name.Length - 12, 12);
        UIPanelType type = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), str_type);
        uIController.Hide(type);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;//检测点击
    }

    /// <summary>
    /// 点击
    /// </summary>
    public virtual void OnClick(){  }


    /// <summary>
    /// 事件监听
    /// </summary>
    protected virtual void  OnEvent(MyEvent eventType) {

    }




    /// <summary>
    /// 当前脚本的主要逻辑执行方法
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator OnLogic() {

        return null;
    }

    /// <summary>
    /// 显示时需要更新
    /// </summary>
    public virtual void UpdateShow()
    {

    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
         
    }
}
