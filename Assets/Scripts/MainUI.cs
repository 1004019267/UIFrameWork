/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     MainUI.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-12
 *Description:   Ö÷³¡¾°ÏÔÊ¾
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrameWork;
public class MainUI : BaseUI
{

     void Awake()
    {
        currentUIType.mode = UIFormShowMode.HideOther;

        RigisterBtnOnClick("btnShop", go=>OpenUI(ProConst.ShopUI));
    }
}
