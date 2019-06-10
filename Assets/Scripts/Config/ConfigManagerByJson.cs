/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     ConfigManagerByJson.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-12
 *Description:   
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrameWork
{
    public class ConfigManagerByJson : IConfigManager
    {
        /// <summary>
        /// ��ֵ�Լ���
        /// </summary>
        public Dictionary<string, string> AppSetting { get; } = new Dictionary<string, string>();


        public ConfigManagerByJson(string path)
        {
            InitAndAnalysisJson(path);
        }
        /// <summary>
        /// �õ�AppSetting�����ֵ
        /// </summary>
        /// <returns></returns>
        public int GetAppSettingMaxNumber()
        {
            return (AppSetting != null && AppSetting.Count >= 1) ?
                AppSetting.Count : 0;
        }

        /// <summary>
        /// ��ʼ��Json���ݼ��ص�������
        /// </summary>
        void InitAndAnalysisJson(string path)
        {
            TextAsset configInfo = null;
            KeyValueInfo keyValueInfo = null;
            if (string.IsNullOrEmpty(path))
                return;

            try
            {
                configInfo = Resources.Load<TextAsset>(path);
                keyValueInfo = JsonUtility.FromJson<KeyValueInfo>(configInfo.text);
            }
            catch (System.Exception)
            {
                //�Զ����쳣
                throw new AnalysisJsonException($"{GetType()}/InitAndAnalysisJson/Json Analysis Exception! Parameter Path{path}");
            }
            foreach (var item in keyValueInfo.ConfigInfo)
            {
                AppSetting.Add(item.Key, item.Value);
            }
        }
    }
}
