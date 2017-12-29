using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UIAnimMadeModelConst
{
    public const string KEY_AnimInfo = "AnimInfo";
}

public class UIAnimMadeModel : UIBaseModel
{
    private Animation curAnim;
    public Animation CurAnim
    {
        get
        {
            return curAnim;
        }

        set
        {
            Animation old = curAnim;
            curAnim = value;
            ValueChangeArgs ve = new ValueChangeArgs(UIAnimMadeModelConst.KEY_AnimInfo, old, value);
            DispatchValueUpdateEvent(ve);
        }
    }
}

