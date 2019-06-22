using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Base Attack", menuName = "Chambers of Elrankat/Skills/Base Attack")]
public class SkillDataBaseAttack : SkillData
{
    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        cast_info.origin_pos += Vector3.up * 1.5f;
        cast_info.dir = cast_info.end_pos - cast_info.origin_pos;
        cast_info.dir.Normalize();
        GameObject display = Instantiate(skill_display, cast_info.origin_pos + cast_info.dir, Quaternion.identity);
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.InitInstance(SkillBehaviour, cast_info);

        //test
        Collider[] hit_colliders = Physics.OverlapSphere(instance.transform.position, 2);
        for (int i = 0; i < hit_colliders.Length; i++)
        {
            if (hit_colliders[i].gameObject.tag == "Enemy")
            {
                hit_colliders[i].GetComponent<EnemySimple>().Hurt(weapon_dmg.Buffed_value * skill_dmg_mult);
            }
        }
        Destroy(display, 2.5f);
    }

    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {
        //Instant cast/damage spell void behaviour to prevent warnings
    }

    public override string GetDescription()
    {
        string ret_des = "";

        ret_des += "Deals (";
        ret_des += weapon_dmg.Buffed_value * skill_dmg_mult;
        ret_des += ") ";
        ret_des += skill_dmg_mult * 100;
        ret_des += "% weapon damge to enemies in front of you";

        return ret_des;
    }
}
