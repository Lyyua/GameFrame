using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using LitJson;
using UnityEditor;

public class UIExportAssetInfo : UIBasePanel
{
    public UIExportAssetInfo() : base(new UIProperty(UIWindowStyle.Normal, UIWindowMode.NeedBack, UIColliderType.Normal, UIAnimationType.Normal, "UI/ExportAssetInfoUI"))
    {
    }

    private Button export;
    private InputField assetNameInput;

    protected override void OnAwakeInitUI()
    {
        export = TransformExtension.FindComponent<Button>(CacheTrans, "ExportButton");
        export.onClick.AddListener(ExportAsset);

        assetNameInput = TransformExtension.FindComponent<InputField>(CacheTrans, "AssetNameInput");
    }

    private void ExportAsset()
    {
        NodesSaveInfoStruct ns = ScriptableObject.CreateInstance<NodesSaveInfoStruct>();
        string json = AnimAssetCtrl.Instance.AnimListToString();
        ns.SetNodesInfo(json);
        AssetDatabase.CreateAsset(ns, string.Format("Assets/{0}.asset", assetNameInput.text));
        UIWindowMgr.Instance.PopPanel();
    }

    public override void OnRefresh()
    {
        throw new NotImplementedException();
    }
}
