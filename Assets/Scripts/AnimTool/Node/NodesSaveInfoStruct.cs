using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodesSaveInfoStruct : ScriptableObject
{
    [HideInInspector]
    public string nodesTran;
    List<List<Vector3>> nodesInfo;
    public void SetNodesInfo(string s)
    {
        nodesTran = s;
    }

    public string GetNodesInfo()
    {
        return nodesTran;
    }

    public void AddPosList(List<Vector3> list)
    {
        nodesInfo.Add(list);
    }

    public List<List<Vector3>> GetPosList()
    {
        return nodesInfo;
    }

}
