using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;


public class UIRecordAnim : UIBasePanel
{
    public UIRecordAnim() : base(new UIProperty(UIWindowStyle.Normal, UIWindowMode.NeedBack, UIColliderType.Normal, UIAnimationType.Normal, "UI/RecordAnimUI"))
    {
    }

    private Button showAnimButton;
    private Button recordButton;
    private Button startExportButton;
    private Button backButton;
    private Button startCommissionButton;
    private Dropdown showPlayStyle;
    private Dropdown recordStyle;   //fixed or update


    protected override void OnAwakeInitUI()
    {
        showAnimButton = TransformExtension.FindComponent<Button>(CacheTrans, "ShowAnimButton");
        showAnimButton.onClick.AddListener(ShowAnim);
        recordButton = TransformExtension.FindComponent<Button>(CacheTrans, "RecordButton");
        recordButton.onClick.AddListener(StartRecord);
        startCommissionButton = TransformExtension.FindComponent<Button>(CacheTrans, "StartCommissionButton");
        startCommissionButton.onClick.AddListener(StartCommission);
        startExportButton = TransformExtension.FindComponent<Button>(CacheTrans, "StartExportButton");
        startExportButton.onClick.AddListener(StartExport);
        backButton = TransformExtension.FindComponent<Button>(CacheTrans, "BackButton");
        backButton.onClick.AddListener(Back);

        showPlayStyle = TransformExtension.FindComponent<Dropdown>(CacheTrans, "ShowPlayStyle");

        recordStyle = TransformExtension.FindComponent<Dropdown>(CacheTrans, "RecordStyle");

    }

    void ButtonStateInit()
    {
        startCommissionButton.interactable = false;
    }

    private void StartRecord()
    {
        AnimAssetCtrl.Instance.PlayAnim(0);
        AnimAssetCtrl.Instance.StartCoroutine(RecordData());
    }

    private void StartCommission()
    {
        UIWindowMgr.Instance.PopPanel(this);
        UIWindowMgr.Instance.PushPanel<UIAnimCommission>();
    }

    private void Back()
    {
        AnimAssetCtrl.Instance.HeadParentNULL();
        UIWindowMgr.Instance.PopPanel();
        UIWindowMgr.Instance.PushPanel<UIAnimFBXChoose>();
    }

    private void StartExport()
    {
        UIWindowMgr.Instance.PopPanel(this);
        UIWindowMgr.Instance.PushPanel<UIExportAssetInfo>();
    }
    protected override void OnExitBefore()
    {
    }

    private void ShowAnim()
    {
        AnimAssetCtrl.Instance.PlayAnim(showPlayStyle.value);
    }
    
    IEnumerator RecordData()
    {
        AnimAssetCtrl.Instance.AnimListClear();
        if (recordStyle.value == 0)
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                yield return new WaitForEndOfFrame();
                if (AnimAssetCtrl.Instance.AnimComplete())
                {
                    startCommissionButton.interactable = true;
                    yield break;
                }
                AnimAssetCtrl.Instance.WriteInfo();
            }
        }
        else
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (AnimAssetCtrl.Instance.AnimComplete())
                {
                    startCommissionButton.interactable = true;
                    yield break;
                }
                AnimAssetCtrl.Instance.WriteInfo();
            }
        }
    }

    public override void OnRefresh()
    {
        startCommissionButton.interactable = false;
    }
}


