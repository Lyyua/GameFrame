
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnimationPlay : BaseAnimationPlay
{
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;
    public int deadDir;
    public DeadAnimationPlay()
    {
    }

    public override void HandleInput(AnimationCMD cmd)
    {
        AnimationSystem.Instance.curAnim = this;
        if (deadDir > 5)
        {
            curAnimData = AnimationSystem.Instance.animInfo.dead_stand_qian;
        }
        else
        {
            curAnimData = AnimationSystem.Instance.animInfo.dead_stand_hou;
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
        }
        else
        {
            _irow++;
        }
    }

    public override AnimationCMD CMDFilter(List<AnimationCMD> cmds)
    {
        return AnimationCMD.None;
    }
}

