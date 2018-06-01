using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

/// <summary>
/// 客户端登录请求
/// </summary>
public class LoginRequest : BaseRequest {


    protected override void Start()
    {
        m_Action = ActionCode.Login;
        m_Request = RequestCode.User;
        base.Start();
        test();
    }

    /// <summary>
    /// just test
    /// </summary>
    private void test()
    {
        SendRequest("scode","123");
    }

    public void SendRequest(string username,string password)
    {
        string data = username + "," + password;
        M_MC.SendRequest(m_Request,m_Action,data);
    }


    public override void OnResponse(string data)
    {
        
    }

    

}
