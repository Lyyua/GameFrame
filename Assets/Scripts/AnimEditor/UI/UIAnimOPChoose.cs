using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using LitJson;
using System.Collections.Generic;

public class UIAnimOPChoose : UIBasePanel
{
    private Button loadAsset;
    private Button recordAsset;
    public UIAnimOPChoose() : base(new UIProperty(UIWindowStyle.Normal, UIWindowMode.NeedBack, UIColliderType.Normal, UIAnimationType.Normal, "UI/AnimEditorUI"))
    {
    }

    public override void OnRefresh()
    {
       
    }

    protected override void OnAwakeInitUI()
    {
        loadAsset = TransformExtension.FindComponent<Button>(CacheTrans, "LoadAssetButton");
        loadAsset.onClick.AddListener(ConfirmAssetInfo);

        recordAsset = TransformExtension.FindComponent<Button>(CacheTrans, "RecordAssetButton");
        recordAsset.onClick.AddListener(RecordAssetWindow);
    }

    void ConfirmAssetInfo()
    {
        UIWindowMgr.Instance.PopPanel();
        //UIMainManager.Instance.HideCurPage();
        //UILoadAssetInfo inputAsset = UIMainManager.Instance.PopPanel<UILoadAssetInfo>(AnimAssetCtrl.Instance.root);
        UIWindowMgr.Instance.PushPanel<UILoadAssetInfo>(AnimAssetCtrl.Instance.root);
    }

    void RecordAssetWindow()
    {
        UIWindowMgr.Instance.PopPanel();
        UIWindowMgr.Instance.PushPanel<UIAnimFBXChoose>(AnimAssetCtrl.Instance.root);
    }
}
