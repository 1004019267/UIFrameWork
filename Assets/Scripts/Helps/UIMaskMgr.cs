/**
 *Copyright(C) 2019 by DefaultCompany
 *All rights reserved.
 *FileName:     UIMaskMgr.cs
 *Author:       why
 *Version:      1.0
 *UnityVersion:2018.3.9f1
 *Date:         2019-05-12
 *Description:   UI���ֹ�����
 * ����"��������"��ʵ��
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UIFrameWork
{
    public class UIMaskMgr : MonoBehaviour
    {
        public static UIMaskMgr instance;
        //UI���ڵ����
        Transform traRoot;
        //UI�ű��ڵ����
        Transform traUISprites;
        //�������
        Transform TopPanle;
        //�������
        Transform MaskPanle;
        //UI���
        Camera UICamera;
        //UI�����ʼ����
        float OriginalUICameraDepth;

        private void Start()
        {
            instance = this;
            traRoot = GameObject.FindGameObjectWithTag(SysDefine.canvasTag).transform;
            traUISprites = UnityHelper.Find(traRoot, SysDefine.scriptsMgrNode);
            //�ѽű��ڵ���ӵ��ܽű����ӽڵ�
            UnityHelper.SetParent(traUISprites, gameObject.transform);
            //�õ�������� �������
            TopPanle = traRoot;
            MaskPanle = UnityHelper.Find(traRoot, "UIMaskPanel");
            //�õ�UI�����ԭʼ�Ĳ���
            UICamera = GameObject.FindGameObjectWithTag(SysDefine.UICameraTag).GetComponent<Camera>();
            if (UICamera != null)
            {
                OriginalUICameraDepth = UICamera.depth;
            }
            else
            {
                Debug.Log($"{GetType()} UICamera==null Please Check!");
            }

        }

        /// <summary>
        /// ��������״̬
        /// </summary>
        /// <param name="ActiveUI">��Ҫ��ʾ��UI����</param>
        /// <param name="type"></param>
        public void SetMaskWnd(Transform activeUI, UIFormLucenyType type = UIFormLucenyType.Lucency)
        {
            //���㴰������ ��ֹ���canvas�ڵ�
            TopPanle.SetAsLastSibling();
            //�������ִ����Լ�����͸����
            float alpha=0;
            switch (type)
            {
                case UIFormLucenyType.Lucency:
                    MaskPanle.gameObject.SetActive(true);
                    alpha =SysDefine.lucency;
                    break;
                case UIFormLucenyType.Translucence:
                    MaskPanle.gameObject.SetActive(true);
                    alpha = SysDefine.translucence;
                    break;
                case UIFormLucenyType.ImPenetrable:
                    MaskPanle.gameObject.SetActive(true);
                    alpha = SysDefine.imPenetrable;
                    break;
                case UIFormLucenyType.Pentrate:
                    if (MaskPanle.gameObject.activeInHierarchy)
                    {
                        MaskPanle.gameObject.SetActive(false); 
                    }
                    break;
            }
            MaskPanle.GetComponent<Image>().color = new Color(1, 1, 1, alpha) ;

            //���ִ�������
            TopPanle.SetAsLastSibling();
            //��ʾ���������
            activeUI.SetAsLastSibling();
            //���ӵ�ǰUI���������(��֤��ǰ�����Ϊ��ǰ��ʾ)
            if (UICamera != null)
            {
                OriginalUICameraDepth = UICamera.depth + 100;
            }
        }
        /// <summary>
        /// ȡ������״̬
        /// </summary>
        public void CancleMaskWnd()
        {
            //���㴰������
            TopPanle.transform.SetAsFirstSibling();
            //�������ִ��� ������������
            if (MaskPanle.gameObject.activeInHierarchy)
            {
                MaskPanle.gameObject.SetActive(false);
            }
            if (UICamera != null)
            {
                //�ָ�����
                UICamera.depth = OriginalUICameraDepth;
            }
        }
    }
}
