
using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnAimPlay : BaseAnimationPlay
{
    AnimationCMD curCMD;
    AnimationCMD lastCMD;

    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public TurnOnAimPlay()
    {
    }

    public override AnimationCMD CMDFilter(List<AnimationCMD> cmds)
    {
        return AnimationCMD.None;
    }

    public override void HandleInput(AnimationCMD cmd)
    {
        AnimationSystem.Instance.curAnim = this;
        curAnimData = AnimationSystem.Instance.animInfo.idle_shootStand;
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
            _irow = 0;
            AnimationFactory.GetAnimation<AimAnimationPlay>().HandleInput(AnimationCMD.Aim);
        }
        else
        {
            _irow++;
        }
    }
}

