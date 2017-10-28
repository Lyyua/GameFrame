
using System;
using System.Collections.Generic;
using UnityEngine;

public class SquatMoveAnimationPlay : BaseAnimationPlay
{
    AnimationCMD lastCMD;

    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow;

    public SquatMoveAnimationPlay()
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
                AnimationFactory.GetAnimation<SquatAnimationPlay>().HandleInput(AnimationCMD.None);
                break;
            case AnimationCMD.MoveLeft:
                curAnimData = AnimationSystem.Instance.animInfo.left_squat_shoot;
                return;
            case AnimationCMD.MoveRight:
                curAnimData = AnimationSystem.Instance.animInfo.right_squat_shoot;
                return;
            case AnimationCMD.MoveFoward:
                curAnimData = AnimationSystem.Instance.animInfo.forward_squat_shoot;
                return;
            case AnimationCMD.MoveBack:
                curAnimData = AnimationSystem.Instance.animInfo.back_squat_shoot;
                return;
            case AnimationCMD.Reload:
                OnExit();
                AnimationFactory.GetAnimation<SquatReloadAnimationPlay>().HandleInput(curCMD);
                break;
            case AnimationCMD.SquatToIdle:
                OnExit();
                AnimationFactory.GetAnimation<SquatToIdlePlay>().HandleInput(curCMD);
                break;
        }
    }

    public override AnimationCMD CMDFilter(List<AnimationCMD> cmds)
    {
        AnimationCMD filterCmd = AnimationCMD.None;
        for (int i = 0; i < cmds.Count; i++)
        {
            if (cmds[i] == AnimationCMD.TurnOnAim || cmds[i] == AnimationCMD.TurnOffAim || cmds[i] == AnimationCMD.IdleToSquat)
            {
                continue;
            }
            else if (cmds[i] == AnimationCMD.Reload)
            {
                filterCmd = AnimationCMD.Reload;
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
        bool complete = AnimationSystem.Instance.animCycle.AnimPlay(curAnimData, ref _irow);
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

