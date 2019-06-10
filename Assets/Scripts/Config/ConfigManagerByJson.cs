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
        /// 键值对集合
        /// </summary>
        public Dictionary<string, string> AppSetting { get; } = new Dictionary<string, string>();


        public ConfigManagerByJson(string path)
        {
            InitAndAnalysisJson(path);
        }
        /// <summary>
        /// 得到AppSetting最大数值
        /// </summary>
        /// <returns></returns>
        public int GetAppSettingMaxNumber()
        {
            return (AppSetting != null && AppSetting.Count >= 1) ?
                AppSetting.Count : 0;
        }

        /// <summary>
        /// 初始化Json数据加载到集合中
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
                //自定义异常
                throw new AnalysisJsonException($"{GetType()}/InitAndAnalysisJson/Json Analysis Exception! Parameter Path{path}");
            }
            foreach (var item in keyValueInfo.ConfigInfo)
            {
                AppSetting.Add(item.Key, item.Value);
            }
        }
    }
}
