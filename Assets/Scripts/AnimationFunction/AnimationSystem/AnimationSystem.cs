using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationSystem : MonoBehaviour
{
    public AnimationState curState = AnimationState.Idle;
    //public AnimationState lastState = AnimationState.Idle;
    public BaseAnimationPlay curAnimPlay = null;
    private AnimationNodesCycle anim;

    public IdleMoveAnimationPlay idleMove;
    public IdleAnimationPlay idle;
    public TurnOnAimPlay turnOnAim;
    public TurnOffAimPlay turnOffAim;
    public AimAnimationPlay aim;
    public AimMoveAnimationPlay aimMove;
    public IdleReloadAnimationPlay idleReload;

    private void Start()
    {
        anim = GetComponent<AnimationNodesCycle>();
        idleMove = new IdleMoveAnimationPlay(gameObject, this, anim);
        idle = new IdleAnimationPlay(gameObject, this, anim);
        turnOnAim = new TurnOnAimPlay(gameObject, this, anim);
        turnOffAim = new TurnOffAimPlay(gameObject, this, anim);
        aim = new AimAnimationPlay(gameObject, this, anim);
        aimMove = new AimMoveAnimationPlay(gameObject, this, anim);
        idleReload = new IdleReloadAnimationPlay(gameObject, this, anim);
    }

    public void Analysis(List<AnimationCMD> cmds)
    {
        //AnyTime  die

        //General
        switch (curState)
        {
            case AnimationState.Idle:
                idle.HandleInput(ref curState, ref curAnimPlay, cmds);
                break;
            case AnimationState.TurnOnAim:
                break;
            case AnimationState.TurnOffAim:
                break;
            case AnimationState.IdleMove:
                idleMove.HandleInput(ref curState, ref curAnimPlay, cmds);
                break;
            case AnimationState.Aim:
                aim.HandleInput(ref curState, ref curAnimPlay, cmds);
                break;
            case AnimationState.IdleReload:
                idleReload.HandleInput(ref curState, ref curAnimPlay, cmds);
                break;
            case AnimationState.AimMove:
                aimMove.HandleInput(ref curState, ref curAnimPlay, cmds);
                break;
        }
    }

    //状态中转
    public void SetCurAnim(BaseAnimationPlay anim, AnimationCMD cmd)
    {
        List<AnimationCMD> cmds = new List<AnimationCMD>();
        cmds.Add(cmd);
        curAnimPlay = anim;
        anim.HandleInput(ref curState, ref curAnimPlay, cmds);
    }

    private void FixedUpdate()
    {
        if (curAnimPlay != null)
        {
            curAnimPlay.OnUpdate();
        }
    }
}
