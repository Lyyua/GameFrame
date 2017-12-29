using UnityEngine;
using System.Collections;

public class StartWindow : MonoBehaviour
{
    void Start()
    {
        UIWindowMgr.Instance.PushPanel<UIAnimOPChoose>();
    }
}
