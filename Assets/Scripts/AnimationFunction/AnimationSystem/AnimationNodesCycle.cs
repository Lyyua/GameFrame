using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationNodesCycle : MonoBehaviour
{
    public Transform body;
    public Transform hands;
    private Transform[] nodesArray;
    private Transform[] handsNodesArray;
    private Transform[] tempNodesArray;
    private Transform[] tempHandsNodesArray;
    private void Awake()
    {
        tempNodesArray = body.transform.FindChild("01").GetComponentsInChildren<Transform>();
        tempHandsNodesArray = hands.transform.FindChild("01").GetComponentsInChildren<Transform>();
        int tempIndex = 0;
        nodesArray = new Transform[71];
        handsNodesArray = new Transform[71];

        for (int i = 0; i < tempNodesArray.Length; i++)
        {
            if (tempNodesArray[i].name == "SpawnPoint")
            {
                continue;
            }
            else
            {
                nodesArray[tempIndex] = tempNodesArray[i];
                tempIndex++;
            }
        }
        tempIndex = 0;

        for (int i = 0; i < tempHandsNodesArray.Length; i++)
        {
            if (tempHandsNodesArray[i].name == "SpawnPoint" || tempHandsNodesArray[i].name == "Pivot")
            {
                continue;
            }
            else
            {
                handsNodesArray[tempIndex] = tempHandsNodesArray[i];
                tempIndex++;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="infoall">动作数据</param>
    public bool AnimPlay(List<Nodes[]> infoall, ref int irow)
    {
        Nodes[] temp = infoall[irow];
        for (int i = 0; i < nodesArray.Length; i++)
        {
            nodesArray[i].localPosition = temp[i].GetVector3();
            handsNodesArray[i].localPosition = temp[i].GetVector3();
            nodesArray[i].localEulerAngles = temp[i].GetEuler();
            handsNodesArray[i].localEulerAngles = temp[i].GetEuler();
        }
        if (irow == infoall.Count - 1)
        {
            return true;
        }
        return false;
    }
    //只循环下半身
    public bool AnimPlayLowerBody(List<Nodes[]> infoall, ref int irow)
    {
        Nodes[] temp = infoall[irow];
        for (int i = 0; i < 15; i++)
        {
            nodesArray[i].localPosition = temp[i].GetVector3();
            handsNodesArray[i].localPosition = temp[i].GetVector3();
            nodesArray[i].localEulerAngles = temp[i].GetEuler();
        }
        if (irow == infoall.Count - 1)
        {
            return true;
        }
        return false;
    }
}
