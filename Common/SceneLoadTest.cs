using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadTest : MonoBehaviour {

    public static int m_SceeneIndx;//需要加载的场景buildindex
    private float progress;
    private AsyncOperation m_Async;
    public Slider loadSlider;
    public Text loadText;
    private float targetNum;
    public Text tipText;

    public void Start()
    {
        StartCoroutine("LoadSceneSync", m_SceeneIndx);
    }

    IEnumerator LoadSceneSync() {

        m_Async = SceneManager.LoadSceneAsync(m_SceeneIndx);
        m_Async.allowSceneActivation = false;
        progress = m_Async.progress;
        
        yield return progress;
    }


    private void Update()
    {
        targetNum = m_Async.progress;
        if (m_Async.progress >= 0.9f) { targetNum = 1; }
        if (loadSlider.value != targetNum)
        {
            loadSlider.value = Mathf.Lerp(loadSlider.value, targetNum, Time.deltaTime);
            if (Mathf.Abs(loadSlider.value - targetNum) <= 0.01f) loadSlider.value = targetNum;
        }
        tipText.text = "挑战地下城要记得准备复活药以备万一哟";
        loadText.text ="已加载" + ((int)(loadSlider.value * 100)) + "%";
        if ((int)(loadSlider.value * 100) == 100)
        {
            m_Async.allowSceneActivation = true;
        }
    }


}
