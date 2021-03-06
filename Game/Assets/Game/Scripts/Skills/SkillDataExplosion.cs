﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arcane Explosion", menuName = "Chambers of Elrankat/Skills/Arcane Explosion")]
public class SkillDataExplosion : SkillData
{
    public float projectile_range;
    public float projectile_range_mult;
    public float projectile_area;
    public float effect_area;
    public float effect_area_mult;

    public float projectile_speed;

    public GameObject explosion_display;

    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        cast_info.origin_pos += Vector3.up * 1.5f;
        cast_info.dir = cast_info.end_pos - cast_info.origin_pos;
        cast_info.dir.Normalize();

        GameObject display = Instantiate(skill_display, cast_info.origin_pos, Quaternion.identity);
        SkillInstance instance = display.AddComponent<SkillInstance>();
        // instance.transform.localScale = new Vector3(effect_area * effect_area_mult, effect_area * effect_area_mult, effect_area * effect_area_mult);
        instance.InitInstance(SkillBehaviour, cast_info);
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {
        instance.transform.Translate(cast_info.dir * projectile_speed, Space.World);

        bool enemy_colision = false;
        Collider[] proj_coll = Physics.OverlapSphere(instance.transform.position, projectile_area);    
        for (int i = 0; i < proj_coll.Length; i++)
        {
            if (proj_coll[i].gameObject.tag == "Enemy")
            {
                enemy_colision = true;
                break;
            }           
        }

        if (Vector3.Distance(cast_info.origin_pos, instance.transform.position) >= projectile_range || enemy_colision)
        {
            //Explode
            Collider[] hit_colliders = Physics.OverlapSphere(instance.transform.position, effect_area * effect_area_mult);
            for (int i = 0; i < hit_colliders.Length; i++)
            {
                if (hit_colliders[i].gameObject.tag == "Enemy")
                {
                    hit_colliders[i].GetComponent<EnemySimple>().Hurt(weapon_dmg.Buffed_value * skill_dmg_mult);
                }

            }
            //Explosion particle spawn
            GameObject instance_explosion = Instantiate(explosion_display, instance.transform.position, Quaternion.identity);
            instance_explosion.transform.localScale = new Vector3(effect_area * effect_area_mult, effect_area * effect_area_mult, effect_area * effect_area_mult);
            Destroy(instance_explosion, 5);
            Destroy(instance);
        }
    }


    public override string GetDescription()
    {
        string ret_des = "";

        ret_des += "Throw a projectile that explodes after traveling a certain distance or colliding with an enemy, dealing (";
        ret_des += weapon_dmg.Buffed_value * skill_dmg_mult;
        ret_des += ") ";    
        ret_des += skill_dmg_mult * 100;
        ret_des += "% weapon damge to enemies inside the explosion radius";

        return ret_des;
    }
}
