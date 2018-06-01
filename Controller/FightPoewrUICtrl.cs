using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 战斗力动画显示
/// </summary>
public class FightPoewrUICtrl : MonoBehaviour {
    private Text fightingPower;

    private int startPower = 0;
    private int endPower = 0;
    private bool isUp = false;//战斗力是否上升
    private bool isDown = false;//战斗力是否下将
    private bool isShow = false;
    private float time = 0;
    private float timer = 1.5f;
    private bool isComplete = false;
    private string upOrDown = "";
    private int count = 0;

    private void Awake()
    {
        fightingPower = GameObject.FindGameObjectWithTag("AssistUI").transform.Find("FightingPowerPanel").GetComponent<Text>();
        
       
    }

    private void Update()
    {
        if (isShow) {
            isComplete = false;
            time = 0;
            fightingPower.transform.localScale = new Vector3(1.2f,1.2f,0);
            fightingPower.gameObject.SetActive(true);
            if (isUp)
            {
                startPower += (int)(Time.fixedDeltaTime * 100);
                upOrDown = "+";
                if (startPower >= endPower)
                {
                    startPower = endPower;
                    isShow = false;
                    isUp = false;
                    isComplete = true;
                }
            }
            else
            {
                startPower -= (int)(Time.fixedDeltaTime * 100);
                upOrDown = "-";
                if (startPower <= endPower)
                {
                    startPower = endPower;
                    isShow = false;
                    isDown = false;
                    isComplete = true;
                }
            }
            
            fightingPower.text = "战斗力" + startPower + "\n" +  upOrDown + count;
        }
        if (isComplete) {
            fightingPower.transform.localScale = new Vector3(1f, 1f, 0);
            time += Time.deltaTime;
            if (time >= timer) { fightingPower.gameObject.SetActive(false); time = 0; }
        }
    }


    public void IsUpdateShow(int startPower,int endPower) {
        count = Mathf.Abs(startPower - endPower);
        this.startPower = startPower;
        this.endPower = endPower;
        if (startPower == endPower) { isShow = false; return; }
        if (startPower < endPower)
        {
            isShow = true;
            isUp = true;

        }
        else
        {
            isShow = true;
            isDown = true;
        }
    }


}
