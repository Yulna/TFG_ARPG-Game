using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    UNDEFINED,
    SKILL_SWORDMANSHIP,
    SKILL_MAGICAL
}

[CreateAssetMenu(fileName = "Void Skill", menuName = "Chambers of Elrankat/Skills/Void Skill")]
public class SkillData : ScriptableObject
{
    //General info  
    public string skill_name;
    public string skill_description;
    public Sprite skill_icon;
    public SkillType skill_type;
    public StatVariable weapon_dmg;
    public GameObject skill_display;
    public float skill_dmg_mult;
    public float cast_time_mult;
    public float cost;              //Resource cost of the skill
    public float cooldown;          //Cooldown of the skill
    public float cd_timer;


    public delegate void SkillCast(CastInfo cast_info);
    public SkillCast skill_cast_del = null;
  
    public SkillInstance.SkillBehaviour skill_instance_del;


    private void OnEnable()
    {
        skill_cast_del = SkillCastBehaviour;
        skill_instance_del = SkillBehaviour;
        cd_timer = 0;
    }

    public void CastSkill(CastInfo cast_info)
    {
        cd_timer = cooldown;
        if (skill_cast_del != null)
            skill_cast_del(cast_info);
    }



    //Skill cast basic method
    public virtual void SkillCastBehaviour(CastInfo cast_info)
    {
        GameObject display = Instantiate(skill_display, cast_info.origin_pos, Quaternion.identity);
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.InitInstance(SkillBehaviour, cast_info);
    }

    public virtual void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {
        //TEST
        instance.transform.Translate(cast_info.dir * 0.2f, Space.World);
        cast_info.curr_dist += cast_info.dir.magnitude * 0.1f;
        //TEST        
        Debug.Log("Default skill launched");
    }


    //Cast and behviour modifiers methods
    public void AddCastBehaviour(SkillCast new_cast)
    {
        skill_cast_del += new_cast;
    }

    public void RemoveCastBehaviour(SkillCast old_cast)
    {
        skill_cast_del -= old_cast;
    }

    public void AddInstanceBehaviour(SkillInstance.SkillBehaviour new_cast)
    {
        skill_instance_del += new_cast;
    }

    public void RemoveInstanceBehaviour(SkillInstance.SkillBehaviour old_cast)
    {
        skill_instance_del -= old_cast;
    }

    public bool IsOnCooldown()
    {
        if (cd_timer <= 0)
            return false;
        else
            return true;
    }

    public float GetCDPercentile()
    {
        if (cooldown == 0)
            return 0;
        else
            return cd_timer / cooldown;
    }

    public virtual string GetName()
    {
        return name;
    }

    public virtual string GetCostString()
    {
        if (cost == 0)
            return "No cost";
        return cost.ToString() + " Mana"; 
    }

    public virtual string GetDescription()
    {
        return "Skill description";
    }
}
