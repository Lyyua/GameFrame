using System.Collections;
using UnityEngine;

public class UILayerMgr : UnitySingleton<UILayerMgr>
{
    #region 字段和属性

    protected GameObject mGo;

    public Transform UIWindowRoot { get; private set; }

    public Transform FixedWindowRoot { get; private set; }

    public Transform NormalWindowRoot { get; private set; }

    public Transform PopupWindowRoot { get; private set; }

    public Transform TopBarWindowRoot { get; private set; }

    public Transform GameUIRoot { get; private set; }

    public Camera UICamera { get; private set; }

    #endregion 字段和属性

    public UILayerConst.UIDisplayMode UIDisplayMode { get; private set; }

    public void SetDepthAndRoot(UIBasePanel mPage)
    {
        switch (mPage.UIPageProperty.WindowStyle)
        {
            case UIWindowStyle.GameUI:
                mPage.CacheTrans.SetParent(GameUIRoot);
                break;
            case UIWindowStyle.Fixed:
                mPage.CacheTrans.SetParent(FixedWindowRoot);
                break;
            case UIWindowStyle.Normal:
                mPage.CacheTrans.SetParent(NormalWindowRoot);
                break;
            case UIWindowStyle.PopUp:
                mPage.CacheTrans.SetParent(PopupWindowRoot);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 设置隐藏与显示
    /// </summary>
    /// <param name="isVisible"></param>
    public void SetVisible(GameObject go, bool isVisible)
    {
        switch (UIDisplayMode)
        {
            case UILayerConst.UIDisplayMode.UILayer:
                if (go != null && !go.activeSelf)
                {
                    go.SetActive(true);
                }
                //go.layer = LayerMask.NameToLayer(isVisible ? UILayerConst.ShowUILayer : UILayerConst.HideUILayer);
                break;
            case UILayerConst.UIDisplayMode.UIActive:
                if (go != null)
                {
                    go.SetActive(isVisible);
                }
                break;
            default:
                break;
        }
    }

    protected override void OnInit()
    {
        InitRoot();
        base.OnInit();
    }

    protected override void OnUnInit()
    {
        base.OnUnInit();
    }

    private void InitRoot()
    {
        FixedWindowRoot = CreateGameObject(UIWindowRoot, UILayerConst.FixedRoot).transform;

        NormalWindowRoot = CreateGameObject(UIWindowRoot, UILayerConst.NormalRoot).transform;

        PopupWindowRoot = CreateGameObject(UIWindowRoot, UILayerConst.PopupRoot).transform;

        GameUIRoot = CreateGameObject(UIWindowRoot, UILayerConst.GameUIRoot).transform;
    }

    private GameObject CreateGameObject(Transform root, string name)
    {
        GameObject go = new GameObject(name);
        go.transform.parent = root;
        go.layer = LayerMask.NameToLayer(UILayerConst.ShowUILayer);
        go.transform.localPosition = Vector3.zero;
        return go;
    }

}

public class UILayerConst
{
    /// <summary>
    /// UI显示隐藏的模式
    /// </summary>
    public enum UIDisplayMode
    {
        UILayer,
        UIActive,
    }

    public const int FixedDepth = 100;
    public const int NormalDepth = 50;
    public const int PopUpDepth = 150;

    public const string ShowUILayer = "UI";
    public const string HideUILayer = "HideUI";

    public const string UIRoot = "UIWindowRoot";
    public const string FixedRoot = "FixedWindowRoot";
    public const string NormalRoot = "NormalWindowRoot";
    public const string PopupRoot = "PopupWindowRoot";
    public const string GameUIRoot = "GameUIRoot";
    public const string Camera = "UICamera";
}
