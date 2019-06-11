﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dragon Breath", menuName = "Chambers of Elrankat/Skills/Dragon Breath")]
public class SkillDataDragonBreath : SkillData
{

    public float range;
    public float range_mult;
    public float angle;
    public float angle_mult;

    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        cast_info.origin_pos += Vector3.up * 1.5f;
        GameObject display = Instantiate(skill_display, cast_info.origin_pos, Quaternion.LookRotation(cast_info.dir,new Vector3(0,1,0)));
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.transform.localScale = new Vector3(range * range_mult, range * range_mult, range * range_mult);
        instance.InitInstance(SkillBehaviour, cast_info);
        Destroy(display, 1.5f);


        //Damage calc
        Collider[] hit_colliders = Physics.OverlapSphere(cast_info.origin_pos, range * range_mult);
        Debug.Log("Dragon breath breathing");
        int i = 0;
        while (i < hit_colliders.Length)
        {
            if (hit_colliders[i].gameObject.tag == "Enemy")
            {

                Vector3 enemy_dir = hit_colliders[i].gameObject.transform.position - cast_info.origin_pos;
                float enemy_angle = Vector3.Angle(enemy_dir, cast_info.dir);
                if (enemy_angle < angle * angle_mult * 0.5f)
                    hit_colliders[i].GetComponent<EnemySimple>().Hurt(5);
            }
            i++;
        }
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {
        //Override to aviod default function
    }

}
