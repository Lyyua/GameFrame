
using System;
using System.Collections.Generic;
using UnityEngine;

public class SquatReloadAnimationPlay : BaseAnimationPlay
{
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public SquatReloadAnimationPlay()
    {
    }

    public override AnimationCMD CMDFilter(List<AnimationCMD> cmds)
    {
        return AnimationCMD.None;
    }

    public override void HandleInput(AnimationCMD cmd)
    {
        AnimationSystem.Instance.curAnim = this;
        curAnimData = AnimationSystem.Instance.animInfo.reload_squat;
        if (_irow == 0)
        {
            //音效播放
        }
    }

    public override void OnExit()
    {
        _irow = 0;
    }
    public override void OnUpdate()
    {
        bool complete = AnimationSystem.Instance.animCycle.AnimPlay(curAnimData, ref _irow);
        if (complete)
        {
            OnExit();
            AnimationFactory.GetAnimation<SquatAnimationPlay>().HandleInput(AnimationCMD.None);
            //子弹充满
        }
        else
        {
            _irow++;
        }
    }
}

