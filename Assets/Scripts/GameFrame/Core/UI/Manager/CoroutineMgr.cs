using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineMgr : Singleton<CoroutineMgr>
{
    public void StartCoroutine(IEnumerator cor)
    {
        ApplicationMgr.Instance.StartCoroutine(cor);
    }

    public Coroutine StartCoroutineReturn(IEnumerator cor)
    {
        return ApplicationMgr.Instance.StartCoroutine(cor);
    }

    public void StartCoroutine(string cor)
    {
        ApplicationMgr.Instance.StartCoroutine(cor);
    }

    // 停止协程
    public void StopCoroutine(IEnumerator cor)
    {
        ApplicationMgr.Instance.StopCoroutine(cor);
    }

    public void StopCoroutine(Coroutine cor)
    {
        ApplicationMgr.Instance.StopCoroutine(cor);
    }

    public void StopCoroutine(string cor)
    {
        ApplicationMgr.Instance.StopCoroutine(cor);
    }

    // 停止所有协程
    public void StopAllCoroutines()
    {
        ApplicationMgr.Instance.StopAllCoroutines();
    }
}
