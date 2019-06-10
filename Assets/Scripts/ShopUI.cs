/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     ShopUI.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-12
 *Description:   �̵�UI
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

        //ע������
        RigisterBtnOnClick("pink", go => {
            OpenUI(ProConst.PropDataUI);
            SendMsg("Props","pink",new List<string> { });
        });
        RigisterBtnOnClick("purple", go => {
            OpenUI(ProConst.PropDataUI);
            SendMsg("Props", "purple", new string[] { "��ɫʯͷ��������", "����" });
        });
        RigisterBtnOnClick("green", go => {
            OpenUI(ProConst.PropDataUI);
            SendMsg("Props", "green", new string[] { "��ɫʯͷ��������", "����" });
        });
        RigisterBtnOnClick("blue", go => {
            OpenUI(ProConst.PropDataUI);
            SendMsg("Props", "blue", new string[] { "��ɫʯͷ��������", "����" });
        });
    }
}
