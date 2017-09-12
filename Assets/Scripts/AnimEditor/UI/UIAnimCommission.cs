using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UIAnimCommission : UIBasePanel
{
    public UIAnimCommission() : base("UI/CommissionUI")
    {
    }

    private Button commissionButton;
    private Button stopCommissionButton;
    private Button frameCommissionButton;
    private Button nextFrameButton;
    private Button updateFrameInfoButton;
    private Button sureButton;
    private Button backButton;
    private Button allAplyButton;

    private Transform frameInfoPivot;
    private InputField pxo;
    private InputField pyo;
    private InputField pzo;

    private InputField exo;
    private InputField eyo;
    private InputField ezo;

    private Dropdown commissionPlayStyle;  // once or loop

    private Coroutine commission;

    private int curAnimIndex = 0;
    private int nextAnimIndex = 0;

    protected override void OnAwakeInitUI()
    {
        commissionButton = TransformExtension.FindComponent<Button>(trans, "CommissionButton");
        commissionButton.onClick.AddListener(StartCommission);
        frameCommissionButton = TransformExtension.FindComponent<Button>(trans, "FrameCommissionButton");
        frameCommissionButton.onClick.AddListener(StartFrameCommission);
        nextFrameButton = TransformExtension.FindComponent<Button>(trans, "NextFrameButton");
        nextFrameButton.onClick.AddListener(NextFrame);
        stopCommissionButton = TransformExtension.FindComponent<Button>(trans, "StopCommissionButton");
        stopCommissionButton.onClick.AddListener(StopCommission);
        updateFrameInfoButton = TransformExtension.FindComponent<Button>(trans, "UpdateFrameInfoButton");
        updateFrameInfoButton.onClick.AddListener(UpdateFrameInfoClick);

        backButton = TransformExtension.FindComponent<Button>(trans, "BackButton");
        backButton.onClick.AddListener(Back);

        frameInfoPivot = TransformExtension.FindComponent<Transform>(trans, "FrameInfoPivot");

        pxo = TransformExtension.FindComponent<InputField>(frameInfoPivot, "PXOffset");
        pyo = TransformExtension.FindComponent<InputField>(frameInfoPivot, "PYOffset");
        pzo = TransformExtension.FindComponent<InputField>(frameInfoPivot, "PZOffset");

        exo = TransformExtension.FindComponent<InputField>(frameInfoPivot, "EXOffset");
        eyo = TransformExtension.FindComponent<InputField>(frameInfoPivot, "EYOffset");
        ezo = TransformExtension.FindComponent<InputField>(frameInfoPivot, "EZOffset");

        sureButton = TransformExtension.FindComponent<Button>(frameInfoPivot, "SureButton");
        sureButton.onClick.AddListener(SureFrameInfoClick);

        allAplyButton = TransformExtension.FindComponent<Button>(frameInfoPivot, "AllAplyButton");
        allAplyButton.onClick.AddListener(AllFrameAply);

        commissionPlayStyle = TransformExtension.FindComponent<Dropdown>(trans, "CommissionPlayStyle");
    }

    public override void OnUpdate()
    {
        if (!frameInfoPivot.gameObject.activeSelf) return;
        try
        {
            AnimAssetCtrl.Instance.SetHeadLocalPos(new Vector3(float.Parse(pxo.text),
                float.Parse(pyo.text),
                float.Parse(pzo.text)));

            AnimAssetCtrl.Instance.SetHeadLocalEuler(new Vector3(float.Parse(exo.text),
                float.Parse(eyo.text),
                float.Parse(ezo.text)));
        }
        catch
        {

        }
    }

    private void StartCommission()
    {
        ConfirmAnimOpShut();

        commission = AnimAssetCtrl.Instance.StartCoroutine(Commission());
    }

    private void StartFrameCommission()
    {
        ConfirmAnimOpShut();
        curAnimIndex = 0;
        AnimAssetCtrl.Instance.NodeAnimPlay(ref curAnimIndex);
        AnimAssetCtrl.Instance.InitCurFrameInfo();
        nextAnimIndex = 1;
    }

    private void NextFrame()
    {
        bool complete = AnimAssetCtrl.Instance.NodeAnimPlay(ref nextAnimIndex);
        AnimAssetCtrl.Instance.InitCurFrameInfo();
        if (!complete)
        {
            curAnimIndex++;
            nextAnimIndex++;
        }
    }

    void UpdateFrameInfoClick()
    {
        frameInfoPivot.gameObject.SetActive(!frameInfoPivot.gameObject.activeSelf);
    }

    void SureFrameInfoClick()
    {
        AnimAssetCtrl.Instance.SetHeadNodeInfo(curAnimIndex);
        FrameInputReset();
    }

    void AllFrameAply()
    {
        AnimAssetCtrl.Instance.AllFrameOffset(new Vector3(float.Parse(pxo.text),
                float.Parse(pyo.text),
                float.Parse(pzo.text)),
                new Vector3(float.Parse(exo.text),
                float.Parse(eyo.text),
                float.Parse(ezo.text)));
        FrameInputReset();
        frameInfoPivot.gameObject.SetActive(false);
    }

    void FrameInputReset()
    {
        pxo.text = "0";
        pyo.text = "0";
        pzo.text = "0";
        exo.text = "0";
        eyo.text = "0";
        ezo.text = "0";
    }

    void Back()
    {
        UIMainManager.Instance.BackPreWindow();
    }

    private void StopCommission()
    {
        ConfirmAnimOpShut();
        curAnimIndex = 0;
        AnimAssetCtrl.Instance.NodeAnimPlay(ref curAnimIndex);
        curAnimIndex = 0;
    }

    private void ConfirmAnimOpShut()
    {
        if (commission != null)
        {
            AnimAssetCtrl.Instance.StopCoroutine(commission);
        }
        AnimAssetCtrl.Instance.AnimStop();
    }

    IEnumerator Commission()
    {
        curAnimIndex = 0;
        while (true)
        {
            if (commissionPlayStyle.value == 0)
            {
                yield return new WaitForFixedUpdate();
                yield return new WaitForEndOfFrame();
                AnimAssetCtrl.Instance.NodeAnimPlay(ref curAnimIndex);
                curAnimIndex++;

                while (true)
                {
                    yield return new WaitForFixedUpdate();
                    yield return new WaitForEndOfFrame();
                    bool complete = AnimAssetCtrl.Instance.NodeAnimPlay(ref curAnimIndex);
                    if (complete)
                    {
                        break;
                    }
                    curAnimIndex++;
                }
                break;
            }
            else
            {

                yield return new WaitForFixedUpdate();
                yield return new WaitForEndOfFrame();
                AnimAssetCtrl.Instance.NodeAnimPlay(ref curAnimIndex);

                while (true)
                {
                    yield return new WaitForFixedUpdate();
                    yield return new WaitForEndOfFrame();
                    bool complete = AnimAssetCtrl.Instance.NodeAnimPlay(ref curAnimIndex);
                    if (complete)
                    {
                        curAnimIndex = 0;
                        break;
                    }
                    curAnimIndex++;
                }
            }
        }

    }

    protected override void OnActiveBefore()
    {
        UIMainManager.Instance.RegisteUpdate(OnUpdate);
    }

    protected override void OnExitBefore()
    {
        ConfirmAnimOpShut();
        UIMainManager.Instance.LogOutUpdate(OnUpdate);
    }
}