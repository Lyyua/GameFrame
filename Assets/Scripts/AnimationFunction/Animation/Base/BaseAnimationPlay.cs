using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseAnimationPlay
{
    //处理混合指令，实际上只能有一个指令起作用，当前版本混合指令的处理只是筛选   高级的用法其实应该是混合动画。
    //某些情况实际上很难触发，安全处理
    public abstract void HandleInput(ref AnimationState state, ref BaseAnimationPlay curAnim, List<AnimationCMD> cmds);
    public abstract void OnUpdate();
    public abstract void OnExit();
}
