/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     Text.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-12
 *Description:   
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrameWork;
public class Text1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.ShowUI(ProConst.LoginUI);
    }

}
