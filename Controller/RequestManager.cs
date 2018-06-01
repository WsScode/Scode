
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


/// <summary>
/// 处理服务器返回的信息 并交给相应的Request类处理
/// </summary>
public class RequestManager : BaseController {
    private Dictionary<ActionCode, BaseRequest> m_abDict = new Dictionary<ActionCode, BaseRequest>();

   

    public RequestManager(ManagerController mc) : base(mc) { }

    public void AddRequest(ActionCode act, BaseRequest request)
    {
        if (!m_abDict.ContainsKey(act))
        {
            m_abDict.Add(act, request);
        }
    }

    public void RemoveRequest(ActionCode act)
    {
        if (m_abDict.ContainsKey(act))
        {
            m_abDict.Remove(act);
        }
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    /// <summary>
    /// 服务器回应数据处理
    /// 分发给相应的类处理
    /// </summary>
    public void ProcessResponse(ActionCode act,string data)
    {
        
        BaseRequest request =  m_abDict.TryGetTool(act);
        if (request == null)
        {
            Debug.LogWarning("ActionCode: " + act + " ，不存在对应的Request类");
            return;
        }
        request.OnResponse(data);
    }

}
