
using System;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimationPlay : BaseAnimationPlay
{

    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public IdleAnimationPlay()
    {
    }

    public override void HandleInput(AnimationCMD curCMD)
    {
        AnimationSystem.Instance.curAnim = this;
        switch (curCMD)
        {
            case AnimationCMD.None:
                curAnimData = AnimationSystem.Instance.animInfo.idle_stand_0;
                break;
            case AnimationCMD.MoveLeft:
                OnExit();
                AnimationFactory.GetAnimation<IdleMoveAnimationPlay>().HandleInput(curCMD);
                break;
            case AnimationCMD.MoveRight:
                OnExit();
                AnimationFactory.GetAnimation<IdleMoveAnimationPlay>().HandleInput(curCMD);
                break;
            case AnimationCMD.MoveFoward:
                OnExit();
                AnimationFactory.GetAnimation<IdleMoveAnimationPlay>().HandleInput(curCMD);
                break;
            case AnimationCMD.MoveBack:
                OnExit();
                AnimationFactory.GetAnimation<IdleMoveAnimationPlay>().HandleInput(curCMD);
                break;
            case AnimationCMD.TurnOnAim:
                OnExit();
                AnimationFactory.GetAnimation<TurnOnAimPlay>().HandleInput(curCMD);
                break;
            case AnimationCMD.Reload:
                OnExit();
                AnimationFactory.GetAnimation<IdleReloadAnimationPlay>().HandleInput(curCMD);
                break;
            case AnimationCMD.IdleToSquat:
                OnExit();
                AnimationFactory.GetAnimation<IdleToSquatPlay>().HandleInput(curCMD);
                break;
        }
    }

    public override void OnExit()
    {
        _irow = 0;
    }
    //条件处理过滤
    public override AnimationCMD CMDFilter(List<AnimationCMD> cmds)
    {
        AnimationCMD filterCmd = AnimationCMD.None;
        for (int i = 0; i < cmds.Count; i++)
        {
            if (cmds[i] == AnimationCMD.TurnOffAim || cmds[i] == AnimationCMD.SquatToIdle)
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
    public override void OnUpdate()
    {
        AnimationSystem.Instance.animCycle.AnimPlay(curAnimData, ref _irow);
    }
}

