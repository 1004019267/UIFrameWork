/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     SysDefine.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion：2017.2.2f1
 *Date:         2019-05-10
 *Description:   UI框架核心参数
 * 1.系统常量
 * 2.全局性方法
 * 3.系统枚举类型
 * 4.委托定义
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIFrameWork
{
    #region 系统枚举类型
    /// <summary>
    /// UI窗体(位置)类型
    /// </summary>
    public enum UIFormType
    {
        /// <summary>
        /// 普通窗体
        /// </summary>//一般是全屏窗体
        Normal,
        /// <summary>
        /// 固定窗体
        /// </summary>//一般是固定不动的 某一方向的一组图标
        Fixed,
        /// <summary>
        /// 弹出窗体
        /// </summary>
        PopUp,
    }

    /// <summary>
    /// UI窗体的显示类型
    /// </summary>
    public enum UIFormShowMode
    {
        /// <summary>
        /// 普通  
        /// </summary>//同一层级叠加
        Normal,
        /// <summary>
        /// 反向切换 
        /// </summary>//按照相反方向切换过去 原路弹回来
        ReverseChange,
        /// <summary>
        /// 隐藏其他 
        /// </summary>//覆盖掉其他窗口
        HideOther
    }

    /// <summary>
    /// UI窗体透明度类型
    /// </summary>
    public enum UIFormLucenyType
    {
        /// <summary>
        /// 完全透明，不能穿透
        /// </summary>
        Lucency,
        /// <summary>
        /// 半透明，不能穿透
        /// </summary>
        Translucence,
        /// <summary>
        /// 低透明度，不能穿透
        /// </summary>
        ImPenetrable,
        /// <summary>
        /// 透明可以穿透
        /// </summary>
        Pentrate
    }
    #endregion  
    public class SysDefine
    {
        // 路径常量
        public const string canvasPath = "Canvas";
        public const string UIConfigPath = "UIFormsConfigInfo";
        public const string SysConfigInfoPath = "SysConfigInfo";

        //标签常量
        public const string canvasTag = "Canvas";
        public const string UICameraTag = "UICamera";
        // 节点常量
        public const string normalNode = "Normal";
        public const string fixedNode = "Fixed";
        public const string popUpNode = "PopUp";
        public const string scriptsMgrNode = "ScriptsMgr";
        //遮罩管理器里 透明度常量
        public const float lucency = 0;
        public const float translucence = 0.2f;
        public const float imPenetrable = 0.8f;
        //摄像机层深的常量

        //全局性方法

        //委托的定义
    }
}