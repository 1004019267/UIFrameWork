/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     SelectUI.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-11
 *Description:   选择英雄窗体
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrameWork;
public class SelectUI :BaseUI
{
    private void Awake()
    {
        //定义窗口性质 (默认数值)
        currentUIType.mode = UIFormShowMode.HideOther;
        //进入主城
        RigisterBtnOnClick("BtnConfirm", go => {
            OpenUI(ProConst.MainUI);
            OpenUI(ProConst.HeroUI);
        });
        //返回方法
        RigisterBtnOnClick("BtnClose", go => CloseUI());
    }

}
