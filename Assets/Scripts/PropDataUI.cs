/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     PropDataUI.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-12
 *Description:   道具详细信息窗体
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFrameWork;
public class PropDataUI : BaseUI
{
    Text nameText;
    private void Awake()
    {

        currentUIType.type = UIFormType.PopUp;
        currentUIType.mode = UIFormShowMode.ReverseChange;
        currentUIType.lucenyType = UIFormLucenyType.Translucence;

        nameText= UnityHelper.Find(gameObject.transform,"Text").GetComponent<Text>();
        ReceiveMsg("Props", hander =>
        {
            if (nameText)
            {
                string[] strArray = hander.Values as string[];
                nameText.text = strArray[0];                
            }
        });

        RigisterBtnOnClick("btnClose", go => CloseUI());
    }
}
