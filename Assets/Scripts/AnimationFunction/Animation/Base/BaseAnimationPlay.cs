using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseAnimationPlay : BaseFSM
{
    //处理混合指令，实际上只能有一个指令起作用，当前版本混合指令的处理只是筛选   高级的用法其实应该是混合动画。
    //某些情况实际上很难触发，安全处理
    public abstract void HandleInput(AnimationCMD cmd);
    public abstract AnimationCMD CMDFilter(List<AnimationCMD> cmds);
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
}
