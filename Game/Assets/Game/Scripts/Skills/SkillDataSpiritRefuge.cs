﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spirit Refuge", menuName = "Chambers of Elrankat/Skills/Spirit Refuge")]
public class SkillDataSpiritRefuge : SkillData
{
    public StatVariable health_regen;
    public float heal_mult;
    public float range;
    public float duration;
    public float effect_area;
    public float effect_area_mult;

    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        if(Vector3.Distance(cast_info.end_pos, cast_info.origin_pos) > range)
        {
            cast_info.end_pos = cast_info.origin_pos + (cast_info.dir * range);   
        }     
        
        GameObject display = Instantiate(skill_display, cast_info.end_pos + Vector3.down, Quaternion.identity);
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.transform.localScale = new Vector3(effect_area * effect_area_mult, effect_area * effect_area_mult, effect_area * effect_area_mult);
        instance.InitInstance(skill_instance_del, cast_info);
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {        
        Collider[] hit_colliders = Physics.OverlapSphere(cast_info.end_pos, effect_area*effect_area_mult);

        for (int i = 0; i < hit_colliders.Length; i++)
        {
            if (hit_colliders[i].gameObject.tag == "Player")
            {
                CharacterController.instance.HealPlayer(health_regen.Buffed_value * heal_mult * Time.deltaTime);
            }
        }

        cast_info.curr_duration += Time.deltaTime;
        if (cast_info.curr_duration > duration)
            Destroy(instance);
    }

    public override string GetDescription()
    {
        string ret_des = "";

        ret_des += "Bless a nearby area, while standing inside the area heal yourself ";
        ret_des += health_regen.Buffed_value * heal_mult + " life per second";

        ret_des += "\nHeal value is equal to ";
        ret_des += heal_mult * 100;
        ret_des += "% of your natural health regeneration";

        return ret_des;
    }
}
