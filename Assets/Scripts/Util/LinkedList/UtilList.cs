using System;
using System.Collections;
using UnityEngine;
[Serializable]
public class UtilList<T>
{
    public UtilListNode<T> Head; //头哨兵
    public UtilListNode<T> Trail;//尾哨兵
    private int _count = 0;
    public int Count
    {
        get { return _count; }
    }
    public UtilListNode<T> First
    {
        get
        {
            if (!IsEmpty())
            {
                return Head.Next;
            }
            else
            {
                return null;
            }
        }
    }
    public UtilListNode<T> End
    {
        get
        {
            if (!IsEmpty())
            {
                return Trail.Previous;
            }
            else
            {
                return null;
            }
        }
    }
    public UtilList()
    {
        Head = new UtilListNode<T>();
        Trail = new UtilListNode<T>();
        Head.Next = Trail;
        Trail.Previous = Head;
    }

    public UtilListNode<T> Enqueue(T data)
    {
        UtilListNode<T> node = new UtilListNode<T>(data);
        Trail.Previous.Next = node;
        node.Previous = Trail.Previous;
        node.Next = Trail;
        Trail.Previous = node;
        _count++;
        return node;
    }

    public UtilListNode<T> Dequeue()
    {
        if (IsEmpty())
        {
            return null;
        }
        UtilListNode<T> node = Head.Next;
        UtilListNode<T> nextNode = node.Next;
        Head.Next = nextNode;
        nextNode.Previous = Head;
        _count--;
        return node;
    }

    /// <summary>
    /// 把list接在End后面，清空list
    /// </summary>
    /// <param name="list"></param>
    public void AddEndAfter(UtilList<T> list)
    {
        End.Next = list.First;
        list.First.Previous = End;

        list.End.Next = Trail;
        Trail.Previous = list.End;

        list.Clear();
    }

    public void RemoveBefore(UtilListNode<T> node)
    {
        if (node == Head || node == Trail)
        {
            Debug.LogError("不能删除头尾哨兵");
        }
        if (node == null)
        {
            Debug.LogError("删除目标为null");
        }
        Head.Next = node;
        node.Previous = Head;
    }

    public void Clear()
    {
        Head.Next = Trail;
        Trail.Previous = Head;
    }

    public bool IsEmpty()
    {
        if (Head.Next == Trail && Trail.Previous == Head)
        {
            return true;
        }
        return false;
    }
}
