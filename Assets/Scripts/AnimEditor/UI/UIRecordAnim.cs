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

    protected override void OnActiveBefore()
    {
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().InitAnimInfo();
        if (UIModelMgr.Instance.GetModel<UIAnimMadeModel>().CurAnim)
        {
            UIModelMgr.Instance.GetModel<UIAnimMadeModel>().CurAnim.Stop();
        }
        ButtonStateInit();
    }

    void ButtonStateInit()
    {
        startCommissionButton.interactable = false;
    }

    private void StartRecord()
    {
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().PlayAnim(0);
        CoroutineMgr.Instance.StartCoroutine(RecordData());
    }

    private void StartCommission()
    {
        UIWindowMgr.Instance.PopPanel(this);
        UIWindowMgr.Instance.PushPanel<UIAnimCommission>();
    }

    private void Back()
    {
        //AnimAssetCtrl.Instance.HeadParentNULL();
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
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().PlayAnim(showPlayStyle.value);
    }
    
    IEnumerator RecordData()
    {
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().AnimListClear();
        if (recordStyle.value == 0)
        {
            //fixed
            while (true)
            {
                yield return new WaitForFixedUpdate();
                yield return new WaitForEndOfFrame();
                if (!UIModelMgr.Instance.GetModel<UIAnimMadeModel>().CurAnim.isPlaying)
                {
                    startCommissionButton.interactable = true;
                    yield break;
                }
                UIModelMgr.Instance.GetModel<UIAnimMadeModel>().WriteInfo();
            }
        }
        else
        {
            //each update
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (!UIModelMgr.Instance.GetModel<UIAnimMadeModel>().CurAnim.isPlaying)
                {
                    startCommissionButton.interactable = true;
                    yield break;
                }
                UIModelMgr.Instance.GetModel<UIAnimMadeModel>().WriteInfo();
            }
        }
    }

    public override void OnRefresh()
    {
        startCommissionButton.interactable = false;
    }
}


