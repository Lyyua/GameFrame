
using System;
using System.Collections.Generic;
using UnityEngine;

public class IdleReloadAnimationPlay : BaseAnimationPlay
{
    AnimationCMD _curCmd;
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public IdleReloadAnimationPlay()
    {
    }

    public override void HandleInput(AnimationCMD curCmd)
    {
        if (_irow == 0)
        {
            //
        }
        _curCmd = curCmd;
        AnimationSystem.Instance.curAnim = this;
        curAnimData = AnimationSystem.Instance.animInfo.reload_stand;
    }

    public override void OnExit()
    {
        _irow = 0;
    }
    // 可以接收移动指令并响应，只不过没写，因为需求不是这样
    public override AnimationCMD CMDFilter(List<AnimationCMD> cmds)
    {
        AnimationCMD filterCmd = AnimationCMD.None;
        for (int i = 0; i < cmds.Count; i++)
        {
            if (cmds[i] == AnimationCMD.TurnOnAim)
            {
                filterCmd = AnimationCMD.TurnOnAim;
                break;
            }
            else if (cmds[i] == AnimationCMD.TurnOffAim)
            {
                filterCmd = AnimationCMD.TurnOffAim;
                break;
            }
            filterCmd = cmds[i];
        }
        return filterCmd;
    }
    public override void OnUpdate()
    {
        bool complete = AnimationSystem.Instance.animCycle.AnimPlay(curAnimData, ref _irow);
        if (complete)
        {
            if (_curCmd == AnimationCMD.TurnOnAim)
            {
                //设置下一帧
                OnExit();
                AnimationFactory.GetAnimation<AimAnimationPlay>().HandleInput(AnimationCMD.Aim);
            }
            else
            {
                //设置下一帧
                OnExit();
                AnimationFactory.GetAnimation<IdleAnimationPlay>().HandleInput(AnimationCMD.None);
            }
            //子弹充满
        }
        else
        {
            _irow++;
        }
    }
}

