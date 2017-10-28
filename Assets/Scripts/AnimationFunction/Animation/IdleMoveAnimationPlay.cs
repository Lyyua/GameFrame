
using System;
using System.Collections.Generic;
using UnityEngine;

public class IdleMoveAnimationPlay : BaseAnimationPlay
{
    AnimationCMD lastCMD;

    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    bool runTag;
    int _irow;

    public IdleMoveAnimationPlay()
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
                OnExit();
                AnimationFactory.GetAnimation<IdleAnimationPlay>().HandleInput(AnimationCMD.Aim);
                break;
            case AnimationCMD.MoveLeft:
                curAnimData = AnimationSystem.Instance.animInfo.left_stand;
                //移动音效
                //位移
                return;
            case AnimationCMD.MoveRight:
                curAnimData = AnimationSystem.Instance.animInfo.right_stand;
                return;
            case AnimationCMD.MoveFoward:
                //走跑切换
                if (runTag)
                {
                    curAnimData = AnimationSystem.Instance.animInfo.run;
                }
                else
                {
                    curAnimData = AnimationSystem.Instance.animInfo.forward_stand;
                }
                return;
            case AnimationCMD.MoveBack:
                curAnimData = AnimationSystem.Instance.animInfo.back_stand;
                return;
            case AnimationCMD.Reload:
                OnExit();
                AnimationFactory.GetAnimation<IdleReloadAnimationPlay>().HandleInput(curCMD);
                break;
            case AnimationCMD.TurnOnAim:
                OnExit();
                AnimationFactory.GetAnimation<AimAnimationPlay>().HandleInput(curCMD);
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
                filterCmd = cmds[i];
                break;
            }
            else if (cmds[i] == AnimationCMD.TurnOnAim)
            {
                filterCmd = cmds[i];
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

