/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     LoginUI.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-11
 *Description:   登录窗体
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFrameWork;
public class LoginUI : BaseUI
{
    public Text txtLoginName;
    public Text txtLoginNameByBtn;
    void Awake()
    {
        //定义窗口性质 (默认数值)
        currentUIType.type = UIFormType.Normal;
        currentUIType.mode = UIFormShowMode.Normal;
        currentUIType.lucenyType = UIFormLucenyType.Lucency;

        //注册按钮
        RigisterBtnOnClick("btnLogin", go =>
        {
            //前台或者后台检查用户名称与密码

            //如果成功，则登录下一个窗口
            OpenUI(ProConst.SelectUI);
        });
    }

    void Start()
    {
        string str = Show("LogonSystem");
        if (txtLoginName)
        {
            txtLoginName.text = str;
        }
        if (txtLoginNameByBtn)
        {
            txtLoginNameByBtn.text = str;
        }
    }
}
