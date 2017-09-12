using UnityEngine;
using System.Collections;

public abstract class UIBasePanel
{
    public Transform trans;
    public GameObject gameObj;
    private string loadPath;
    public UIBasePanel(string _path) { loadPath = _path; }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
    protected abstract void OnAwakeInitUI();

    void Init()
    {
        gameObj = GameObject.Instantiate(Resources.Load(loadPath)) as GameObject;
        trans = gameObj.transform;
    }

    public void OnEnter()
    {
        Init();
        OnAwakeInitUI();
        OnActiveBefore();
        OnActive();
    }

    public void Show()
    {
        OnActiveBefore();
        OnActive();
    }

    public void OnExit()
    {
        OnExitBefore();
        OnDeactive();
    }

    public void OnRelease()
    {
        GameObject.Destroy(gameObj);
    }

    protected virtual void OnExitBefore()
    {
        //退出队列前的操作，比如UI操作
    }

    protected virtual void OnActiveBefore()
    {

    }
    private void OnActive()
    {
        gameObj.SetActive(true);
    }

    private void OnDeactive()
    {
        gameObj.SetActive(false);
    }
}
