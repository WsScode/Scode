using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{

    private Text tooltipText;

    public  void Awake()
    {
        tooltipText = transform.Find("Info").GetComponent<Text>();
    }


    public void Show(string text) {
        gameObject.SetActive(true);
        tooltipText.text = text;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void  SetLocalPosition(Vector2 vec2) {
        transform.localPosition = vec2;
    }

}
