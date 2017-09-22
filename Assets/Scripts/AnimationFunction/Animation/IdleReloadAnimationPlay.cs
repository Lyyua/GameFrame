
using System;
using System.Collections.Generic;
using UnityEngine;

public class IdleReloadAnimationPlay : BaseAnimationPlay
{
    AnimationCMD curCMD;
    AnimationCMD lastCMD;

    private GameObject _player;
    private AnimationNodesCycle _anim;
    private AnimationSystem _sys;
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public IdleReloadAnimationPlay(GameObject player, AnimationSystem sys, AnimationNodesCycle anim)
    {
        _player = player;
        _anim = anim;
        _sys = sys;
    }

    public override void HandleInput(ref AnimationState state, ref BaseAnimationPlay curAnim, List<AnimationCMD> cmds)
    {
        CMDFilter(cmds);
        if (lastCMD != curCMD)
        {
            lastCMD = curCMD;
            //指令发生改变，不可中断
            return;
        }
        curAnimData = ClientGameManager.instance.animAssetInfo.reload_stand;
        state = AnimationState.IdleReload;
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
            _irow = 0;
            if (curCMD == AnimationCMD.TurnOnAim)
            {
                _sys.SetCurAnim(_sys.aim, AnimationCMD.Aim);
            }
            else
            {
                _sys.SetCurAnim(_sys.idle, AnimationCMD.None);
            }
        }
        else
        {
            _irow++;
        }
    }
}

