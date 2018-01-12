using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PoolObjectStruct
{
    public GameObject originGo;
    public List<GameObject> enableCollection = new List<GameObject>();
    public List<GameObject> unableCollection = new List<GameObject>();
}

public class PoolKey
{
    public const string BLOOD = "blood";
    public const string FLARE = "flare";
}

public class LyuPool : MonoBehaviour
{
    static Dictionary<string, PoolObjectStruct> pool = new Dictionary<string, PoolObjectStruct>();

    //初始化池内数据
    public static void InitObjectPool(string keyname, GameObject pEntity, int count)
    {
        if (pool.ContainsKey(keyname))
        {
            pEntity.SetActive(false);
            pool[keyname].originGo = pEntity;
        }
        else
        {
            PoolObjectStruct objstruct = new PoolObjectStruct();
            pEntity.SetActive(false);
            objstruct.originGo = pEntity;
            pool.Add(keyname, objstruct);

        }
        for (int i = 0; i < count; i++)
        {
            pool[keyname].enableCollection.Add(GameObject.Instantiate(pEntity));
        }
    }

    //从池内获取
    public static GameObject GetFromPool(string keyname)
    {
        if (!pool.ContainsKey(keyname))
        {
            Debug.LogException(new Exception("没有初始化池"));
            return null;
        }
        else
        {
            if (pool[keyname].enableCollection.Count > 0)
            {
                GameObject go = pool[keyname].enableCollection[0];
                pool[keyname].enableCollection.RemoveAt(0);
                pool[keyname].unableCollection.Add(go);
                go.SetActive(true);
                return go;
            }
            else
            {
                GameObject go = GameObject.Instantiate(pool[keyname].originGo);
                pool[keyname].unableCollection.Add(go);
                go.SetActive(true);
                return go;
            }
        }
    }

    public static void WaitToDestroy(float dtime, string key, GameObject go)
    {
        ApplicationMgr.Instance.StartCoroutine(WaitToDo(dtime, key, go));
    }

    static IEnumerator WaitToDo(float dtime, string key, GameObject go)
    {
        yield return new WaitForSeconds(dtime);
        Recycle(key, go);
    }

    //回收入池
    public static void Recycle(string keyname, GameObject pEntity)
    {
        if (!pool.ContainsKey(keyname))
        {
            Debug.LogException(new Exception("不存在这个实例，从未创建过，或者意外销毁"));
        }
        else
        {
            List<GameObject> unableCol = pool[keyname].unableCollection;
            for (int i = 0; i < unableCol.Count; i++)
            {
                if (unableCol[i] == pEntity)
                {
                    pEntity.SetActive(false);
                    pool[keyname].unableCollection.Remove(pEntity);
                    pool[keyname].enableCollection.Add(pEntity);
                    break;
                }
            }
        }
    }
}

