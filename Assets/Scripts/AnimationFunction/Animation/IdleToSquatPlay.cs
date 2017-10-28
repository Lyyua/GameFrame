
using System;
using System.Collections.Generic;
using UnityEngine;

public class IdleToSquatPlay : BaseAnimationPlay
{
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;
    bool hasHold = false; //下蹲后姿态确认

    public IdleToSquatPlay()
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
        hasHold = false;
    }
    public override void OnUpdate()
    {
        bool complete = false;
        if (hasHold)
        {
            complete = AnimationSystem.Instance.animCycle.AnimPlayLowerBody(curAnimData, ref _irow);
        }
        else
        {
            complete = AnimationSystem.Instance.animCycle.AnimPlay(curAnimData, ref _irow);
            hasHold = true;
        }
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

