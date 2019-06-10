/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     MessageCenter.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-12
 *Description:   ��Ϣ��������
 * ����UI���������UI����֮�����ݴ�ֵ
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrameWork
{
    /// <summary>
    /// ��ֵ�� ���ί��ʵ��ί�д���
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
        /// ��Ϣ���Ļ���ϼ�
        /// </summary>
        public static Dictionary<string, DelMessageDelivery> dicMsgs = new Dictionary<string, DelMessageDelivery>();

        /// <summary>
        /// �����Ϣ����
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
        /// ȡ����Ϣ����
        /// </summary>
        public static void RemoveMsgListener(string msgType, DelMessageDelivery hander)
        {
            if (dicMsgs.ContainsKey(msgType))
            {
                dicMsgs[msgType] -= hander;
            }
        }

        /// <summary>
        /// ȡ��������Ϣ����
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
