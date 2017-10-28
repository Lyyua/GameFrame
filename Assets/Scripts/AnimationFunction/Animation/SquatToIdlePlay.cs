
using System;
using System.Collections.Generic;
using UnityEngine;

public class SquatToIdlePlay : BaseAnimationPlay
{
    AnimationCMD curCMD;

    private AnimationNodesCycle _anim;
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public SquatToIdlePlay()
    {
    }

    public override void HandleInput(AnimationCMD cmd)
    {
        AnimationSystem.Instance.curAnim = this;
        curAnimData = AnimationSystem.Instance.animInfo.shoot_squat_stand;
    }

    public override void OnExit()
    {
        _irow = 0;
    }

    public override AnimationCMD CMDFilter(List<AnimationCMD> cmds)
    {
        AnimationCMD filterCmd = AnimationCMD.None;
        for (int i = 0; i < cmds.Count; i++)
        {
            if (cmds[i] == AnimationCMD.TurnOnAim)
            {
                filterCmd = AnimationCMD.Aim;
                break;
            }
            filterCmd = cmds[i];
        }
        return filterCmd;
    }

    public override void OnUpdate()
    {
        bool complete = _anim.AnimPlayLowerBody(curAnimData, ref _irow);
        if (complete)
        {
            _irow = 0;
            if (curCMD == AnimationCMD.Aim)
            {
                AnimationFactory.GetAnimation<AimAnimationPlay>().HandleInput(AnimationCMD.Aim);
            }
            else
            {
                AnimationFactory.GetAnimation<IdleAnimationPlay>().HandleInput(AnimationCMD.None);
            }
        }
        else
        {
            _irow++;
        }
    }
}

