
using System;
using System.Collections.Generic;
using UnityEngine;

public class AimAnimationPlay : BaseAnimationPlay
{
    AnimationCMD lastCMD;

    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public AimAnimationPlay()
    {
    }

    public override void HandleInput(AnimationCMD curCMD)
    {
        AnimationSystem.Instance.curAnim = this;
        if (lastCMD != curCMD)
        {
            lastCMD = curCMD;
            //指令发生改变，强制中断切换
            _irow = 0;
        }
        switch (curCMD)
        {
            case AnimationCMD.None:
                curAnimData = AnimationSystem.Instance.animInfo.fire_stand_0;
                break;
            case AnimationCMD.MoveLeft:
                OnExit();
                AnimationFactory.GetAnimation<AimMoveAnimationPlay>().HandleInput(curCMD);
                break;
            case AnimationCMD.MoveRight:
                OnExit();
                AnimationFactory.GetAnimation<AimMoveAnimationPlay>().HandleInput(curCMD);
                break;
            case AnimationCMD.MoveFoward:
                OnExit();
                AnimationFactory.GetAnimation<AimMoveAnimationPlay>().HandleInput(curCMD);
                break;
            case AnimationCMD.MoveBack:
                OnExit();
                AnimationFactory.GetAnimation<AimMoveAnimationPlay>().HandleInput(curCMD);
                break;
            case AnimationCMD.Reload:
                OnExit();
                AnimationFactory.GetAnimation<IdleReloadAnimationPlay>().HandleInput(curCMD);
                break;
            case AnimationCMD.Aim:

                curAnimData = AnimationSystem.Instance.animInfo.fire_stand_0;
                break;
            case AnimationCMD.TurnOffAim:
                OnExit();
                AnimationFactory.GetAnimation<TurnOffAimPlay>().HandleInput(curCMD);
                break;
            case AnimationCMD.IdleToSquat:
                OnExit();
                AnimationFactory.GetAnimation<AimToSquatPlay>().HandleInput(curCMD);
                break;
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
            _irow = 0;
        }
        else
        {
            _irow++;
        }
    }
    //只对当前状态生效的指令
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
            else if (cmds[i] == AnimationCMD.Fire)
            {
                continue;
            }
            filterCmd = cmds[i];
        }
        return filterCmd;
    }
}

