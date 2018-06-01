using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


public class BaseRequest : MonoBehaviour {
    protected ManagerController m_mc;
    protected ManagerController M_MC
    {
        get { if (m_mc == null) { m_mc = ManagerController.Instance; } return m_mc; }
    }



    protected RequestCode m_Request;
    protected ActionCode m_Action;

    /// <summary>
    /// 重写此方法 需要给初始化m_Action
    /// </summary>
    protected virtual void Start()
    {
        ManagerController.Instance.AddRequest(m_Action, this);
    }


    /// <summary>
    /// 向服务器发送request
    /// </summary>
    public virtual void SendRequest(string data)
    {
        M_MC.SendRequest(m_Request,m_Action,data);
    }
    public virtual void SendRequest() { }

    /// <summary>
    /// 服务器回应数据处理
    /// </summary>
    /// <param name="act"></param>
    /// <param name="data"></param>
    public virtual void OnResponse(string data)
    {

    }

	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnDestroy()
    {
        //M_MC.
    }
}
