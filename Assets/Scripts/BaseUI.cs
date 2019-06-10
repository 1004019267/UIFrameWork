/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     BaseUI.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion：2017.2.2f1
 *Date:         2019-05-10
 *Description:  UI窗体父类
 *定义UI窗体的父类
 * 有四个生命周期
 * 1.Display显示状态
 * 2.Hiding隐藏状态
 * 3.ReDisplay再显示状态
 * 4.Freeze冻结状态 就是弹出窗体后面的窗体冻结
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrameWork
{
    //窗体类型
    public class UIType
    {
        /// <summary>
        /// 是否清空"栈集合" 反向切换
        /// </summary>
        public bool isClearStack = false;
        /// <summary>
        /// UI窗体(位置)类型
        /// </summary>
        public UIFormType type = UIFormType.Normal;
        /// <summary>
        ///UI窗体显示类型
        /// </summary>
        public UIFormShowMode mode = UIFormShowMode.Normal;
        /// <summary>
        /// UI窗体透明度类型
        /// </summary>
        public UIFormLucenyType lucenyType = UIFormLucenyType.Lucency;

    }

    public class BaseUI : MonoBehaviour
    {
        public UIType currentUIType { get; set; } = new UIType();

        #region 窗体的四种状态(生命周期)
        /// <summary>
        /// 显示状态
        /// </summary>
        public virtual void ActiveTrue()
        {
            gameObject.SetActive(true);
            //设置弹出窗体调用
            if (currentUIType.type==UIFormType.PopUp)
            {
                UIMaskMgr.instance.SetMaskWnd(gameObject.transform, currentUIType.lucenyType);
            }
        }
        /// <summary>
        /// 隐藏状态
        /// </summary>
        public virtual void ActiveFalse()
        {
            gameObject.SetActive(false);
            if (currentUIType.type == UIFormType.PopUp)
            {
                UIMaskMgr.instance.CancleMaskWnd();
            }
        }
        /// <summary>
        /// 重新显示状态
        /// </summary>
        public virtual void ReActiveTrue()
        {
            gameObject.SetActive(true);
            if (currentUIType.type == UIFormType.PopUp)
            {
                UIMaskMgr.instance.SetMaskWnd(gameObject.transform, currentUIType.lucenyType);
            }
        }

        /// <summary>
        /// 冻结状态
        /// </summary>
        public virtual void Freeze()
        {
            gameObject.SetActive(true);
        }
        #endregion

        #region 封装子类常用方法
        /// <summary>
        /// 注册按钮事件 
        /// </summary>
        protected void RigisterBtnOnClick(string btnName, EventTriggerListener.VoidDelegate del)
        {
            Transform btn = UnityHelper.Find(gameObject.transform, btnName);
            EventTriggerListener.Get(btn?.gameObject).onClick = del;
        }

        protected void RigisterBtnOnClick(Transform btn, EventTriggerListener.VoidDelegate del)
        {          
            EventTriggerListener.Get(btn?.gameObject).onClick = del;
        }
        /// <summary>
        /// 打开UI窗体
        /// </summary>
        /// <param name="UIName"></param>
        protected void OpenUI(string UIName)
        {
            UIManager.instance.ShowUI(UIName);
        }

        /// <summary>
        /// 关闭UI窗体
        /// </summary>
        protected void CloseUI()
        {
            string UIName;
            //int intPos = -1;
            //命名空间+类名
            UIName = GetType().ToString();
            //查询第一次出现在这在第几位
            //intPos = UIName.IndexOf('.');

            //if (intPos != -1)
            //{
            //    UIName = UIName.Substring(intPos + 1);
            //}

            UIManager.instance.CloseUI(UIName);

        }

        /// <summary>
        /// 发送方法
        /// </summary>
        /// <param name="name">消息类型</param>
        /// <param name="msgName">消息名称</param>
        /// <param name="msgContent">消息内容</param>
        protected void SendMsg(string msgType,string msgName,object msgContent)
        {
            KeyValuesUpdate kv = new KeyValuesUpdate(msgName, msgContent);
            MessageCenter.SendMessage(msgType, kv);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="hander"></param>
        protected void ReceiveMsg(string msgType,MessageCenter.DelMessageDelivery hander)
        {
            MessageCenter.AddMsgListener(msgType,hander);
        }

        /// <summary>
        /// 显示语言
        /// </summary>
        /// <param name="id"></param>
        protected string Show(string id)
        {         
            return LauguageMgr.instance.ShowText(id);
        }
        #endregion
    }
}