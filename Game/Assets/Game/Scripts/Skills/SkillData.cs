using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Ball", menuName = "Chambers of Elrankat/Skills/Void Skill")]
public class SkillData : ScriptableObject
{
    //General info  
    public string skill_name;
    public Sprite skill_icon;
    public StatVariable weapon_dmg;
    public GameObject skill_display;
    public float skill_dmg_mult;
    public float cast_time_mult;
    public float cost;              //Resource cost of the skill
    public float cooldown;          //Cooldown of the skill


    public delegate void SkillCast(CastInfo cast_info);
    SkillCast skill_cast_del = null;

    private void OnEnable()
    {
        skill_cast_del = SkillCastBehaviour;
    }

    public void CastSkill(CastInfo cast_info)
    {
        if (skill_cast_del != null)
            skill_cast_del(cast_info);
    }


    
    //Skill cast basic method
    public virtual void SkillCastBehaviour(CastInfo cast_info)
    {
        GameObject display = Instantiate(skill_display, cast_info.origin_pos, Quaternion.identity);
        SkillInstance instance =  display.AddComponent<SkillInstance>();
        instance.InitInstance(SkillBehaviour, cast_info);
    }

    public virtual void SkillBehaviour(ref CastInfo cast_info, GameObject display)
    {
        //TEST
        display.transform.Translate(cast_info.dir * 0.2f, Space.World);

        cast_info.curr_dist += cast_info.dir.magnitude * 0.1f;
        //TEST

        Debug.Log("Default skill launched");
    }

    //Cast and behviour modifiers methods
    public void AddCastBehaviour( SkillCast new_cast)
    {
        skill_cast_del += new_cast;

    }

    public void RemoveCastBehaviour( SkillCast old_cast)
    {
        skill_cast_del -= old_cast;
    }
}
