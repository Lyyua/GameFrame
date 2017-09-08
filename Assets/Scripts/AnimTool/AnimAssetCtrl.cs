using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class AnimAssetCtrl : MonoBehaviour
{
    private static AnimAssetCtrl _instance;
    public static AnimAssetCtrl Instance { get { return _instance; } }
    public Transform root;
    public Animation anim;
    public Animation curAnim;
    private List<Nodes[]> animList = new List<Nodes[]>();
    private Transform[] nodeList;

    private void Start()
    {
        UIMainManager.Instance.PopPanel<UIAnimEditor>(root);
        nodeList = anim.transform.Find("01").GetComponentsInChildren<Transform>();
        _instance = this;
        curAnim = anim;
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

    public void Clear()
    {
        animList.Clear();
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

    public void NodeAnimPlay(ref int index)
    {
        Nodes[] tempList = animList[index];
        //List<Nodes> tempList = all[replayIndex];
        for (int i = 0; i < tempList.Length; i++)
        {
            nodeList[i].localPosition = tempList[i].GetVector3();
            nodeList[i].localEulerAngles = tempList[i].GetEuler();
        }
        index++;
        if (index >= animList.Count)
        {
            index = 0;
        }
    }

    public string AnimListToString()
    {
        Debug.Log("导出数据: 一共" + animList.Count);
        return LitJson.JsonMapper.ToJson(animList);
    }
}
