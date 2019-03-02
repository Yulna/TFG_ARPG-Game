using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    //General info  
    public string skill_name;
    public int magnitude;           //Damage or healing of the skill
    public int cost;                //Resource cost of the skill
    public float distance;          //Max range of the skill
    public float speed;             //Speed of the projectile
    public float duration;          //Time skill is active (mainly for buffs)
    public float cooldown;          //Cooldown of the skill
    public GameObject skill_display;//Go with the art for the skill <-- Same GO

    public LayerMask obj_mask;

    public bool SkillUpdate(GameObject display,ref CastInfo cast_info)
    {
        if (cast_info.curr_dist <= distance)
        {
            SkillBehaviour(display, ref cast_info);
            return true;
        }
        else
        {
            OnSkillEnd();
            return false;
        }
        Debug.LogWarning("Some Error ocurred during skill Update");
        return false;
    }

    //Return true if the skill can be casted, false otherwise (Still in CD, no mana...)
    public virtual void SkillBehaviour(GameObject display, ref CastInfo cast_info)
    {
        Debug.LogWarning("No SkillBehaviour() override detected");
    }

    public virtual void OnSkillEnd()
    {
        Debug.LogWarning("No OnSkillEnd() override detected");
    }
}