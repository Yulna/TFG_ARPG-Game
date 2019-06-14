using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wind Rush", menuName = "Chambers of Elrankat/Skills/Wind Rush")]
public class SkillDataWindRush : SkillData
{
    public float projectile_range;
    public float projectile_range_mult;
    public float effect_area;
    public float effect_area_mult;
    public float push_force;

    public float projectile_speed;


    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        GameObject display = Instantiate(skill_display, cast_info.origin_pos, Quaternion.identity);
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.transform.localScale = new Vector3(effect_area * effect_area_mult, effect_area * effect_area_mult, effect_area * effect_area_mult);
        instance.InitInstance(SkillBehaviour, cast_info);
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {
        instance.transform.Translate(cast_info.dir * projectile_speed, Space.World);
        cast_info.curr_dist += cast_info.dir.magnitude * projectile_speed;

        Collider[] hit_colliders = Physics.OverlapSphere(instance.transform.position, effect_area * effect_area_mult);

        for (int i = 0; i < hit_colliders.Length; i++)
        {
            if (hit_colliders[i].gameObject.tag == "Enemy" && !cast_info.hitted_colliders.Contains(hit_colliders[i]))
            {
                hit_colliders[i].GetComponent<EnemySimple>().Hurt(weapon_dmg.Buffed_value * skill_dmg_mult);
                hit_colliders[i].GetComponent<EnemySimple>().PushTowards(cast_info.dir, push_force);
                cast_info.hitted_colliders.Add(hit_colliders[i]);
            }
        }

        if (cast_info.curr_dist >= (projectile_range * projectile_range_mult))
        {
            Destroy(instance);
        }
    }

    public override string GetDescription()
    {
        string ret_des = "";

        ret_des += "Create a tornado that moves towards the cursor, dealing (";
        ret_des += weapon_dmg.Buffed_value * skill_dmg_mult;
        ret_des += ") ";
        ret_des += skill_dmg_mult * 100;
        ret_des += "% weapon damge to enemies in its path and pushing them backwards";

        return ret_des;
    }
}
