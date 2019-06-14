using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Earthquake", menuName = "Chambers of Elrankat/Skills/Earthquake")]
public class SkillDataEarthquake : SkillData
{
    public float effect_area;
    public float effect_area_mult;
    public float duration;
    public float duration_mult;
    public float stun_duration;
    public float slow_magnitude;
    public float slow_duration;

    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        GameObject display = Instantiate(skill_display, cast_info.origin_pos, Quaternion.identity);
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.transform.localScale = new Vector3(effect_area * effect_area_mult, effect_area * effect_area_mult, effect_area * effect_area_mult);
        instance.InitInstance(SkillBehaviour, cast_info);

        //Initial damage
        Collider[] hit_colliders = Physics.OverlapSphere(cast_info.end_pos, effect_area * effect_area_mult);
        for (int i = 0; i < hit_colliders.Length; i++)
        {
            if (hit_colliders[i].gameObject.tag == "Enemy")
            {
                hit_colliders[i].GetComponent<EnemySimple>().Hurt(weapon_dmg.Buffed_value * skill_dmg_mult);
                hit_colliders[i].GetComponent<EnemySimple>().Stun(stun_duration);
            }

        }
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {
        cast_info.curr_duration += Time.deltaTime;

        Collider[] hit_colliders = Physics.OverlapSphere(instance.transform.position, effect_area * effect_area_mult);

        for (int i = 0; i < hit_colliders.Length; i++)
        {
            if (hit_colliders[i].gameObject.tag == "Enemy")
            {
                hit_colliders[i].GetComponent<EnemySimple>().ApplySlow(slow_magnitude,slow_duration);
                cast_info.hitted_colliders.Add(hit_colliders[i]);
            }
        }

        if (cast_info.curr_duration >= (duration * duration_mult))
            Destroy(instance);
    }

    public override string GetDescription()
    {
        string ret_des = "";

        ret_des += "Shake the ground arround you, damaging enemies for (";
        ret_des += weapon_dmg.Buffed_value * skill_dmg_mult;
        ret_des += ") ";
        ret_des += skill_dmg_mult * 100;
        ret_des += "% weapon damge and stunning them for " + stun_duration + " seconds.";
        ret_des += "\nThe earthquake leaves a unstable zone behind during " + duration * duration_mult +
                    " seconds which slows enemies inside it by " + slow_magnitude * 100 + "%";

        return ret_des;
    }
}
