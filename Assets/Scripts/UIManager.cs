/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     UIManager.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion：2017.2.2f1
 *Date:         2019-05-10
 *Description:   UI管理器
 * 是整个UI框架的核心，用户程序通过本脚本，来实现框架绝大多数功能
 * 原则
 * 低耦合，高聚合
 * 方法单一职责
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIFrameWork
{
    public class UIManager : MonoBehaviour
    {

        public static UIManager instance;
        /// <summary>
        /// UI窗体预设路径 <窗口名字，路径>
        /// </summary>
        Dictionary<string, string> UIPaths = new Dictionary<string, string>();
        /// <summary>
        /// 缓存所有UI窗体
        /// </summary>
        Dictionary<string, BaseUI> cacheUIs = new Dictionary<string, BaseUI>();
        /// <summary>
        /// 当前显示的UI窗体
        /// </summary>
        Dictionary<string, BaseUI> showUIs = new Dictionary<string, BaseUI>();
        /// <summary>
        /// 具备 "反向切换"窗体的管理
        /// </summary>
        Stack<BaseUI> staUIs = new Stack<BaseUI>();
        /// <summary>
        /// UI根节点
        /// </summary>
        Transform traRoot;
        /// <summary>
        /// 全屏幕显示的节点
        /// </summary>
        Transform traNormal;
        /// <summary>
        /// 固定显示的节点
        /// </summary>
        Transform traFixed;
        /// <summary>
        /// 弹出节点
        /// </summary>
        Transform traPopUp;
        /// <summary>
        /// UI脚本管理节点
        /// </summary>
        Transform traUISprites;

        /// <summary>
        /// 初始化核心数据，加载"UI窗体路径"到集合中
        /// </summary>
        private void Awake()
        {
            instance = this;
            InitRootCanvasLoading();
            //得到UI根节点、全屏节点、固定节点、弹出节点
            traRoot = GameObject.FindGameObjectWithTag(SysDefine.canvasTag).transform;
            traNormal = UnityHelper.Find(traRoot, SysDefine.normalNode);
            traFixed = UnityHelper.Find(traRoot, SysDefine.fixedNode);
            traPopUp = UnityHelper.Find(traRoot, SysDefine.popUpNode);
            traUISprites = UnityHelper.Find(traRoot, SysDefine.scriptsMgrNode);
            //把本脚本作为"根UI窗体"的子节点 true世界坐标 false局部坐标
            this.gameObject.transform.SetParent(traUISprites, false);
            //"rootUI窗体"在场景转换时候，不允许被销毁
            DontDestroyOnLoad(traRoot);
            //初始化"UI窗体"路径信息         
            InitUIPathData();
        }
        /// <summary>
        /// 打开/显示UI窗体
        /// </summary>
        public void ShowUI(string UIName)
        {
            BaseUI baseUI = null;
            if (string.IsNullOrEmpty(UIName))
                return;


            baseUI = LoadUIToCacheUIs(UIName);

            if (baseUI == null)
                return;

            //是否清空"栈集合"中得数据
            if (baseUI.currentUIType.isClearStack)
            {
                if (!ClearStack())
                    Debug.Log($"栈中数据没有成功清空请检查参数{UIName}");
            }

            //根据不同的UI窗体的显示模式，分别作不同的加载处理
            switch (baseUI.currentUIType.mode)
            {
                case UIFormShowMode.Normal:
                    LoadUIToShowUIs(UIName);
                    break;
                case UIFormShowMode.ReverseChange:
                    PushUIToStack(UIName);
                    break;
                case UIFormShowMode.HideOther:
                    EnterUIAndHideOther(UIName);
                    break;
            }
        }

        /// <summary>
        /// 关闭(返回上一个)窗体
        /// </summary>
        public void CloseUI(string UIName)
        {
            //参数检查
            if (string.IsNullOrEmpty(UIName))
                return;
            //所有UI窗体 没有记录直接返回
            cacheUIs.TryGetValue(UIName, out BaseUI baseUI);
            if (baseUI == null)
                return;

            switch (baseUI.currentUIType.mode)
            {
                case UIFormShowMode.Normal:
                    ExitUI(UIName);
                    break;
                case UIFormShowMode.ReverseChange:
                    PopUIToStack();
                    break;
                case UIFormShowMode.HideOther:
                    ExitUIAndDisPlayOther(UIName);
                    break;
            }

        }
        #region 显示"UI管理器"内部核心数据，测试使用

        /// <summary>
        /// 显示所有UI窗体的数量
        /// </summary>
        /// <returns></returns>
        public int ShowCacheUIsCount()
        {
            return cacheUIs?.Count ?? 0;
        }
        /// <summary>
        /// 显示当前窗体的数量
        /// </summary>
        /// <returns></returns>
        public int ShowShowUIsCount()
        {
            return showUIs?.Count ?? 0;
        }
        /// <summary>
        /// 显示栈窗体的数量
        /// </summary>
        /// <returns></returns>
        public int ShowStaUIsCount()
        {
            return staUIs?.Count ?? 0;
        }
        #endregion

        #region 私有方法 
        /// <summary>
        /// 初始化加载(rootUI窗体)Canvas预制体
        /// </summary>
        private void InitRootCanvasLoading()
        {
            ResourcesMgr.GetInstance().LoadAsset(SysDefine.canvasPath, false);
        }

        /// <summary>
        /// 加载与判断指定的UI窗体的名字，加载到"所有UI窗体"缓存里
        /// </summary>
        /// <param name="UIName"></param>
        /// <returns></returns>
        BaseUI LoadUIToCacheUIs(string UIName)
        {
            //得到就返回 得不到返回null
            cacheUIs.TryGetValue(UIName, out BaseUI baseUI);
            if (baseUI == null)
            {
                baseUI = LoadUI(UIName);
            }
            return baseUI;
        }

        /// <summary>
        /// 根据不同位置信息，加载到root下不同的节点
        /// </summary>
        BaseUI LoadUI(string UIName)
        {
            GameObject UIPrefabs = null;
            BaseUI baseUI = null;

            UIPaths.TryGetValue(UIName, out string uiPath);
            if (!string.IsNullOrEmpty(uiPath))
            {
                UIPrefabs = ResourcesMgr.GetInstance().LoadAsset(uiPath, false);
            }

            if (traRoot != null && UIPrefabs != null)
            {
                baseUI = UIPrefabs.GetComponent<BaseUI>();
                if (baseUI == null)
                {
                    Debug.Log($"baseUI==null,请确认窗口是否有BaseUI脚本，参数:{UIName}");
                    return null;
                }

                switch (baseUI.currentUIType.type)
                {
                    case UIFormType.Normal:
                        UIPrefabs.transform.SetParent(traNormal, false);
                        break;
                    case UIFormType.Fixed:
                        UIPrefabs.transform.SetParent(traFixed, false);
                        break;
                    case UIFormType.PopUp:
                        UIPrefabs.transform.SetParent(traPopUp, false);
                        break;
                }

                UIPrefabs.SetActive(false);
                cacheUIs.Add(UIName, baseUI);
                return baseUI;
            }
            else
            {
                Debug.Log($"traRoot!=null Or UIPrefabs!=null Please Check  参数{UIName}");
            }

            Debug.Log($"出现不可预估的错误 参数{UIName}");
            return null;
        }

        /// <summary>
        /// 把当前窗体加载到当前显示窗体集合中
        /// </summary>
        void LoadUIToShowUIs(string UIName)
        {
            showUIs.TryGetValue(UIName, out BaseUI baseUI);

            if (baseUI != null)
                return;

            cacheUIs.TryGetValue(UIName, out BaseUI baseUIFormCache);

            if (baseUIFormCache != null)
            {
                showUIs.Add(UIName, baseUIFormCache);
                baseUIFormCache.ActiveTrue();
            }

        }

        /// <summary>
        /// UI窗体入栈
        /// </summary>
        void PushUIToStack(string UIName)
        {
            //判断栈中是否有其他的窗体，有则冻结
            if (staUIs.Count > 0)
            {
                BaseUI topUI = staUIs.Peek();
                topUI.Freeze();
            }
            //判断UI所有窗体 是否有指定的UI窗体 有就处理
            cacheUIs.TryGetValue(UIName, out BaseUI baseUI);
            if (baseUI != null)
            {
                baseUI.ActiveTrue();
                //把指定UI窗体，入栈
                staUIs.Push(baseUI);
            }
            else
                Debug.Log($"baseUI==null,请确认窗口是否有BaseUI脚本，参数:{UIName}");
        }

        /// <summary>
        /// 退出指定UI窗体
        /// </summary>
        /// <param name="UIName"></param>
        void ExitUI(string UIName)
        {
            //"正在显示集合"如果没有记录 则直接返回
            showUIs.TryGetValue(UIName, out BaseUI baseUI);
            if (baseUI == null)
                return;
            //指定窗体，标记为隐藏状态,从正在显示集合中移除
            baseUI.ActiveFalse();
            showUIs.Remove(UIName);
        }

        /// <summary>
        /// 反向切换 窗体的出栈处理
        /// </summary>
        void PopUIToStack()
        {
            BaseUI baseUI = staUIs.Pop();
            baseUI.ActiveFalse();
            if (staUIs.Count >= 2)
            {
                //下一个窗体重新显示
                staUIs.Peek().ReActiveTrue();
            }
        }

        /// <summary>
        /// 打开窗体 隐藏其他窗体
        /// </summary>
        /// <param name="UIName"></param>
        void EnterUIAndHideOther(string UIName)
        {
            if (string.IsNullOrEmpty(UIName))
                return;
            showUIs.TryGetValue(UIName, out BaseUI baseUI);
            if (baseUI != null)
                return;
            //把正在显示集合 栈集合都隐藏
            foreach (var item in showUIs)
            {
                item.Value.ActiveFalse();
            }

            foreach (var item in staUIs)
            {
                item.ActiveFalse();
            }

            //把当前窗体加入到"正在显示窗体"集合中,且做显示处理
            cacheUIs.TryGetValue(UIName, out BaseUI baseUIFromAll);

            if (baseUIFromAll != null)
            {
                showUIs.Add(UIName, baseUIFromAll);
                baseUIFromAll.ActiveTrue();
            }
        }

        /// <summary>
        /// 关闭窗体 显示其他窗体
        /// </summary>
        /// <param name="UIName"></param>
        void ExitUIAndDisPlayOther(string UIName)
        {
            if (string.IsNullOrEmpty(UIName))
                return;

            showUIs.TryGetValue(UIName, out BaseUI baseUI);
            if (baseUI == null)
                return;
            //把当前窗口失活移除
            baseUI.ActiveFalse();
            showUIs.Remove(UIName);
            //把正在显示集合 栈集合都激活
            foreach (var item in showUIs)
            {
                item.Value.ReActiveTrue();
            }

            foreach (var item in staUIs)
            {
                item.ReActiveTrue();
            }
        }

        /// <summary>
        /// 是否清空栈集合中数据
        /// </summary>
        /// <returns></returns>
        bool ClearStack()
        {
            if (staUIs != null && staUIs.Count >= 1)
            {
                staUIs.Clear();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 初始化"UI窗体"路径数据
        /// </summary>
        void InitUIPathData()
        {
            IConfigManager configManager = new ConfigManagerByJson(SysDefine.UIConfigPath);
            UIPaths = configManager?.AppSetting;
        }
        #endregion
    }
}