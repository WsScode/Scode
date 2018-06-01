using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 加载图集图片
/// </summary>
public static class LoadSprite  {

    public static Sprite LoadSpriteBySkill(SkillInfo skill)
    {
        if (skill == null) return null;
        //加载图集的图片
        Object[] os = Resources.LoadAll("Sprites/Skill/Skill_GongShou");
        for (int i = 0; i < os.Length; i++)
        {
            if (os[i].GetType() == typeof(Sprite) && os[i].name == skill.M_Icon)
            {
               return (Sprite)os[i];
            }
        }
        return null;
    }

    public static Sprite LoadSpriteByName(string path,string spriteName)
    {
        Object[] os = Resources.LoadAll("Sprites/" + path);
        for (int i = 0; i < os.Length; i++)
        {
            if (os[i].GetType() == typeof(Sprite) && os[i].name == spriteName)
            {
                return (Sprite)os[i];
            }
        }
        return null;
    }

    public static Texture2D LoadTexture2DByName(string path,string name)
    {
        Object[] os = Resources.LoadAll("Sprites/" + path);
        for (int i = 0; i < os.Length; i++)
        {
            if (os[i].GetType() == typeof(Texture2D) && os[i].name == name)
            {
                return (Texture2D)os[i];
            }
        }
        return null;

    }

}
