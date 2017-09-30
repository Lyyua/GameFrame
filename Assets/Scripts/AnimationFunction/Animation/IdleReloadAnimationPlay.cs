﻿
using System;
using System.Collections.Generic;
using UnityEngine;

public class IdleReloadAnimationPlay : BaseAnimationPlay
{
    AnimationCMD curCMD;

    private GameObject _player;
    private AnimationNodesCycle _anim;
    private SignalDispatchSystem _sys;
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public IdleReloadAnimationPlay(GameObject player, SignalDispatchSystem sys, AnimationNodesCycle anim)
    {
        _player = player;
        _anim = anim;
        _sys = sys;
    }

    public override void HandleInput(ref AnimationState state, ref BaseAnimationPlay curAnim, List<AnimationCMD> cmds)
    {
        CMDFilter(cmds);
        if (_irow == 0)
        {
            _sys.audioCtrl.PlayReLoadAudio(_anim.spawnPoint);
        }
        curAnim = this;
        curAnimData = ClientGameManager.instance.animAssetInfo.reload_stand;
        state = AnimationState.IdleReload;
    }

    public override void OnExit()
    {
        _irow = 0;
    }
    // 可以接收移动指令并响应，只不过没写，因为需求不是这样
    void CMDFilter(List<AnimationCMD> cmds)
    {
        curCMD = AnimationCMD.None;
        for (int i = 0; i < cmds.Count; i++)
        {
            if (cmds[i] == AnimationCMD.TurnOnAim)
            {
                curCMD = AnimationCMD.TurnOnAim;
                return;
            }
            else if (cmds[i] == AnimationCMD.TurnOffAim)
            {
                curCMD = AnimationCMD.TurnOffAim;
                return;
            }
            curCMD = cmds[i];
        }
    }
    public override void OnUpdate()
    {
        bool complete = _anim.AnimPlay(curAnimData, ref _irow);
        if (complete)
        {
            if (curCMD == AnimationCMD.TurnOnAim)
            {
                //设置下一帧
                OnExit();
                _sys.SetCurAnim(_sys.aim, AnimationCMD.Aim);
            }
            else
            {
                //设置下一帧
                OnExit();
                _sys.SetCurAnim(_sys.idle, AnimationCMD.None);
            }
            _sys.autoFire.BulletCountReset();
        }
        else
        {
            _irow++;
        }
    }
}

