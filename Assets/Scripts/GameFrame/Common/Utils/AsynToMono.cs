using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;

public class AsyncStruct
{
    public Action<object> action;
    public object state;

    public AsyncStruct(Action<object> _action, object _state)
    {
        action = _action;
        state = _state;
    }
}

public class AsynToMono : MonoBehaviour
{
    public static AsynToMono Instance = null;
    Queue<AsyncStruct> que = new Queue<AsyncStruct>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddPacFromClient(AsyncStruct asy)
    {
        lock (que)
        {
            que.Enqueue(asy);
        }
    }

    private void Update()
    {
        if (que.Count == 0)
        {
            return;
        }
        lock (que)
        {
            AsyncStruct asy = que.Dequeue();
            asy.action(asy.state);
        }
    }
}
