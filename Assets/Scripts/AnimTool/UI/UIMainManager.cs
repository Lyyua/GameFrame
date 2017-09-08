using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public delegate void OnUpdate();
public delegate void OnFixedUpdate();
public class UIMainManager : MonoBehaviour
{
    public static UIMainManager _instance;
    public static UIMainManager Instance { get { return _instance; } }

    private Dictionary<string, UIBasePanel> UIDIC = new Dictionary<string, UIBasePanel>();
    private Stack<UIBasePanel> windowNavgation = new Stack<UIBasePanel>();
    public UIBasePanel curPage;
    event OnUpdate updateUIEvent;
    event OnFixedUpdate fixedupdateUIEvent;

    private void Awake()
    {
        _instance = this;
    }

    public void RegisteUpdate(OnUpdate e)
    {
        updateUIEvent += e;
    }

    public void LogOutUpdate(OnUpdate e)
    {
        updateUIEvent -= e;
    }

    private void Update()
    {
        updateUIEvent();
    }

    public void RegisteFixedUpdate(OnFixedUpdate e)
    {
        fixedupdateUIEvent += e;
    }

    public void LogOutFixedUpdate(OnFixedUpdate e)
    {
        fixedupdateUIEvent -= e;
    }

    private void FixedUpdate()
    {
        fixedupdateUIEvent();
    }

    public void AddPanel<T>() where T : UIBasePanel, new()
    {
        Type t = typeof(T);
        string pageName = t.ToString();
        if (!UIDIC.ContainsKey(pageName))
        {
            T ui = new T();
            UIDIC.Add(pageName, ui);
            curPage = ui;
            windowNavgation.Push(curPage);
            updateUIEvent += curPage.OnUpdate;
            fixedupdateUIEvent += curPage.OnFixedUpdate;
        }
        else
        {
            curPage = UIDIC[pageName];
        }
    }

    public T PopPanel<T>(Transform _root) where T : UIBasePanel, new()
    {
        Type t = typeof(T);
        string pageName = t.ToString();
        if (!UIDIC.ContainsKey(pageName))
        {
            AddPanel<T>();
            curPage.OnEnter();
            TransformExtension.SetParent(curPage.trans, _root);
            TransformExtension.ResetLocalTransform(curPage.trans);
            return (T)curPage;
        }
        else
        {
            curPage = UIDIC[pageName];
            curPage.Show();
            windowNavgation.Push(curPage);
            return (T)curPage;
        }
    }

    public void ShutPanel<T>() where T : UIBasePanel, new()
    {
        Type t = typeof(T);
        string pageName = t.ToString();
        if (!UIDIC.ContainsKey(pageName))
        {
            Debug.LogError("不存在该窗口");
        }
        else
        {
            curPage.OnExit();
            windowNavgation.Pop();
            curPage = windowNavgation.Peek();
        }
    }

    public void HideCurPage()
    {
        curPage.OnExit();
    }

    public void BackPreWindow()
    {
        curPage.OnExit();
        windowNavgation.Pop();
        curPage = windowNavgation.Peek();
        curPage.Show();
    }
}
