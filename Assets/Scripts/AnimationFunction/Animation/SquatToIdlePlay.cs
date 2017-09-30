
using System;
using System.Collections.Generic;
using UnityEngine;

public class SquatToIdlePlay : BaseAnimationPlay
{
    AnimationCMD curCMD;

    private GameObject _player;
    private AnimationNodesCycle _anim;
    private SignalDispatchSystem _sys;
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public SquatToIdlePlay(GameObject player, SignalDispatchSystem sys, AnimationNodesCycle anim)
    {
        _player = player;
        _anim = anim;
        _sys = sys;
    }

    public override void HandleInput(ref AnimationState state, ref BaseAnimationPlay curAnim, List<AnimationCMD> cmds)
    {
        CMDFilter(cmds);
        curAnim = this;
        curAnimData = ClientGameManager.instance.animAssetInfo.shoot_squat_stand;
        state = AnimationState.SquatToIdle;
    }

    public override void OnExit()
    {
        _irow = 0;
    }

    void CMDFilter(List<AnimationCMD> cmds)
    {
        curCMD = AnimationCMD.None;
        for (int i = 0; i < cmds.Count; i++)
        {
            if (cmds[i] == AnimationCMD.TurnOnAim)
            {
                curCMD = AnimationCMD.Aim;
                return;
            }
            curCMD = cmds[i];
        }
    }

    public override void OnUpdate()
    {
        bool complete = _anim.AnimPlayLowerBody(curAnimData, ref _irow);
        if (complete)
        {
            _irow = 0;
            if (curCMD == AnimationCMD.Aim)
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

