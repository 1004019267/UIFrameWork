/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     UIMaskMgr.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-12
 *Description:   UI遮罩管理器
 * 负责"弹出窗体"的实现
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UIFrameWork
{
    public class UIMaskMgr : MonoBehaviour
    {
        public static UIMaskMgr instance;
        //UI根节点对象
        Transform traRoot;
        //UI脚本节点对象
        Transform traUISprites;
        //顶层面板
        Transform TopPanle;
        //遮罩面板
        Transform MaskPanle;
        //UI相机
        Camera UICamera;
        //UI相机初始层深
        float OriginalUICameraDepth;

        private void Start()
        {
            instance = this;
            traRoot = GameObject.FindGameObjectWithTag(SysDefine.canvasTag).transform;
            traUISprites = UnityHelper.Find(traRoot, SysDefine.scriptsMgrNode);
            //把脚本节点添加到总脚本的子节点
            UnityHelper.SetParent(traUISprites, gameObject.transform);
            //得到顶层面版 遮罩面板
            TopPanle = traRoot;
            MaskPanle = UnityHelper.Find(traRoot, "UIMaskPanel");
            //得到UI摄像机原始的层深
            UICamera = GameObject.FindGameObjectWithTag(SysDefine.UICameraTag).GetComponent<Camera>();
            if (UICamera != null)
            {
                OriginalUICameraDepth = UICamera.depth;
            }
            else
            {
                Debug.Log($"{GetType()} UICamera==null Please Check!");
            }

        }

        /// <summary>
        /// 设置遮罩状态
        /// </summary>
        /// <param name="ActiveUI">需要显示的UI窗体</param>
        /// <param name="type"></param>
        public void SetMaskWnd(Transform activeUI, UIFormLucenyType type = UIFormLucenyType.Lucency)
        {
            //顶层窗体下移 防止多个canvas遮挡
            TopPanle.SetAsLastSibling();
            //启用遮罩窗体以及设置透明度
            float alpha=0;
            switch (type)
            {
                case UIFormLucenyType.Lucency:
                    MaskPanle.gameObject.SetActive(true);
                    alpha =SysDefine.lucency;
                    break;
                case UIFormLucenyType.Translucence:
                    MaskPanle.gameObject.SetActive(true);
                    alpha = SysDefine.translucence;
                    break;
                case UIFormLucenyType.ImPenetrable:
                    MaskPanle.gameObject.SetActive(true);
                    alpha = SysDefine.imPenetrable;
                    break;
                case UIFormLucenyType.Pentrate:
                    if (MaskPanle.gameObject.activeInHierarchy)
                    {
                        MaskPanle.gameObject.SetActive(false); 
                    }
                    break;
            }
            MaskPanle.GetComponent<Image>().color = new Color(1, 1, 1, alpha) ;

            //遮罩窗体下移
            TopPanle.SetAsLastSibling();
            //显示窗体的下移
            activeUI.SetAsLastSibling();
            //增加当前UI摄像机层深(保证当前摄像机为最前显示)
            if (UICamera != null)
            {
                OriginalUICameraDepth = UICamera.depth + 100;
            }
        }
        /// <summary>
        /// 取消遮罩状态
        /// </summary>
        public void CancleMaskWnd()
        {
            //顶层窗体上移
            TopPanle.transform.SetAsFirstSibling();
            //禁用遮罩窗体 如果激活则禁用
            if (MaskPanle.gameObject.activeInHierarchy)
            {
                MaskPanle.gameObject.SetActive(false);
            }
            if (UICamera != null)
            {
                //恢复层深
                UICamera.depth = OriginalUICameraDepth;
            }
        }
    }
}
