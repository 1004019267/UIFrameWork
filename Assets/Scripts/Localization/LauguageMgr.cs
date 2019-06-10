/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     LauguageMgr.cs
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
    public class LauguageMgr : MonoBehaviour
    {
        public static LauguageMgr instance;
        Dictionary<string, string> dicLauguageCache = new Dictionary<string, string>();

        void Awake()
        {
            instance = this;
            Init();
        }

        /// <summary>
        /// 显示文本信息
        /// </summary>
        /// <param name="lauguageID"></param>
        /// <returns></returns>
        public string ShowText(string lauguageID)
        {
            string strQueryResult=string.Empty;
            if (string.IsNullOrEmpty(lauguageID)) return null;
            //查询处理
            if (dicLauguageCache!=null&&dicLauguageCache.Count>=1)
            {
                dicLauguageCache.TryGetValue(lauguageID, out strQueryResult);
                if (!string.IsNullOrEmpty(strQueryResult))
                {
                    return strQueryResult;
                }
            }
            Debug.Log($"{GetType()}/ShowText()/Query is Null! Parameter lauguageID {lauguageID}");
            return null;
        }

        private void Init()
        {
            IConfigManager config = new ConfigManagerByJson("LauguageJSONConfig");
            dicLauguageCache = config?.AppSetting;
        }
    }
}