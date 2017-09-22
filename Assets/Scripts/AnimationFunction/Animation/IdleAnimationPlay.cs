
using System;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimationPlay : BaseAnimationPlay
{
    AnimationCMD curCMD;

    private GameObject _player;
    private AnimationNodesCycle _anim;
    private AnimationSystem _sys;
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow = 0;

    public IdleAnimationPlay(GameObject player, AnimationSystem sys, AnimationNodesCycle anim)
    {
        _player = player;
        _sys = sys;
        _anim = anim;
    }

    public override void HandleInput(ref AnimationState state, ref BaseAnimationPlay curAnim, List<AnimationCMD> cmds)
    {
        CMDFilter(cmds);
        switch (curCMD)
        {
            case AnimationCMD.None:
                curAnimData = ClientGameManager.instance.animAssetInfo.idle_stand_0;
                curAnim = _sys.idle;
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
            case AnimationCMD.IdleReload:
                OnExit();
                _sys.SetCurAnim(_sys.idleReload, curCMD);
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
            if (cmds[i] == AnimationCMD.TurnOffAim)
            {
                continue;
            }
            else if (cmds[i] == AnimationCMD.IdleReload)
            {
                curCMD = AnimationCMD.IdleReload;
                return;
            }
            curCMD = cmds[i];
        }
    }
    public override void OnUpdate()
    {
        _anim.AnimPlay(ClientGameManager.instance.animAssetInfo.idle_stand_0, ref _irow);
    }
}

