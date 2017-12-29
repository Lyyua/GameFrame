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
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().SetAnimList(JsonMapper.ToObject<List<Nodes[]>>(ns.GetNodesInfo()));
        www.Dispose();
        CreateAnimGameObject();
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().InitAnimInfo();
        UIWindowMgr.Instance.PopPanel();
        UIWindowMgr.Instance.PushPanel<UIAnimCommission>();
    }

    void CreateAnimGameObject()
    {
        DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/Resources/FBX/");
        FileInfo[] info = d.GetFiles();

        GameObject temp = GameObject.Instantiate(Resources.Load("FBX/" + info[0].Name.EndsWith(".fbx"))) as GameObject;
        temp.transform.parent = UIModelMgr.Instance.GetModel<UIAnimMadeModel>().ModelRoot;
        temp.transform.localPosition = Vector3.zero;
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().CurAnim = temp.GetComponent<Animation>();
    }

    void Back()
    {
        UIWindowMgr.Instance.PopPanel();
    }

    public override void OnRefresh()
    {
       
    }
}
