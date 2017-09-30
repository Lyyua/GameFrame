using UnityEngine;

public class AnimationSystem
{
    public GameObject curGo;
    public AnimationNodesCycle anim;
    public IdleMoveAnimationPlay idleMove;
    public IdleAnimationPlay idle;
    public TurnOnAimPlay turnOnAim;
    public TurnOffAimPlay turnOffAim;
    public AimAnimationPlay aim;
    public AimMoveAnimationPlay aimMove;
    public IdleReloadAnimationPlay idleReload;
    public IdleToSquatPlay idleToSquat;
    public SquatToIdlePlay squatToIdle;
    public SquatAnimationPlay squat;
    public SquatMoveAnimationPlay squatMove;
    public SquatReloadAnimationPlay squatReload;
    public AimToSquatPlay aimToSquat;
    public DeadAnimationPlay dead;

    private void InitAnimation()
    {
        //idleMove = new IdleMoveAnimationPlay(curGo, this, anim);
        //idle = new IdleAnimationPlay(curGo, this, anim);
        //turnOnAim = new TurnOnAimPlay(curGo, this, anim);
        //turnOffAim = new TurnOffAimPlay(curGo, this, anim);
        //aim = new AimAnimationPlay(curGo, this, anim);
        //aimMove = new AimMoveAnimationPlay(curGo, this, anim);
        //idleReload = new IdleReloadAnimationPlay(curGo, this, anim);
        //idleToSquat = new IdleToSquatPlay(curGo, this, anim);
        //squatToIdle = new SquatToIdlePlay(curGo, this, anim);
        //squat = new SquatAnimationPlay(curGo, this, anim);
        //squatMove = new SquatMoveAnimationPlay(curGo, this, anim);
        //squatReload = new SquatReloadAnimationPlay(curGo, this, anim);
        //aimToSquat = new AimToSquatPlay(curGo, this, anim);
        //dead = new DeadAnimationPlay(curGo, this, anim);
    }

    //void StateSwitch()
    //{
    //    switch (curState)
    //    {
    //        case AnimationState.Idle:
    //            idle.HandleInput(ref curState, ref curAnimPlay, _cmds);
    //            break;
    //        case AnimationState.IdleMove:
    //            idleMove.HandleInput(ref curState, ref curAnimPlay, _cmds);
    //            break;
    //        case AnimationState.Aim:
    //            aim.HandleInput(ref curState, ref curAnimPlay, _cmds);
    //            break;
    //        case AnimationState.IdleReload:
    //            idleReload.HandleInput(ref curState, ref curAnimPlay, _cmds);
    //            break;
    //        case AnimationState.AimMove:
    //            aimMove.HandleInput(ref curState, ref curAnimPlay, _cmds);
    //            break;
    //        case AnimationState.Squat:
    //            squat.HandleInput(ref curState, ref curAnimPlay, _cmds);
    //            break;
    //        case AnimationState.SquatMove:
    //            squatMove.HandleInput(ref curState, ref curAnimPlay, _cmds);
    //            break;
    //        case AnimationState.SquatReload:
    //            squatReload.HandleInput(ref curState, ref curAnimPlay, _cmds);
    //            break;
    //        case AnimationState.SquatToIdle:
    //            squatToIdle.HandleInput(ref curState, ref curAnimPlay, _cmds);
    //            break;
    //    }
    //}
}

