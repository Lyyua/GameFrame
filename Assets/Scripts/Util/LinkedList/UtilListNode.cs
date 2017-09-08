using System;
using System.Collections;
[Serializable]
public class UtilListNode<T>
{
    private T _data;
    private UtilListNode<T> _next;
    private UtilListNode<T> _previous;

    public UtilListNode()
    {
        _data = default(T);
        _next = null;
        _previous = null;
    }

    public UtilListNode(T data)
    {
        _data = data;
        _next = null;
        _previous = null;
    }
    public T Data
    {
        get
        {
            return _data;
        }
        set
        {
            _data = value;
        }
    }

    public UtilListNode<T> Next
    {
        get
        {
            return _next;
        }
        set
        {
            _next = value;
        }
    }
    public UtilListNode<T> Previous
    {
        get
        {
            return _previous;
        }
        set
        {
            _previous = value;
        }
    }
}
