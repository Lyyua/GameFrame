
using System;
using System.Collections.Generic;
using UnityEngine;

public class SquatMoveAnimationPlay : BaseAnimationPlay
{
    AnimationCMD curCMD;
    AnimationCMD lastCMD;

    private GameObject _player;
    private AnimationNodesCycle _anim;
    private SignalDispatchSystem _sys;
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow;

    public SquatMoveAnimationPlay(GameObject player, SignalDispatchSystem sys, AnimationNodesCycle anim)
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
            //指令发生改变，强制中断切换
            _irow = 0;
        }
        curAnim = this;
        switch (curCMD)
        {
            case AnimationCMD.None:
                OnExit();
                _sys.SetCurAnim(_sys.squat, AnimationCMD.None);
                break;
            case AnimationCMD.MoveLeft:
                curAnimData = ClientGameManager.instance.animAssetInfo.left_squat_shoot;
                state = AnimationState.SquatMove;
                if (_sys.netView == null) return;
                if (_sys.netView.isMine)
                {
                    _sys.moveCtrl.Move(-1, 0);
                }
                return;
            case AnimationCMD.MoveRight:
                curAnimData = ClientGameManager.instance.animAssetInfo.right_squat_shoot;
                state = AnimationState.SquatMove;
                if (_sys.netView == null) return;
                if (_sys.netView.isMine)
                {
                    _sys.moveCtrl.Move(1, 0);
                }
                return;
            case AnimationCMD.MoveFoward:
                curAnimData = ClientGameManager.instance.animAssetInfo.forward_squat_shoot;
                state = AnimationState.SquatMove;
                if (_sys.netView == null) return;
                if (_sys.netView.isMine)
                {
                    _sys.moveCtrl.Move(0, 1);
                }
                return;
            case AnimationCMD.MoveBack:
                curAnimData = ClientGameManager.instance.animAssetInfo.back_squat_shoot;
                state = AnimationState.SquatMove;
                if (_sys.netView == null) return;
                if (_sys.netView.isMine)
                {
                    _sys.moveCtrl.Move(0, -1);
                }
                return;
            case AnimationCMD.Reload:
                OnExit();
                _sys.SetCurAnim(_sys.squatReload, curCMD);
                break;
            case AnimationCMD.SquatToIdle:
                OnExit();
                _sys.SetCurAnim(_sys.squatToIdle, curCMD);
                break;
        }
    }

    void CMDFilter(List<AnimationCMD> cmds)
    {
        curCMD = AnimationCMD.None;
        for (int i = 0; i < cmds.Count; i++)
        {
            if (cmds[i] == AnimationCMD.TurnOnAim || cmds[i] == AnimationCMD.TurnOffAim || cmds[i] == AnimationCMD.IdleToSquat)
            {
                continue;
            }
            else if (cmds[i] == AnimationCMD.Reload)
            {
                curCMD = AnimationCMD.Reload;
                return;
            }
            else if (cmds[i] == AnimationCMD.Fire)
            {
                _sys.autoFire.Fire(true);
                continue;
            }
            curCMD = cmds[i];
        }
    }

    public override void OnExit()
    {
        _irow = 0;
        _sys.moveCtrl.Move(0, 0);
    }

    public override void OnUpdate()
    {
        bool complete = _anim.AnimPlay(curAnimData, ref _irow);
        if (complete)
        {
            _irow = 0;

        }
        else
        {
            _irow++;
        }
    }
}

