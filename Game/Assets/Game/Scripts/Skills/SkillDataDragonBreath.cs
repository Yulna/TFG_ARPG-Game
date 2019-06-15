using System.Collections;
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
        cast_info.dir = cast_info.end_pos - cast_info.origin_pos;
        cast_info.dir.Normalize();
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
                    hit_colliders[i].GetComponent<EnemySimple>().Hurt(weapon_dmg.Buffed_value * skill_dmg_mult);
            }
            i++;
        }
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {
        //Override to aviod default function
    }

    public override string GetDescription()
    {
        string ret_des = "";

        ret_des += "Throw fire in a cone infront of you, dealing (";
        ret_des += weapon_dmg.Buffed_value * skill_dmg_mult;
        ret_des += ") ";
        ret_des += skill_dmg_mult * 100;
        ret_des += "% weapon damge to enemies touched by the fire";

        return ret_des;
    }
}
