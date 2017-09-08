using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UIRecordAnim : UIBasePanel
{
    public UIRecordAnim() : base("UI/RecordAnimUI")
    {
    }

    private Button recordButton;
    private Button commissionButton;
    private Button stopCommissionButton;
    private Button showAnimButton;
    private Button startExportButton;
    private Button backButton;

    private Dropdown showPlayStyle;
    private Dropdown commissionPlayStyle;  // once or loop
    private Dropdown recordStyle;   //fixed or update
    private int animIndex = 0;
    private Coroutine commission;
    private bool canCommission = false;

    protected override void OnAwakeInitUI()
    {
        recordButton = TransformExtension.FindComponent<Button>(trans, "RecordButton");
        recordButton.onClick.AddListener(StartRecord);
        commissionButton = TransformExtension.FindComponent<Button>(trans, "CommissionButton");
        commissionButton.onClick.AddListener(StartCommission);
        stopCommissionButton = TransformExtension.FindComponent<Button>(trans, "StopCommissionButton");
        stopCommissionButton.onClick.AddListener(StopCommission);
        showAnimButton = TransformExtension.FindComponent<Button>(trans, "ShowAnimButton");
        showAnimButton.onClick.AddListener(ShowAnim);
        startExportButton = TransformExtension.FindComponent<Button>(trans, "StartExportButton");
        startExportButton.onClick.AddListener(StartExport);
        backButton = TransformExtension.FindComponent<Button>(trans, "BackButton");
        backButton.onClick.AddListener(Back);

        showPlayStyle = TransformExtension.FindComponent<Dropdown>(trans, "ShowPlayStyle");
        commissionPlayStyle = TransformExtension.FindComponent<Dropdown>(trans, "CommissionPlayStyle");
        recordStyle = TransformExtension.FindComponent<Dropdown>(trans, "RecordStyle");
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {
        commissionButton.enabled = canCommission;
    }

    private void StartRecord()
    {
        ConfirmAnimOpShut();
        AnimAssetCtrl.Instance.PlayAnim(0);
        AnimAssetCtrl.Instance.StartCoroutine(RecordData());
    }

    private void StartCommission()
    {
        ConfirmAnimOpShut();

        commission = AnimAssetCtrl.Instance.StartCoroutine(Commission());
    }

    private void StopCommission()
    {
        ConfirmAnimOpShut();
        animIndex = 0;
        AnimAssetCtrl.Instance.NodeAnimPlay(ref animIndex);
        animIndex = 0;
    }

    private void ConfirmAnimOpShut()
    {
        if (commission != null)
        {
            AnimAssetCtrl.Instance.StopCoroutine(commission);
        }
        AnimAssetCtrl.Instance.AnimStop();
    }

    private void Back()
    {
        canCommission = false;
        UIMainManager.Instance.BackPreWindow();
    }

    private void StartExport()
    {
        UIMainManager.Instance.ShutPanel<UIRecordAnim>();
        UIMainManager.Instance.PopPanel<UIExportAssetInfo>(AnimAssetCtrl.Instance.root);
    }

    protected override void OnActiveBefore()
    {
        UIMainManager.Instance.RegisteFixedUpdate(OnFixedUpdate);
    }

    protected override void OnExitBefore()
    {
        UIMainManager.Instance.LogOutFixedUpdate(OnFixedUpdate);
    }

    private void ShowAnim()
    {
        ConfirmAnimOpShut();
        AnimAssetCtrl.Instance.PlayAnim(showPlayStyle.value);
    }

    IEnumerator Commission()
    {
        animIndex = 0;
        if (commissionPlayStyle.value == 0)
        {
            yield return new WaitForFixedUpdate();
            yield return new WaitForEndOfFrame();
            AnimAssetCtrl.Instance.NodeAnimPlay(ref animIndex);

            while (true)
            {
                if (animIndex == 0)
                {
                    yield break;
                }
                yield return new WaitForFixedUpdate();
                yield return new WaitForEndOfFrame();
                AnimAssetCtrl.Instance.NodeAnimPlay(ref animIndex);
            }
        }
        else
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                yield return new WaitForEndOfFrame();
                AnimAssetCtrl.Instance.NodeAnimPlay(ref animIndex);

                while (true)
                {
                    if (animIndex == 0)
                    {
                        break;
                    }
                    yield return new WaitForFixedUpdate();
                    yield return new WaitForEndOfFrame();
                    AnimAssetCtrl.Instance.NodeAnimPlay(ref animIndex);
                }
            }
        }
    }
    IEnumerator RecordData()
    {
        AnimAssetCtrl.Instance.Clear();
        if (recordStyle.value == 0)
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                yield return new WaitForEndOfFrame();
                if (AnimAssetCtrl.Instance.AnimComplete())
                {
                    canCommission = true;
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
                    canCommission = true;
                    yield break;
                }
                AnimAssetCtrl.Instance.WriteInfo();
            }
        }
    }
}
