using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UIAnimMadeModelConst
{
    public const string KEY_AnimInfo = "AnimInfo";
}

public class UIAnimMadeModel : UIBaseModel
{
    private Animation curAnim; //当前执行的动画
    public Animation CurAnim
    {
        get
        {
            return curAnim;
        }

        set
        {
            Animation old ;
            if (curAnim==null)
            {
                old = null;
            }
            else
            {
                old = curAnim;
            }            
            curAnim = value;
            ValueChangeArgs ve = new ValueChangeArgs(UIAnimMadeModelConst.KEY_AnimInfo, old, value);
            DispatchValueUpdateEvent(ve);
        }
    }

    private Transform[] nodeList;
    public GameObject headObj;
    private Transform head;
    private int headIndex;

    public void InitAnimInfo()
    {
        nodeList = curAnim.transform.Find("01").GetComponentsInChildren<Transform>();
        for (int i = 0; i < nodeList.Length; i++)
        {
            if (nodeList[i].name == "Head")
            {
                head = nodeList[i];
                headIndex = i;
            }
        }
        TransformExtension.SetParent(headObj.transform, head);
        headObj.transform.localPosition = Vector3.zero;
    }
}

