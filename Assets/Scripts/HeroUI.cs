/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     HeroUI.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-12
 *Description:  英雄信息显示 
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrameWork;
public class HeroUI :BaseUI
{

    private void Awake()
    {
        //此窗体固定在主窗体上面显示
        currentUIType.type = UIFormType.Fixed; 

        //
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
