using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueBoxPanel : BasePanel {

    private Text m_Name;//对话时  人物的名称
    private Text m_Content;


    public override void Awake()
    {
        m_showUIType = ShowUIType.HideOther;
        m_Name = transform.Find("Name").GetComponent<Text>();
        m_Content = transform.Find("Content").GetComponent<Text>();
        
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
       
    }

    public override void OnEnter(UIController uIController)
    {
        base.OnEnter(uIController);
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    

}
