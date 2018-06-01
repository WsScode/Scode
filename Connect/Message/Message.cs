using Common;
using System.Linq;
using System.Text;
using System;
using UnityEngine;

public class Message  {
    private byte[] m_MsgData = new byte[10240];
    private int m_StartIndex = 0;

    public byte[] M_MsgData { get { return m_MsgData; } }
    public int M_StartIndex { get { return m_StartIndex; } }
    public int m_RemainSize { get { return m_MsgData.Length - m_StartIndex; } }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="msgAmount"> 数据长度</param>
    /// <param name="processCallBack">信息处理委托方法</param>
    public void ReadMessage(int msgAmount, Action<ActionCode, string> processCallBack)
    {
        m_StartIndex += msgAmount;
        while (true)
        {
            if (m_StartIndex <= 4) return;
            int count = BitConverter.ToInt32(m_MsgData, 0);
            if ((m_StartIndex - 4) >= count)
            {
                ActionCode ac = (ActionCode)BitConverter.ToInt32(m_MsgData, 4);
                string s = Encoding.UTF8.GetString(m_MsgData, 8, count - 4);
                processCallBack(ac, s); 
                Array.Copy(m_MsgData, 4, m_MsgData, 0, m_StartIndex - 4 - count);
                m_StartIndex -= (count + 4);
            }
        }

    }


    public byte[] PackMessage(RequestCode request, ActionCode action, string data)
    {
        byte[] requestBytes = BitConverter.GetBytes((int)request);
        byte[] actionBytes = BitConverter.GetBytes((int)action);  
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int amount = requestBytes.Length + actionBytes.Length + dataBytes.Length;
        byte[] amountBytes = BitConverter.GetBytes(amount);
        byte[] bytes = amountBytes.Concat(requestBytes).ToArray().Concat(actionBytes).ToArray().Concat(dataBytes).ToArray();
        
        return bytes;
    }

}
