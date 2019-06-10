/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     SelectUI.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-11
 *Description:   ѡ��Ӣ�۴���
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
        //���崰������ (Ĭ����ֵ)
        currentUIType.mode = UIFormShowMode.HideOther;
        //��������
        RigisterBtnOnClick("BtnConfirm", go => {
            OpenUI(ProConst.MainUI);
            OpenUI(ProConst.HeroUI);
        });
        //���ط���
        RigisterBtnOnClick("BtnClose", go => CloseUI());
    }

}
