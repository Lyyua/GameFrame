using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using LitJson;

public class UILoadAssetInfo : UIBasePanel
{
    private Button startLoad;
    private Button backButton;
    private InputField assetBundleName;
    private InputField assetName;

    public UILoadAssetInfo() : base("UI/LoadAssetInfoUI")
    {
    }

    protected override void OnAwakeInitUI()
    {
        startLoad = TransformExtension.FindComponent<Button>(trans, "StartLoad");
        startLoad.onClick.AddListener(LoadAsset);

        backButton = TransformExtension.FindComponent<Button>(trans, "BackButton");
        backButton.onClick.AddListener(Back);

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
        AnimAssetCtrl.Instance.SetAnimList(JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo()));
        www.Dispose();
        UIMainManager.Instance.ShutPanel<UILoadAssetInfo>();
        UIMainManager.Instance.PopPanel<UIAnimCommission>(AnimAssetCtrl.Instance.root);
    }

    void Back()
    {
        UIMainManager.Instance.BackPreWindow();
    }
}
