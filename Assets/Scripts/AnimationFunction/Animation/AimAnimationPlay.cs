﻿
using System;
using System.Collections.Generic;
using UnityEngine;

public class AimAnimationPlay : BaseAnimationPlay
{
    AnimationCMD curCMD;
    AnimationCMD lastCMD;

    private GameObject _player;
    private AnimationSystem _sys;
    private AnimationNodesCycle _anim;
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public AimAnimationPlay(GameObject player, AnimationSystem sys, AnimationNodesCycle anim)
    {
        _player = player;
        _sys = sys;
        _anim = anim;
    }

    public override void HandleInput(ref AnimationState state, ref BaseAnimationPlay curAnim, List<AnimationCMD> cmds)
    {
        CMDFilter(cmds);
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
                curAnimData = ClientGameManager.instance.animAssetInfo.fire_stand_0;
                break;
            case AnimationCMD.MoveLeft:
                OnExit();
                _sys.SetCurAnim(_sys.aimMove, curCMD);
                break;
            case AnimationCMD.MoveRight:
                OnExit();
                _sys.SetCurAnim(_sys.aimMove, curCMD);
                break;
            case AnimationCMD.MoveFoward:
                OnExit();
                _sys.SetCurAnim(_sys.aimMove, curCMD);
                break;
            case AnimationCMD.MoveBack:
                OnExit();
                _sys.SetCurAnim(_sys.aimMove, curCMD);
                break;
            case AnimationCMD.IdleReload:
                OnExit();
                _sys.SetCurAnim(_sys.idleReload, curCMD);
                break;
            case AnimationCMD.Aim:
                state = AnimationState.Aim;
                curAnimData = ClientGameManager.instance.animAssetInfo.fire_stand_0;
                break;
            case AnimationCMD.TurnOffAim:
                OnExit();
                _sys.SetCurAnim(_sys.turnOffAim, curCMD);
                break;
            case AnimationCMD.Squart:
                break;
            case AnimationCMD.Stand:
                break;
        }
    }

    public override void OnExit()
    {
        _irow = 0;
    }

    public override void OnUpdate()
    {
        bool complete = _anim.AnimPlay(curAnimData, ref _irow);
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
    void CMDFilter(List<AnimationCMD> cmds)
    {
        curCMD = AnimationCMD.None;
        for (int i = 0; i < cmds.Count; i++)
        {
            if (cmds[i] == AnimationCMD.TurnOnAim)
            {
                continue;
            }
            else if (cmds[i] == AnimationCMD.IdleReload)
            {
                curCMD = AnimationCMD.IdleReload;
                return;
            }
            curCMD = cmds[i];
        }
    }
}
