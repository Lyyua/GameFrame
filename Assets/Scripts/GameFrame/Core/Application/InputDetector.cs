using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class InputDetector : MonoBehaviour
{

    private void Update()
    {
        #region 安卓返回退出的功能

        InputGetKeyDown(KeyCode.Escape);

        #endregion 安卓返回退出的功能
    }

    private void InputGetKeyDown(KeyCode mKeyCode)
    {
        if (Input.GetKeyDown(mKeyCode))
        {
            switch (mKeyCode)
            {
                case KeyCode.None:
                    break;
                case KeyCode.Return:
                    break;
                case KeyCode.Pause:
                    break;
                case KeyCode.Escape:
                    if (Application.platform == RuntimePlatform.Android)
                    {

                    }
                    break;
                case KeyCode.A:
                    break;
                case KeyCode.B:
                    break;
                case KeyCode.C:
                    break;
                case KeyCode.D:
                    break;
                case KeyCode.E:
                    break;
                case KeyCode.F:
                    break;
                case KeyCode.G:
                    break;
                case KeyCode.H:
                    break;
                case KeyCode.I:
                    break;
                case KeyCode.J:
                    break;
                case KeyCode.K:
                    break;
                case KeyCode.L:
                    break;
                case KeyCode.M:
                    break;
                case KeyCode.N:
                    break;
                case KeyCode.O:
                    break;
                case KeyCode.P:
                    break;
                case KeyCode.Q:
                    break;
                case KeyCode.R:
                    break;
                case KeyCode.S:
                    break;
                case KeyCode.T:
                    break;
                case KeyCode.U:
                    break;
                case KeyCode.V:
                    break;
                case KeyCode.W:
                    break;
                case KeyCode.X:
                    break;
                case KeyCode.Y:
                    break;
                case KeyCode.Z:
                    break;
                default:
                    break;
            }
        }
    }
}
