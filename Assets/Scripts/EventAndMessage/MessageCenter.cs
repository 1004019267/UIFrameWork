/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     MessageCenter.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-12
 *Description:   消息传递中心
 * 负责UI框架中所有UI窗体之间数据传值
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrameWork
{
    /// <summary>
    /// 键值对 配合委托实现委托传递
    /// </summary>
    public class KeyValuesUpdate
    {
        public string Key { get; }
        public object Values { get; }

        public KeyValuesUpdate(string key, object values)
        {
            Key = key;
            Values = values;
        }
    }

    public class MessageCenter
    {
        public delegate void DelMessageDelivery(KeyValuesUpdate kv);
        /// <summary>
        /// 消息中心缓存合集
        /// </summary>
        public static Dictionary<string, DelMessageDelivery> dicMsgs = new Dictionary<string, DelMessageDelivery>();

        /// <summary>
        /// 添加消息监听
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="hander"></param>
        public static void AddMsgListener(string msgType, DelMessageDelivery hander)
        {
            if (!dicMsgs.ContainsKey(msgType))
            {
                dicMsgs.Add(msgType, null);
            }
            dicMsgs[msgType] += hander;
        }

        /// <summary>
        /// 取消消息监听
        /// </summary>
        public static void RemoveMsgListener(string msgType, DelMessageDelivery hander)
        {
            if (dicMsgs.ContainsKey(msgType))
            {
                dicMsgs[msgType] -= hander;
            }
        }

        /// <summary>
        /// 取消所有消息监听
        /// </summary>
        public static void RemoveAllMsgListener()
        {
            dicMsgs?.Clear();
        }

        public static void SendMessage(string msgType, KeyValuesUpdate kv)
        {
            if (dicMsgs.TryGetValue(msgType, out DelMessageDelivery del))
            {
                del?.Invoke(kv);
            }
        }
    }

}
