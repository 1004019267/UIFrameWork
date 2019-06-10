/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     UnityHelper.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion：2017.2.2f1
 *Date:         2019-05-10
 *Description:  Unity帮助脚本
 * 提供程序用户一些常用功能方法实现，方便程序员快速开发
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrameWork
{
    public class UnityHelper
    {
        /// <summary>
        /// 递归查找子结点
        /// </summary>
        /// <returns></returns>
        public static Transform Find(Transform parent, string childName)
        {
            Transform child = null;

            child = parent.transform.Find(childName);
            if (child == null)
            {
                //直接是所有子物体对象
                foreach (Transform item in parent)
                {
                    child = Find(item, childName);
                    if (child != null)
                        return child;
                }
            }
            return child;
        }

        /// <summary>
        /// 获取子节点对象的脚本 限定范围游戏对象的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static T GetComponent<T>(Transform parent, string childName) where T : Component
        {
            Transform child = null;
            child = Find(parent, childName);

            return child?.gameObject.GetComponent<T>();
        }

        /// <summary>
        /// 给子节点添加脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public static T AddComponent<T>(Transform parent, string childName) where T : Component
        {
            Transform child = null;
            child = Find(parent, childName);

            //如果有相同的脚本,则先删除
            T[] componentScriptsArray = child.GetComponents<T>();
            for (int i = 0; i < componentScriptsArray.Length; i++)
            {
                GameObject.Destroy(componentScriptsArray?[i]);
            }
            return child?.gameObject.AddComponent<T>();
        }

        /// <summary>
        /// 给子节点设置父对象
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public static void SetParent(Transform parent, Transform child)
        {
            //如果为真，则修改父级相对位置、比例和旋转，以便
            //对象保持与以前相同的世界空间位置、旋转和缩放。
            child.SetParent(parent, false);
            child.localPosition = Vector3.zero;
            child.localScale = Vector3.one;
            child.localEulerAngles = Vector3.zero;
        }

    }
}