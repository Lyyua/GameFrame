public enum AnimationState
{
    Dead,

    Idle,
    IdleMove,
    IdleReload,
    TurnOnAim,  //过程中不支持指令响应
    TurnOffAim, //过程中不支持指令响应
    Aim,
    AimMove,
    Squat,
    SquatToIdle,
    IdleToSquat,
    SquatMove,
    SquatReload,
}


