using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    /// <summary>
    /// 枚举所有的AudioClip
    /// </summary>
    public enum AudioClipEnum { arrow, buy_drug, buy_gem,buy_paper,close1,footsteps_bendiao,footsteps_fenghuang,open1,open2 }


    public static AudioController Instance;


    [HideInInspector]public bool m_IsMute = false;//静音
    public AudioSource m_MainAudioSource;//主播放器 播放场景背景音乐
    public AudioSource m_AssistAudioSource;//
    public AudioClip[] m_AudioClips { get; private set; }

    /// <summary>
    /// 资源打包加载 TODO
    /// </summary>
    private void InitAudioClips()
    {
        //Test

    }


    private void Awake()
    {
        m_MainAudioSource = GetComponent<AudioSource>();
        m_AssistAudioSource = transform.Find("As").GetComponent<AudioSource>();
        MyEventSystem.m_MyEventSystem.RegisterEvent(MyEvent.MyEventType.Volume, OnEvent);
        //PlayAudio(m_AudioSource, m_AudioClips);
        Instance = this;
    }


    void OnEvent(MyEvent evt)
    {
        ChangeVolume(evt.m_FloatPara);
    }


    private void Update()
    {
       
    }


    /// <summary>
    /// 播放背景音效
    /// </summary>
    public void PlayMainAudio(AudioSource audioSource,AudioClip audioClip,bool loop = true)
    {
        m_MainAudioSource = audioSource;
        if (m_IsMute) return;
        audioSource.clip = audioClip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    /// <summary>
    /// 播放其余音效
    /// </summary>
    public void PlayAssistAudio(AudioSource audioSource , AudioClipEnum clip, float volume,float pitch, bool loop = false)
    {
        if (audioSource == null) { audioSource = m_AssistAudioSource; }
        if (m_IsMute) return;
        AudioClip audioClip = Resources.Load<AudioClip>("Sound/" + clip + "");
        audioSource.clip = audioClip;
        audioSource.pitch = pitch;
        audioSource.loop = loop;
        audioSource.Play();
    }


    void ChangeVolume(float volume)
    {
        m_MainAudioSource.volume = volume;
    }



}
