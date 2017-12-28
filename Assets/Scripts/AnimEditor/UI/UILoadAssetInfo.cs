using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using LitJson;
using System.IO;

public class UILoadAssetInfo : UIBasePanel
{
    private Button startLoad;
    private Button backButton;
    private InputField assetBundleName;
    private InputField assetName;

    public UILoadAssetInfo() : base(new UIProperty(UIWindowStyle.Normal, UIWindowMode.NeedBack, UIColliderType.Normal, UIAnimationType.Normal, "UI/LoadAssetInfoUI"))
    {
    }

    protected override void OnAwakeInitUI()
    {
        startLoad = TransformExtension.FindComponent<Button>(CacheTrans, "StartLoad");
        startLoad.onClick.AddListener(LoadAsset);

        backButton = TransformExtension.FindComponent<Button>(CacheTrans, "BackButton");
        backButton.onClick.AddListener(Back);

        assetBundleName = TransformExtension.FindComponent<InputField>(CacheTrans, "AssetBundleName");
        assetName = TransformExtension.FindComponent<InputField>(CacheTrans, "AssetName");
    }

    void LoadAsset()
    {
        ApplicationMgr.Instance.StartCoroutine(LoadAssetAsync());
    }

    IEnumerator LoadAssetAsync()
    {
        //加载AssetBundle
        WWW www = new WWW("file://" + Application.dataPath + "/AssetBundles/" + assetBundleName.text);
        yield return www;
        NodesSaveInfoStruct ns = www.assetBundle.LoadAsset<NodesSaveInfoStruct>(assetName.text);
        AnimAssetCtrl.Instance.SetAnimList(JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo()));
        www.Dispose();
        AnimInit();
        AnimAssetCtrl.Instance.InitAnimInfo();
        UIWindowMgr.Instance.PopPanel();
        UIWindowMgr.Instance.PushPanel<UIAnimCommission>();
    }

    void AnimInit()
    {
        DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/Resources/FBX/");
        FileInfo[] info = d.GetFiles();

        GameObject temp = GameObject.Instantiate(Resources.Load("FBX/" + info[0].Name.EndsWith(".fbx"))) as GameObject;
        temp.transform.parent = AnimAssetCtrl.Instance.modelRoot;
        temp.transform.localPosition = Vector3.zero;
        AnimAssetCtrl.Instance.SetModel(temp);
    }

    void Back()
    {
        UIWindowMgr.Instance.PopPanel();
    }

    public override void OnRefresh()
    {
       
    }
}
