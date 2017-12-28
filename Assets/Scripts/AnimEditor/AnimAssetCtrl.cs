using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;

public class AnimAssetCtrl : MonoBehaviour
{
    private static AnimAssetCtrl _instance;
    public static AnimAssetCtrl Instance { get { return _instance; } }
    public Transform root;
    public Transform modelRoot;
    public GameObject headObj;
    private Animation curAnim;
    private List<Nodes[]> animList = new List<Nodes[]>();
    private List<Nodes[]> backAnimList = new List<Nodes[]>();
    private Transform[] nodeList;
    private Transform head;
    private int headIndex;

    private void Start()
    {
        UIWindowMgr.Instance.PushPanel<UIAnimOPChoose>();
        _instance = this;
    }

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

    public void HeadParentNULL()
    {
        headObj.transform.parent = null;
    }

    public void SetModel(GameObject go)
    {
        if (curAnim != null)
        {
            Destroy(curAnim.gameObject);
        }
        curAnim = go.GetComponent<Animation>();
        curAnim.wrapMode = WrapMode.Loop;
    }

    public void DestroyModel()
    {
        if (curAnim != null)
        {
            Destroy(curAnim.gameObject);
        }
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
        curAnim.Stop();
    }

    public bool AnimComplete()
    {
        return !curAnim.isPlaying;
    }

    public void AnimListClear()
    {
        animList.Clear();
        backAnimList = null;
    }

    public void SetAnimList(List<Nodes[]> list)
    {
        animList = list;
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

    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
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
