/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     AnalysisJsonException.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-12
 *Description:   专门负责对于Json由于路径错误
 * 或者Json格式错误造成的异常进行捕获
 *History:
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrameWork
{
    public class AnalysisJsonException : Exception
    {
        public AnalysisJsonException() : base() { }
        public AnalysisJsonException(string exceptionMsg) : base(exceptionMsg) { }
    }
}