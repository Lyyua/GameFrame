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
            Animation old;
            if (curAnim == null)
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

    private GameObject headObj = null;
    public GameObject HeadObj
    {
        get
        {
            if (headObj == null)
            {
                headObj = GameObject.FindGameObjectWithTag("Head");
            }
            return headObj;
        }

        set
        {
            headObj = value;
        }
    }

    private Transform modelRoot = null;
    public Transform ModelRoot
    {
        get
        {
            if (modelRoot == null)
            {
                modelRoot = GameObject.FindGameObjectWithTag("ModelRoot").transform;
            }
            return modelRoot;
        }

        set
        {
            modelRoot = value;
        }
    }

    private List<Nodes[]> animList = new List<Nodes[]>();
    private List<Nodes[]> backAnimList = new List<Nodes[]>();
    private Transform[] nodeList;
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
        TransformExtension.SetParent(HeadObj.transform, head);
        HeadObj.transform.localPosition = Vector3.zero;
    }

    public void PlayAnim(int type)
    {
        if (type == 0)
        {
            curAnim.wrapMode = WrapMode.Once;
        }
        else
        {
            curAnim.wrapMode = WrapMode.Loop;
        }
        curAnim.Play();
    }

    public void AnimStop()
    {
        if (curAnim != null)
        {
            curAnim.Stop();
        }
    }

    public void SetAnimList(List<Nodes[]> list)
    {
        animList = list;
    }

    public void AnimListClear()
    {
        animList.Clear();
        backAnimList = null;
    }

    public void WriteInfo()
    {
        //身体所有节点信息保存容器
        //List<Nodes> nodesInfoList = new List<Nodes>();
        Nodes[] nodesInfoList = new Nodes[nodeList.Length];
        for (int i = 0; i < nodeList.Length; i++)
        {
            //位置 欧拉角结构体
            Nodes nodes = new Nodes();
            nodes.SetVector3(nodeList[i].localPosition.x, nodeList[i].localPosition.y, nodeList[i].localPosition.z);
            nodes.SetEuler(nodeList[i].localEulerAngles.x, nodeList[i].localEulerAngles.y, nodeList[i].localEulerAngles.z);

            nodesInfoList[i] = nodes;
        }
        //动画容器
        animList.Add(nodesInfoList);
    }

    public bool NodeAnimPlay(ref int index)
    {
        Nodes[] tempList = animList[index];
        //List<Nodes> tempList = all[replayIndex];
        for (int i = 0; i < tempList.Length; i++)
        {
            nodeList[i].localPosition = tempList[i].GetVector3();
            nodeList[i].localEulerAngles = tempList[i].GetEuler();
        }
        if (index == animList.Count - 1) return true;
        return false;
    }

    public void AnimListCut(int start, int end)
    {
        if (backAnimList == null)
        {
            backAnimList = animList;
        }
        if (start < 0 || start > backAnimList.Count - 1)
        {
            return;
        }
        if (end < 0 || end > backAnimList.Count - 1)
        {
            return;
        }
        animList = new List<Nodes[]>();
        for (int i = start; i < backAnimList.Count; i++)
        {
            animList.Add(backAnimList[i]);
            if (i == end)
            {
                return;
            }
        }
    }

    public void CurFramecCut(int index)
    {
        if (backAnimList == null)
        {
            backAnimList = animList;
        }

        animList = new List<Nodes[]>();
        animList.Add(backAnimList[index]);
    }
    public string AnimListToString()
    {
        Debug.Log("导出数据: 一共" + animList.Count);
        return LitJson.JsonMapper.ToJson(animList);
    }
    Vector3 curFrameHeadInfoPos = new Vector3(999, 999, 999);
    Vector3 curFrameHeadInfoEuler = new Vector3(999, 999, 999);

    public void InitCurFrameInfo()
    {
        curFrameHeadInfoPos = head.localPosition;
        curFrameHeadInfoEuler = head.localEulerAngles;
    }

    public void SetHeadLocalPos(Vector3 offset)
    {
        if (curFrameHeadInfoPos == new Vector3(999, 999, 999)) return;
        head.localPosition = curFrameHeadInfoPos + offset;
    }

    public void SetHeadLocalEuler(Vector3 offset)
    {
        if (curFrameHeadInfoEuler == new Vector3(999, 999, 999)) return;
        head.localEulerAngles = curFrameHeadInfoEuler + offset;
    }

    public void SetHeadNodeInfo(int index)
    {
        if (animList.Count == 0)
        {
            return;
        }
        Nodes nodes = new Nodes();
        nodes.SetVector3(head.localPosition.x, head.localPosition.y, head.localPosition.z);
        nodes.SetEuler(head.localEulerAngles.x, head.localEulerAngles.y, head.localEulerAngles.z);
        animList[index][headIndex] = nodes;
    }

    public void AllFrameOffset(Vector3 pos, Vector3 euler)
    {
        for (int i = 0; i < animList.Count; i++)
        {
            Vector3 newpos = animList[i][headIndex].GetVector3();
            animList[i][headIndex].SetVector3(newpos.x + pos.x, newpos.y + pos.y, newpos.z + pos.z);
            Vector3 newEuler = animList[i][headIndex].GetEuler();
            animList[i][headIndex].SetEuler(newEuler.x + euler.x, newEuler.y + euler.y, newEuler.z + euler.z);
        }
    }

    public void FrameRevert()
    {
        animList.Reverse();
    }
}

