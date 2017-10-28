using System.Collections.Generic;
using UnityEngine;

public class AnimationSystem
{
    private static AnimationSystem _instance = null;
    public static AnimationSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AnimationSystem();
            }
            return _instance;
        }
    }
    public AnimationState curState;
    public BaseAnimationPlay curAnim;
    public AnimationNodesCycle animCycle;
    public AnimAssetInfo animInfo;

    void SignalInput(List<AnimationCMD> cmds)
    {
        AnimationCMD filterCmd = curAnim.CMDFilter(cmds);
        curAnim.HandleInput(filterCmd);
    }
}

