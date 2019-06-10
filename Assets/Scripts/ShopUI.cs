/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     ShopUI.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-12
 *Description:   商店UI
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrameWork;
public class ShopUI : BaseUI
{
    private void Awake()
    {
        currentUIType.type = UIFormType.PopUp;
        currentUIType.lucenyType = UIFormLucenyType.ImPenetrable;
        currentUIType.mode = UIFormShowMode.ReverseChange;

        RigisterBtnOnClick("btnClose", go => { CloseUI(); });

        //注册武器
        RigisterBtnOnClick("pink", go => {
            OpenUI(ProConst.PropDataUI);
            SendMsg("Props","pink",new List<string> { });
        });
        RigisterBtnOnClick("purple", go => {
            OpenUI(ProConst.PropDataUI);
            SendMsg("Props", "purple", new string[] { "紫色石头道具详情", "属性" });
        });
        RigisterBtnOnClick("green", go => {
            OpenUI(ProConst.PropDataUI);
            SendMsg("Props", "green", new string[] { "绿色石头道具详情", "属性" });
        });
        RigisterBtnOnClick("blue", go => {
            OpenUI(ProConst.PropDataUI);
            SendMsg("Props", "blue", new string[] { "蓝色石头道具详情", "属性" });
        });
    }
}
