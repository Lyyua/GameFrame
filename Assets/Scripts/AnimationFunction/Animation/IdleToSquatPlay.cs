
using System;
using System.Collections.Generic;
using UnityEngine;

public class IdleToSquatPlay : BaseAnimationPlay
{
    private GameObject _player;
    private AnimationNodesCycle _anim;
    private SignalDispatchSystem _sys;
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;
    bool hasHold = false;

    public IdleToSquatPlay(GameObject player, SignalDispatchSystem sys, AnimationNodesCycle anim)
    {
        _player = player;
        _anim = anim;
        _sys = sys;
    }

    public override void HandleInput(ref AnimationState state, ref BaseAnimationPlay curAnim, List<AnimationCMD> cmds)
    {
        curAnim = this;
        curAnimData = ClientGameManager.instance.animAssetInfo.shoot_stand_squat;
        state = AnimationState.IdleToSquat;
    }

    public override void OnExit()
    {
        _irow = 0;
        hasHold = false;
    }
    public override void OnUpdate()
    {
        bool complete = false;
        if (hasHold)
        {
            complete = _anim.AnimPlayLowerBody(curAnimData, ref _irow);
        }
        else
        {
            complete = _anim.AnimPlay(curAnimData, ref _irow);
            hasHold = true;
        }
        if (complete)
        {
            OnExit();
            _sys.SetCurAnim(_sys.squat, AnimationCMD.None);
        }
        else
        {
            _irow++;
        }
    }
}

