using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UIRecordAnim : UIBasePanel
{
    public UIRecordAnim() : base("UI/RecordAnimUI")
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
        showAnimButton = TransformExtension.FindComponent<Button>(trans, "ShowAnimButton");
        showAnimButton.onClick.AddListener(ShowAnim);
        recordButton = TransformExtension.FindComponent<Button>(trans, "RecordButton");
        recordButton.onClick.AddListener(StartRecord);
        startCommissionButton = TransformExtension.FindComponent<Button>(trans, "StartCommissionButton");
        startCommissionButton.onClick.AddListener(StartCommission);
        startExportButton = TransformExtension.FindComponent<Button>(trans, "StartExportButton");
        startExportButton.onClick.AddListener(StartExport);
        backButton = TransformExtension.FindComponent<Button>(trans, "BackButton");
        backButton.onClick.AddListener(Back);

        showPlayStyle = TransformExtension.FindComponent<Dropdown>(trans, "ShowPlayStyle");

        recordStyle = TransformExtension.FindComponent<Dropdown>(trans, "RecordStyle");

    }

    public override void OnFixedUpdate()
    {

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
        UIMainManager.Instance.HideCurPage();
        UIMainManager.Instance.PopPanel<UIAnimCommission>(AnimAssetCtrl.Instance.root);
    }

    private void Back()
    {
        AnimAssetCtrl.Instance.HeadParentNULL();
        UIMainManager.Instance.BackPreWindow();
    }

    private void StartExport()
    {
        UIMainManager.Instance.ShutPanel<UIRecordAnim>();
        UIMainManager.Instance.PopPanel<UIExportAssetInfo>(AnimAssetCtrl.Instance.root);
    }

    protected override void OnActiveBefore()
    {
        startCommissionButton.interactable = false;
        UIMainManager.Instance.RegisteFixedUpdate(OnFixedUpdate);
    }

    protected override void OnExitBefore()
    {
        UIMainManager.Instance.LogOutFixedUpdate(OnFixedUpdate);
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
}
