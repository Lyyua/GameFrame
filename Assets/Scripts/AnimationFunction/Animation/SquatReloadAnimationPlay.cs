
using System;
using System.Collections.Generic;
using UnityEngine;

public class SquatReloadAnimationPlay : BaseAnimationPlay
{
    AnimationCMD curCMD;

    private GameObject _player;
    private AnimationNodesCycle _anim;
    private SignalDispatchSystem _sys;
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public SquatReloadAnimationPlay(GameObject player, SignalDispatchSystem sys, AnimationNodesCycle anim)
    {
        _player = player;
        _anim = anim;
        _sys = sys;
    }

    public override void HandleInput(ref AnimationState state, ref BaseAnimationPlay curAnim, List<AnimationCMD> cmds)
    {
        if (_irow == 0)
        {
            _sys.audioCtrl.PlayReLoadAudio(_anim.spawnPoint);
        }
        curAnim = this;
        curAnimData = ClientGameManager.instance.animAssetInfo.reload_squat;
        state = AnimationState.SquatReload;
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
            OnExit();
            _sys.SetCurAnim(_sys.squat, AnimationCMD.None);
            _sys.autoFire.BulletCountReset();
        }
        else
        {
            _irow++;
        }
    }
}

