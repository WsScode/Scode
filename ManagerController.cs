using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


/// <summary>
/// 管理所有的Manager脚本 和中转逻辑
/// </summary>
public class ManagerController : MonoBehaviour {

    public static ManagerController Instance;

    private ClientManager m_ClientM;
    private RequestManager m_RequestM;

    private void Awake()
    {
        Init();
        Instance = this;
    }


    void Init()
    {
        m_RequestM = new RequestManager(this);
        m_ClientM = new ClientManager(this);

        m_RequestM.OnStart();
        m_ClientM.OnStart();
        
    }

    private void Update()
    {

    }

    /// <summary>
    /// 服务器消息交给RequestManager处理
    /// </summary>
    public void ProcessResponseMessage(ActionCode action , string data)
    {
        m_RequestM.ProcessResponse(action,data);
    }

    public void AddRequest(ActionCode act,BaseRequest request)
    {
        m_RequestM.AddRequest(act,request);
    }
    public void RemoveRequest(ActionCode act)
    {
        m_RequestM.RemoveRequest(act);
    }

    public void SendRequest(RequestCode request,ActionCode act,string data)
    {
        m_ClientM.SendRequest(request,act,data);
    }

    public void CloseClient()
    {

    }




















    private void OnDestroy()
    {
        m_ClientM.Close();
    }



}
