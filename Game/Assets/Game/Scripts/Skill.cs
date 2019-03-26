using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CastInfo
{
    //Casting info --> Need to be set each time it's casted
    public Vector3 origin_pos;
    public Vector3 end_pos;
    public Vector3 dir;
    public float curr_dist;
}

public class Skill : ScriptableObject
{
    //General info  
    public string skill_name;
    public Sprite skill_icon;
    public int cost;                //Resource cost of the skill
    public float cooldown;          //Cooldown of the skill

    public CastInfo InitCastInfo(Vector3 origin, Vector3 dest)
    {
        CastInfo ret = new CastInfo();
        ret.origin_pos = origin;
        ret.end_pos = dest;
        ret.dir = ret.end_pos - ret.origin_pos;
        ret.dir.Normalize();
        return ret;
    }

    public virtual void CastSkill(Vector3 org, Vector3 dest)
    {
        Debug.LogWarning("No CastSkill() override");
        PlayerController.instance.SpendResource(cost);
        InitCastInfo(org, dest);
    }

    //Return true if the skill can be casted, false otherwise (Still in CD, no mana...)
    public virtual void SkillBehaviour(ref CastInfo cast_inf, GameObject display)
    {
        Debug.LogError("No SkillBehaviour() override detected");
    }
}