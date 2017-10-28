
using System;
using System.Collections.Generic;
using UnityEngine;

public class AimMoveAnimationPlay : BaseAnimationPlay
{
    AnimationCMD lastCMD;

    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow;

    public AimMoveAnimationPlay()
    {
    }

    public override void HandleInput(AnimationCMD curCMD)
    {
        if (lastCMD != curCMD)
        {
            lastCMD = curCMD;
            //指令发生改变，强制中断切换
            _irow = 0;
        }
        AnimationSystem.Instance.curAnim = this;
        switch (curCMD)
        {
            case AnimationCMD.None:
                OnExit();
                AnimationFactory.GetAnimation<AimAnimationPlay>().HandleInput(AnimationCMD.Aim);
                break;
            case AnimationCMD.MoveLeft:
                curAnimData = AnimationSystem.Instance.animInfo.left_stand_shoot;
                return;
            case AnimationCMD.MoveRight:
                curAnimData = AnimationSystem.Instance.animInfo.right_stand_shoot;
                return;
            case AnimationCMD.MoveFoward:
                curAnimData = AnimationSystem.Instance.animInfo.forward_stand_shoot;
                return;
            case AnimationCMD.MoveBack:
                curAnimData = AnimationSystem.Instance.animInfo.back_stand_shoot;
                return;
            case AnimationCMD.Reload:
                OnExit();
                AnimationFactory.GetAnimation<IdleReloadAnimationPlay>().HandleInput(curCMD);
                break;
            case AnimationCMD.TurnOffAim:
                OnExit();
                AnimationFactory.GetAnimation<IdleAnimationPlay>().HandleInput(AnimationCMD.None);
                break;
            case AnimationCMD.IdleToSquat:
                OnExit();
                AnimationFactory.GetAnimation<AimToSquatPlay>().HandleInput(curCMD);
                break;
        }
    }

    public override AnimationCMD CMDFilter(List<AnimationCMD> cmds)
    {
        AnimationCMD filterCmd = AnimationCMD.None;
        for (int i = 0; i < cmds.Count; i++)
        {
            if (cmds[i] == AnimationCMD.TurnOnAim || cmds[i] == AnimationCMD.SquatToIdle)
            {
                continue;
            }
            else if (cmds[i] == AnimationCMD.Reload)
            {
                filterCmd = AnimationCMD.Reload;
                break;
            }
            else if (cmds[i] == AnimationCMD.TurnOffAim)
            {
                filterCmd = AnimationCMD.TurnOffAim;
                break;
            }
            else if (cmds[i] == AnimationCMD.Fire)
            {
                continue;
            }
            filterCmd = cmds[i];
        }
        return filterCmd;
    }

    public override void OnExit()
    {
        _irow = 0;
    }

    public override void OnUpdate()
    {
        bool complete = AnimationSystem.Instance.animCycle.AnimPlayLowerBody(curAnimData, ref _irow);
        if (complete)
        {
            _irow = 0;

        }
        else
        {
            _irow++;
        }
    }
}

