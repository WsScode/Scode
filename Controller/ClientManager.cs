using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using Common;
using System.Threading;



public class ClientManager : BaseController {
    private Socket m_Socket;
    private const string m_IP = "192.168.1.6";
    private const int m_Port = 8868;
    Message m_MSG = new Message();
    Thread hbThread;

    public ClientManager(ManagerController mc):base(mc) {}

    public override void OnStart()
    {
        Connect();
    }




    /// <summary>
    /// 连接服务器
    /// </summary>
    public void Connect()
    {
        m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //m_Socket.Bind(new IPEndPoint(IPAddress.Parse("192.168.1.5"),8869));
        try
        {
            m_Socket.Connect(m_IP, m_Port);

            if (m_Socket.Connected) Debug.Log("连接服务器成功");

            hbThread = new Thread(SendHeartBeat);//开启发送心跳包的线程
            hbThread.Start();


            ReceiveMSG();

            

        }
        catch (Exception e)
        {
            //hbThread.Abort();
            Debug.Log("错误：" + e + " 连接服务器失败");
        }
    }


    public void SendRequest(RequestCode request, ActionCode act,string data)
    {
        byte[] requestByte = m_MSG.PackMessage(request,act,data);
        m_Socket.Send(requestByte);
    }

    /// <summary>
    /// 接收服务器消息数据
    /// </summary>
    public void ReceiveMSG()
    {
        //异步接收数据
        m_Socket.BeginReceive(m_MSG.M_MsgData, m_MSG.M_StartIndex, m_MSG.m_RemainSize, SocketFlags.None, ReciveCallBack, null);

        // JUST FOR TEST
        //byte[] dataBuffer = new byte[1024];
        //int msgLength = m_Socket.Receive(dataBuffer);
        //m_Socket.BeginReceive();

        //byte[] newbyte = new byte[1024];
        //for (int i = 0; i < dataBuffer.Length; i++)
        //{
        //    if (i > 25) break;
        //    newbyte[i] = dataBuffer[i];
        //    Console.WriteLine(dataBuffer[i]);
        //}
        //Console.WriteLine("msgLength: " + msgLength + "  data: " + BitConverter.ToInt32(newbyte,0));//Encoding.UTF8.GetString(dataBuffer)
        //Console.WriteLine("msgLength: " + msgLength + "  dataLength:" + BitConverter.ToInt32(dataBuffer, 4) + "  data: " + Encoding.UTF8.GetString(dataBuffer, 0, 21));
    }

    public void ReciveCallBack(IAsyncResult ar)
    {
        
        try
        {
            if (m_Socket == null || m_Socket.Connected == false) return;
            Debug.Log(m_Socket.EndReceive(ar));
            //得到读取的消息数据的长度
            int count = m_Socket.EndReceive(ar);
            if (count <= 0) { Console.WriteLine("接收的数据错误");return; }
            m_MSG.ReadMessage(count, ProcessMSG);
            //继续接收数据
            ReceiveMSG();
        }
        catch (Exception e)
        {
            Debug.Log("ReciveCallBack Error: " + e);
        }
    }


    /// <summary>
    /// 信息处理
    /// </summary>
    public void ProcessMSG(ActionCode acCode, string data)
    {
        Debug.Log("接收到服务器数据:" +"act: "+acCode +" "+  data);
        m_MC.ProcessResponseMessage(acCode,data);
    }



    void SendHeartBeat()
    {
        while (true)
        {

            try
            {
                Thread.Sleep(2000);
                //byte[] hb = m_MSG.PackMessage(RequestCode.HeartBeat,ActionCode.HeartBeat,"HeartBeat");
                SendRequest(RequestCode.HeartBeat, ActionCode.HeartBeat, "HeartBeat");
                Debug.Log("心跳包发送中");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public void Close()
    {
        hbThread.Abort();
    }

 

}
	

