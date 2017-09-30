
using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnAimPlay : BaseAnimationPlay
{
    AnimationCMD curCMD;
    AnimationCMD lastCMD;

    private GameObject _player;
    private AnimationNodesCycle _anim;
    private SignalDispatchSystem _sys;
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public TurnOnAimPlay(GameObject player, SignalDispatchSystem sys, AnimationNodesCycle anim)
    {
        _player = player;
        _anim = anim;
        _sys = sys;
    }

    public override void HandleInput(ref AnimationState state, ref BaseAnimationPlay curAnim, List<AnimationCMD> cmds)
    {
        curAnim = this;
        curAnimData = ClientGameManager.instance.animAssetInfo.idle_shootStand;
        state = AnimationState.TurnOnAim;
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
            _sys.SetCurAnim(_sys.aim, AnimationCMD.Aim);
        }
        else
        {
            _irow++;
        }
    }
}

