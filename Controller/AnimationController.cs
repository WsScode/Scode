using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController {


    public void PLayAnimBySelfSetting(Animator m_animator) {

    }


    public static float GetAnimationLenth(Animator anim,string clip)
    {
        if (anim == null || clip == null || anim.runtimeAnimatorController == null) return 0;
        RuntimeAnimatorController rac = anim.runtimeAnimatorController;
        AnimationClip[] acClips =  rac.animationClips;
        AnimationClip acClip;
        if (acClips.Length == 0 || acClips == null) return 0;
        for (int i = 0; i < acClips.Length; i++)
        {
            acClip = acClips[i];
            if (acClip != null && acClip.name == clip)
            {
                return acClip.length;
            }
        }
        return 0;
    }


    ///// <summary>
    ///// 获得当前正在播放的动画名称
    ///// </summary>
    ///// <returns></returns>
    //public static string GetCurrentPlayAnimation()
    //{

    //}


}
