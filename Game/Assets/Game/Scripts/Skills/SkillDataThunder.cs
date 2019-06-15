using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Thunder", menuName = "Chambers of Elrankat/Skills/Thunder")]
public class SkillDataThunder : SkillData
{
    public float range;
    public float range_mult;
    public float effect_area;
    public float effect_area_mult;

    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        if (Vector3.Distance(cast_info.end_pos, cast_info.origin_pos) > range)
        {
            cast_info.end_pos = cast_info.origin_pos + (cast_info.dir * range);
        }

        GameObject display = Instantiate(skill_display, cast_info.end_pos, Quaternion.identity);
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.transform.localScale = new Vector3(effect_area * effect_area_mult, effect_area * effect_area_mult, effect_area * effect_area_mult);
        instance.InitInstance(SkillBehaviour, cast_info);

        //test
        Collider[] hit_colliders = Physics.OverlapSphere(cast_info.end_pos, effect_area * effect_area_mult);
        Debug.Log("doing Explosion damage");
        for (int i = 0; i < hit_colliders.Length; i++)
        {
            if (hit_colliders[i].gameObject.tag == "Enemy")
                hit_colliders[i].GetComponent<EnemySimple>().Hurt(weapon_dmg.Buffed_value * skill_dmg_mult);

        }

        Destroy(display, 5);
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {
        //Instant cast/damage spell void behaviour to prevent warnings
    }

    public override string GetDescription()
    {
        string ret_des = "";

        ret_des += "Summon a thunderstrike at the target location, dealing (";
        ret_des += weapon_dmg.Buffed_value * skill_dmg_mult;
        ret_des += ") ";
        ret_des += skill_dmg_mult * 100;
        ret_des += "% weapon damge to enemies inside the explosion radius";

        return ret_des;
    }
}
