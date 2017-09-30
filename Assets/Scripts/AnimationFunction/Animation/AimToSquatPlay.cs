
using System;
using System.Collections.Generic;
using UnityEngine;

public class AimToSquatPlay : BaseAnimationPlay
{
    private GameObject _player;
    private AnimationNodesCycle _anim;
    private SignalDispatchSystem _sys;
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public AimToSquatPlay(GameObject player, SignalDispatchSystem sys, AnimationNodesCycle anim)
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
    }
    public override void OnUpdate()
    {
        bool complete = false;
        complete = _anim.AnimPlayLowerBody(curAnimData, ref _irow);

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

