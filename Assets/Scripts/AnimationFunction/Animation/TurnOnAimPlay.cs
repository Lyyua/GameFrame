
using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnAimPlay : BaseAnimationPlay
{
    AnimationCMD curCMD;
    AnimationCMD lastCMD;

    private GameObject _player;
    private AnimationNodesCycle _anim;
    private AnimationSystem _sys;
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public TurnOnAimPlay(GameObject player, AnimationSystem sys, AnimationNodesCycle anim)
    {
        _player = player;
        _anim = anim;
        _sys = sys;
    }

    public override void HandleInput(ref AnimationState state, ref BaseAnimationPlay curAnim, List<AnimationCMD> cmds)
    {
        if (lastCMD != curCMD)
        {
            lastCMD = curCMD;
            //指令发生改变，不可中断
            return;
        }
        curAnimData = ClientGameManager.instance.animAssetInfo.idle_shootStand;
        state = AnimationState.TurnOnAim;
    }

    public override void OnExit()
    {

    }

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
            _irow = 0;
            _sys.SetCurAnim(_sys.aim, AnimationCMD.Aim);
        }
        else
        {
            _irow++;
        }
    }
}

