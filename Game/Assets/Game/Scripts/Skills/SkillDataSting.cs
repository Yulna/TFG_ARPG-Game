using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sting", menuName = "Chambers of Elrankat/Skills/Sting")]
public class SkillDataSting : SkillData
{

    public float lenght_range;
    public float width_range;

    public float display_speed;

    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        cast_info.origin_pos += Vector3.up * 1.5f;
        cast_info.dir = cast_info.end_pos - cast_info.origin_pos;
        cast_info.dir.Normalize();
        cast_info.end_pos = cast_info.origin_pos + (cast_info.dir * lenght_range);

        GameObject display = Instantiate(skill_display, cast_info.origin_pos, Quaternion.LookRotation(cast_info.dir, Vector3.up));
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.InitInstance(SkillBehaviour, cast_info);

        Collider[] hit_colliders = Physics.OverlapCapsule(cast_info.origin_pos, cast_info.end_pos, width_range);
        Collider closest_enemy = null;
        float closest_dist = Mathf.Infinity;
        for (int i = 0; i < hit_colliders.Length; i++)
        {
            if (hit_colliders[i].gameObject.tag == "Enemy")
            {
                float hit_dist = Vector3.Distance(hit_colliders[i].transform.position, instance.transform.position);
                if (hit_dist < closest_dist)
                {
                    closest_enemy = hit_colliders[i];
                    closest_dist = hit_dist;
                }
            }
        }
        if (closest_dist < Mathf.Infinity && closest_enemy != null)
            closest_enemy.GetComponent<EnemySimple>().Hurt(weapon_dmg.Buffed_value * skill_dmg_mult);

        //Destroy(display, display.GetComponent<ParticleSystem>().main.duration);
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {
        //Instant cast/damage spell void behaviour to prevent warnings

        instance.transform.position += cast_info.dir * Time.deltaTime * display_speed;
        cast_info.curr_dist += Time.deltaTime * display_speed;

        if (cast_info.curr_dist >= lenght_range)
            Destroy(instance);
    }


    public override string GetDescription()
    {
        string ret_des = "";

        ret_des += "Pierce enemies in front of you, dealing (";
        ret_des += weapon_dmg.Buffed_value * skill_dmg_mult;
        ret_des += ") ";
        ret_des += skill_dmg_mult * 100;
        ret_des += "% weapon damge to the first enemy hit";

        return ret_des;
    }
}
