/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     IConfigManager.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-12
 *Description:   ͨ�����ù������ӿ�
 * ����"��ֵ��"�����ļ�ͨ�ý���
 *History:
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrameWork
{
    public interface IConfigManager
    {
        /// <summary>
        /// ֻ������,Ӧ������
        /// �õ���ֵ�Լ�������
        /// </summary>
        Dictionary<string, string> AppSetting { get; }

        /// <summary>
        /// �õ������ļ�(AppSetting)�������
        /// </summary>
        /// <returns></returns>
        int GetAppSettingMaxNumber();
    }

    [Serializable]
    internal class KeyValueInfo
    {
        public List<KeyValueNode> ConfigInfo;
    }

    [Serializable]
    internal class KeyValueNode
    {
        public string Key;
        public string Value;
    }
}