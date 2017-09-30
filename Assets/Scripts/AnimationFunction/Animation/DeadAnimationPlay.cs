
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnimationPlay : BaseAnimationPlay
{
    AnimationCMD curCMD;

    private GameObject _player;
    private AnimationNodesCycle _anim;
    private SignalDispatchSystem _sys;
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;
    public int deadDir;
    public DeadAnimationPlay(GameObject player, SignalDispatchSystem sys, AnimationNodesCycle anim)
    {
        _player = player;
        _anim = anim;
        _sys = sys;
    }

    public override void HandleInput(ref AnimationState state, ref BaseAnimationPlay curAnim, List<AnimationCMD> cmds)
    {
        curAnim = this;
        if (deadDir > 5)
        {
            curAnimData = ClientGameManager.instance.animAssetInfo.dead_stand_qian;
        }
        else
        {
            curAnimData = ClientGameManager.instance.animAssetInfo.dead_stand_hou;
        }
        state = AnimationState.Dead;
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
        }
        else
        {
            _irow++;
        }
    }
}

