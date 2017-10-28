using System;
using System.Collections.Generic;

public class AnimationFactory
{
    private static Dictionary<string, BaseAnimationPlay> animations = new Dictionary<string, BaseAnimationPlay>();

    public static T GetAnimation<T>() where T : BaseAnimationPlay, new()
    {
        Type t = typeof(T);
        string tname = t.ToString();
        if (!animations.ContainsKey(tname))
        {
            T anim = new T();
            animations.Add(tname, anim);
            return anim;
        }
        else
        {
            return (T)animations[tname];
        }
    }
}

