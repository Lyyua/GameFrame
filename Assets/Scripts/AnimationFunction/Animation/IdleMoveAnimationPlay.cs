
using System;
using System.Collections.Generic;
using UnityEngine;

public class IdleMoveAnimationPlay : BaseAnimationPlay
{
    AnimationCMD curCMD;
    AnimationCMD lastCMD;

    private GameObject _player;
    private AnimationNodesCycle _anim;
    private SignalDispatchSystem _sys;

    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    bool runTag;
    int _irow;

    public IdleMoveAnimationPlay(GameObject player, SignalDispatchSystem sys, AnimationNodesCycle anim)
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
                _sys.SetCurAnim(_sys.idle, curCMD);
                break;
            case AnimationCMD.MoveLeft:
                curAnimData = ClientGameManager.instance.animAssetInfo.left_stand;
                state = AnimationState.IdleMove;
                _sys.audioCtrl.PlayWalkAudio(_player.transform);
                if (_sys.netView == null) return;
                if (_sys.netView.isMine)
                {
                    _sys.moveCtrl.Move(-1, 0);
                }
                return;
            case AnimationCMD.MoveRight:
                curAnimData = ClientGameManager.instance.animAssetInfo.right_stand;
                state = AnimationState.IdleMove;
                _sys.audioCtrl.PlayWalkAudio(_player.transform);
                if (_sys.netView == null) return;
                if (_sys.netView.isMine)
                {
                    _sys.moveCtrl.Move(1, 0);
                }
                return;
            case AnimationCMD.MoveFoward:
                if (runTag != _sys._isRun)
                {
                    //走跑切换后，索引重置
                    _irow = 0;
                    runTag = _sys._isRun;
                }
                if (_sys._isRun)
                {
                    curAnimData = ClientGameManager.instance.animAssetInfo.run;
                    _sys.audioCtrl.PlayRunAudio(_player.transform);
                    if (_sys.netView == null) return;
                    if (_sys.netView.isMine)
                    {
                        _sys.moveCtrl.Move(0, 2);
                    }
                }
                else
                {
                    curAnimData = ClientGameManager.instance.animAssetInfo.forward_stand;
                    _sys.audioCtrl.PlayWalkAudio(_player.transform);
                    if (_sys.netView == null) return;
                    if (_sys.netView.isMine)
                    {
                        _sys.moveCtrl.Move(0, 1);
                    }
                }
                state = AnimationState.IdleMove;
                return;
            case AnimationCMD.MoveBack:
                curAnimData = ClientGameManager.instance.animAssetInfo.back_stand;
                state = AnimationState.IdleMove;
                _sys.audioCtrl.PlayWalkAudio(_player.transform);
                if (_sys.netView == null) return;
                if (_sys.netView.isMine)
                {
                    _sys.moveCtrl.Move(0, -1);
                }
                return;
            case AnimationCMD.Reload:
                OnExit();
                _sys.SetCurAnim(_sys.idleReload, curCMD);
                break;
            case AnimationCMD.TurnOnAim:
                OnExit();
                _sys.SetCurAnim(_sys.aim, AnimationCMD.Aim);
                break;
            case AnimationCMD.IdleToSquat:
                OnExit();
                _sys.SetCurAnim(_sys.idleToSquat, curCMD);
                break;
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
    //条件处理过滤
    void CMDFilter(List<AnimationCMD> cmds)
    {
        curCMD = AnimationCMD.None;
        for (int i = 0; i < cmds.Count; i++)
        {
            if (cmds[i] == AnimationCMD.TurnOffAim || cmds[i] == AnimationCMD.SquatToIdle)
            {
                continue;
            }
            else if (cmds[i] == AnimationCMD.Reload)
            {
                curCMD = cmds[i];
                return;
            }
            else if (cmds[i] == AnimationCMD.TurnOnAim)
            {
                curCMD = cmds[i];
                return;
            }
            else if (cmds[i] == AnimationCMD.Fire)
            {
                _sys.autoFire.Fire(false);
                continue;
            }
            curCMD = cmds[i];
        }
    }
}

