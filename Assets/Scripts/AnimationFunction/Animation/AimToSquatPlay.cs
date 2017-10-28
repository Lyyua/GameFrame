
using System;
using System.Collections.Generic;
using UnityEngine;

public class AimToSquatPlay : BaseAnimationPlay
{
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public AimToSquatPlay()
    {
    }

    public override AnimationCMD CMDFilter(List<AnimationCMD> cmds)
    {
        return AnimationCMD.None;
    }

    public override void HandleInput(AnimationCMD cmd)
    {
        AnimationSystem.Instance.curAnim = this;
        curAnimData = AnimationSystem.Instance.animInfo.shoot_stand_squat;
    }

    public override void OnExit()
    {
        _irow = 0;
    }
    public override void OnUpdate()
    {
        bool complete = false;
        complete = AnimationSystem.Instance.animCycle.AnimPlayLowerBody(curAnimData, ref _irow);

        if (complete)
        {
            OnExit();
            AnimationFactory.GetAnimation<SquatAnimationPlay>().HandleInput(AnimationCMD.None);
        }
        else
        {
            _irow++;
        }
    }
}

