﻿
using System;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimationPlay : BaseAnimationPlay
{
    AnimationCMD curCMD;

    private GameObject _player;
    private AnimationNodesCycle _anim;
    private SignalDispatchSystem _sys;
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public IdleAnimationPlay(GameObject player, SignalDispatchSystem sys, AnimationNodesCycle anim)
    {
        _player = player;
        _sys = sys;
        _anim = anim;
    }

    public override void HandleInput(ref AnimationState state, ref BaseAnimationPlay curAnim, List<AnimationCMD> cmds)
    {
        CMDFilter(cmds);
        curAnim = this;
        switch (curCMD)
        {
            case AnimationCMD.None:
                curAnimData = ClientGameManager.instance.animAssetInfo.idle_stand_0;
                state = AnimationState.Idle;
                break;
            case AnimationCMD.MoveLeft:
                OnExit();
                _sys.SetCurAnim(_sys.idleMove, curCMD);
                break;
            case AnimationCMD.MoveRight:
                OnExit();
                _sys.SetCurAnim(_sys.idleMove, curCMD);
                break;
            case AnimationCMD.MoveFoward:
                OnExit();
                _sys.SetCurAnim(_sys.idleMove, curCMD);
                break;
            case AnimationCMD.MoveBack:
                OnExit();
                _sys.SetCurAnim(_sys.idleMove, curCMD);
                break;
            case AnimationCMD.TurnOnAim:
                OnExit();
                _sys.SetCurAnim(_sys.turnOnAim, curCMD);
                break;
            case AnimationCMD.Reload:
                OnExit();
                _sys.SetCurAnim(_sys.idleReload, curCMD);
                break;
            case AnimationCMD.IdleToSquat:
                OnExit();
                _sys.SetCurAnim(_sys.idleToSquat, curCMD);
                break;
        }
    }

    public override void OnExit()
    {
        _irow = 0;
    }
    //条件处理过滤
    void CMDFilter(List<AnimationCMD> cmds)
    {
        curCMD = AnimationCMD.None;
        for (int i = 0; i < cmds.Count; i++)
        {
            if (cmds[i] == AnimationCMD.TurnOffAim || cmds[i] == AnimationCMD.SquatToIdle)
            {
                continue;
            }
            else if (cmds[i] == AnimationCMD.Reload)
            {
                curCMD = AnimationCMD.Reload;
                return;
            }
            else if (cmds[i] == AnimationCMD.Fire)
            {
                _sys.autoFire.Fire(false);
                continue;
            }
            curCMD = cmds[i];
        }
    }
    public override void OnUpdate()
    {
        _anim.AnimPlay(ClientGameManager.instance.animAssetInfo.idle_stand_0, ref _irow);
    }
}

