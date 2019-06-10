/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     LoginUI.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-11
 *Description:   ��¼����
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
        //���崰������ (Ĭ����ֵ)
        currentUIType.type = UIFormType.Normal;
        currentUIType.mode = UIFormShowMode.Normal;
        currentUIType.lucenyType = UIFormLucenyType.Lucency;

        //ע�ᰴť
        RigisterBtnOnClick("btnLogin", go =>
        {
            //ǰ̨���ߺ�̨����û�����������

            //����ɹ������¼��һ������
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
