using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UIAnimCommission : UIBasePanel
{
    public UIAnimCommission() : base(new UIProperty(UIWindowStyle.Normal,UIWindowMode.NeedBack,UIColliderType.Normal,UIAnimationType.Normal, "UI/CommissionUI"))
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
    private Button sureCutButton;
    private Button frameCutButton;
    private Button curFrameCutButton;
    private Button frameRevertButton;

    private Transform frameInfoPivot;
    private InputField pxo;
    private InputField pyo;
    private InputField pzo;

    private InputField exo;
    private InputField eyo;
    private InputField ezo;

    private Transform frameCutInfoPivot;
    private InputField frameStart;
    private InputField frameEnd;

    private Text curFrameCount;

    private Dropdown commissionPlayStyle;  // once or loop

    private Coroutine commission;

    private int curAnimIndex = 0;
    private int nextAnimIndex = 0;

    protected override void OnAwakeInitUI()
    {
        commissionButton = TransformExtension.FindComponent<Button>(CacheTrans, "CommissionButton");
        commissionButton.onClick.AddListener(StartCommission);
        frameCommissionButton = TransformExtension.FindComponent<Button>(CacheTrans, "FrameCommissionButton");
        frameCommissionButton.onClick.AddListener(StartFrameCommission);
        nextFrameButton = TransformExtension.FindComponent<Button>(CacheTrans, "NextFrameButton");
        nextFrameButton.onClick.AddListener(NextFrame);
        stopCommissionButton = TransformExtension.FindComponent<Button>(CacheTrans, "StopCommissionButton");
        stopCommissionButton.onClick.AddListener(StopCommission);
        updateFrameInfoButton = TransformExtension.FindComponent<Button>(CacheTrans, "UpdateFrameInfoButton");
        updateFrameInfoButton.onClick.AddListener(UpdateFrameInfoClick);
        frameRevertButton = TransformExtension.FindComponent<Button>(CacheTrans, "FrameRevertButton");
        frameRevertButton.onClick.AddListener(FrameRevertClick);

        Transform curFrameCountPivot = TransformExtension.FindComponent<Transform>(CacheTrans, "CurFrameCount");
        curFrameCount = TransformExtension.FindComponent<Text>(curFrameCountPivot, "Text");

        backButton = TransformExtension.FindComponent<Button>(CacheTrans, "BackButton");
        backButton.onClick.AddListener(Back);

        frameInfoPivot = TransformExtension.FindComponent<Transform>(CacheTrans, "FrameInfoPivot");

        pxo = TransformExtension.FindComponent<InputField>(frameInfoPivot, "PXOffset");
        pyo = TransformExtension.FindComponent<InputField>(frameInfoPivot, "PYOffset");
        pzo = TransformExtension.FindComponent<InputField>(frameInfoPivot, "PZOffset");

        exo = TransformExtension.FindComponent<InputField>(frameInfoPivot, "EXOffset");
        eyo = TransformExtension.FindComponent<InputField>(frameInfoPivot, "EYOffset");
        ezo = TransformExtension.FindComponent<InputField>(frameInfoPivot, "EZOffset");

        sureButton = TransformExtension.FindComponent<Button>(frameInfoPivot, "SureButton");
        sureButton.onClick.AddListener(SureFrameInfoClick);

        frameCutButton = TransformExtension.FindComponent<Button>(CacheTrans, "FrameCutButton");
        frameCutButton.onClick.AddListener(ShowCutFrame);

        curFrameCutButton = TransformExtension.FindComponent<Button>(CacheTrans, "CurFrameCutButton");
        curFrameCutButton.onClick.AddListener(CurFrameCut);

        frameCutInfoPivot = TransformExtension.FindComponent<Transform>(CacheTrans, "FrameCutInfoPivot");

        frameStart = TransformExtension.FindComponent<InputField>(frameCutInfoPivot, "FrameStart");
        frameEnd = TransformExtension.FindComponent<InputField>(frameCutInfoPivot, "FrameEnd");

        sureCutButton = TransformExtension.FindComponent<Button>(frameCutInfoPivot, "SureCutButton");
        sureCutButton.onClick.AddListener(SureCutFrame);

        allAplyButton = TransformExtension.FindComponent<Button>(frameInfoPivot, "AllAplyButton");
        allAplyButton.onClick.AddListener(AllFrameAply);

        commissionPlayStyle = TransformExtension.FindComponent<Dropdown>(CacheTrans, "CommissionPlayStyle");

        StartFrameCommission();
    }

    public void OnUpdate()
    {
        if (!frameInfoPivot.gameObject.activeSelf) return;
        try
        {
            UIModelMgr.Instance.GetModel<UIAnimMadeModel>().SetHeadLocalPos(new Vector3(float.Parse(pxo.text),
                float.Parse(pyo.text),
                float.Parse(pzo.text)));

            UIModelMgr.Instance.GetModel<UIAnimMadeModel>().SetHeadLocalEuler(new Vector3(float.Parse(exo.text),
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

        commission = CoroutineMgr.Instance.StartCoroutineReturn(Commission());
    }

    private void StartFrameCommission()
    {
        ConfirmAnimOpShut();
        curAnimIndex = 0;
        curFrameCount.text = curAnimIndex.ToString();
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().NodeAnimPlay(ref curAnimIndex);
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().InitCurFrameInfo();
        curAnimIndex++;
    }

    private void NextFrame()
    {
        ConfirmAnimOpShut();
        bool complete = UIModelMgr.Instance.GetModel<UIAnimMadeModel>().NodeAnimPlay(ref curAnimIndex);
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().InitCurFrameInfo();
        curFrameCount.text = curAnimIndex.ToString();
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

    void FrameRevertClick()
    {
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().FrameRevert();
    }

    void SureFrameInfoClick()
    {
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().SetHeadNodeInfo(curAnimIndex);
        FrameInputReset();
    }

    void AllFrameAply()
    {
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().AllFrameOffset(new Vector3(float.Parse(pxo.text),
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

    void ShowCutFrame()
    {
        frameCutInfoPivot.gameObject.SetActive(true);
    }

    void SureCutFrame()
    {
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().AnimListCut(int.Parse(frameStart.text), int.Parse(frameEnd.text));
        frameCutInfoPivot.gameObject.SetActive(false);
    }

    void CurFrameCut()
    {
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().CurFramecCut(curAnimIndex);
    }

    void Back()
    {
        UIWindowMgr.Instance.PopPanel();
    }

    private void StopCommission()
    {
        ConfirmAnimOpShut();
        curAnimIndex = 0;
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().NodeAnimPlay(ref curAnimIndex);
        curAnimIndex = 0;
    }

    private void ConfirmAnimOpShut()
    {
        if (commission != null)
        {
           CoroutineMgr.Instance.StopCoroutine(commission);
        }
        UIModelMgr.Instance.GetModel<UIAnimMadeModel>().AnimStop();
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
                UIModelMgr.Instance.GetModel<UIAnimMadeModel>().NodeAnimPlay(ref curAnimIndex);
                curFrameCount.text = curAnimIndex.ToString();
                curAnimIndex++;

                while (true)
                {
                    yield return new WaitForFixedUpdate();
                    yield return new WaitForEndOfFrame();
                    bool complete = UIModelMgr.Instance.GetModel<UIAnimMadeModel>().NodeAnimPlay(ref curAnimIndex);
                    curFrameCount.text = curAnimIndex.ToString();
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
                UIModelMgr.Instance.GetModel<UIAnimMadeModel>().NodeAnimPlay(ref curAnimIndex);
                curFrameCount.text = curAnimIndex.ToString();
                while (true)
                {
                    yield return new WaitForFixedUpdate();
                    yield return new WaitForEndOfFrame();
                    bool complete = UIModelMgr.Instance.GetModel<UIAnimMadeModel>().NodeAnimPlay(ref curAnimIndex);
                    curFrameCount.text = curAnimIndex.ToString();
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

    protected override void RegisterEvent()
    {
        ApplicationMgr.Instance.onUpdate += OnUpdate;
    }

    protected override void OnExitBefore()
    {
        ConfirmAnimOpShut();
    }

    protected override void RemoveEvent()
    {
        ApplicationMgr.Instance.onUpdate -= OnUpdate;
    }

    public override void OnRefresh()
    {
       
    }
}