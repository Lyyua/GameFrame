using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using LitJson;

public class UILoadAssetInfo : UIBasePanel
{
    private Button startLoad;
    private InputField assetBundleName;
    private InputField assetName;

    public UILoadAssetInfo() : base("UI/LoadAssetInfoUI")
    {
    }

    protected override void OnAwakeInitUI()
    {
        startLoad = TransformExtension.FindComponent<Button>(trans, "StartLoad");
        startLoad.onClick.AddListener(LoadAsset);

        assetBundleName = TransformExtension.FindComponent<InputField>(trans, "AssetBundleName");
        assetName = TransformExtension.FindComponent<InputField>(trans, "AssetName");
    }

    void LoadAsset()
    {
        UIMainManager.Instance.StartCoroutine(LoadAssetAsync());
    }

    IEnumerator LoadAssetAsync()
    {
        //加载AssetBundle
        WWW www = new WWW("file://" + Application.dataPath + "/AssetBundles/" + assetBundleName.text);
        yield return www;
        NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>(assetName.text);
        List<Nodes[]> animNodesList = JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo());
        Debug.Log(string.Format("一共{0}条数据", animNodesList.Count));
        www.Dispose();
        UIMainManager.Instance.ShutPanel<UILoadAssetInfo>();
    }
}
