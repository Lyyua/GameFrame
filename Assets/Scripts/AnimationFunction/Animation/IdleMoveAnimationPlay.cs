
using System;
using System.Collections.Generic;
using UnityEngine;

public class IdleMoveAnimationPlay : BaseAnimationPlay
{
    AnimationCMD curCMD;
    AnimationCMD lastCMD;

    private float lastFootstepTime;
    private GameObject _player;
    private AnimationNodesCycle _anim;
    private AnimationSystem _sys;
    private List<Nodes[]> curAnimData; //动画指令托管给循环频率
    int _irow;

    public IdleMoveAnimationPlay(GameObject player, AnimationSystem sys, AnimationNodesCycle anim)
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

        switch (curCMD)
        {
            case AnimationCMD.None:
                OnExit();
                _sys.SetCurAnim(_sys.idle, curCMD);
                break;
            case AnimationCMD.MoveLeft:
                curAnimData = ClientGameManager.instance.animAssetInfo.left_stand;
                state = AnimationState.IdleMove;
                curAnim = _sys.idleMove;
                return;
            case AnimationCMD.MoveRight:
                curAnimData = ClientGameManager.instance.animAssetInfo.right_stand;
                state = AnimationState.IdleMove;
                curAnim = _sys.idleMove;
                return;
            case AnimationCMD.MoveFoward:
                curAnimData = ClientGameManager.instance.animAssetInfo.forward_stand;
                state = AnimationState.IdleMove;
                curAnim = _sys.idleMove;
                return;
            case AnimationCMD.MoveBack:
                curAnimData = ClientGameManager.instance.animAssetInfo.back_stand;
                state = AnimationState.IdleMove;
                curAnim = _sys.idleMove;
                return;
            case AnimationCMD.TurnOnAim:
                OnExit();
                _sys.SetCurAnim(_sys.aim, AnimationCMD.Aim);
                break;
        }
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
            if (cmds[i] == AnimationCMD.TurnOffAim)
            {
                continue;
            }
            else if (cmds[i] == AnimationCMD.IdleReload)
            {
                curCMD = cmds[i];
                return;
            }
            else if (cmds[i] == AnimationCMD.TurnOnAim)
            {
                curCMD = cmds[i];
                return;
            }
            curCMD = cmds[i];
        }
    }

    public void PlayWalkAudio()
    {
        if (Time.time > lastFootstepTime + 0.55f)
        {
            EffectManager.PlayEffect("Walk", _player.transform.position, _player.transform.rotation);
            lastFootstepTime = Time.time;
        }
    }

    public void PlayRunAudio()
    {
        if (Time.time > lastFootstepTime + 0.37f)
        {
            EffectManager.PlayEffect("Run", _player.transform.position, _player.transform.rotation);
            lastFootstepTime = Time.time;
        }
    }
}

