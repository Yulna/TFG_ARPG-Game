using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ice Armor", menuName = "Chambers of Elrankat/Skills/Ice Armor")]
public class SkillDataIceArmor : SkillData
{
    public float duration;
    public float duration_mult;
    public float effect_area;
    public float effect_area_mult;
    public float slow_magnitude;
    public float slow_duration;

    public StatVariable armor;
    public float armor_add_augment;

    public float curr_duration;
    public bool is_active;

    private void OnEnable()
    {
        skill_cast_del = SkillCastBehaviour;
        skill_instance_del = SkillBehaviour;
        cd_timer = 0;
        is_active = false;
    }

    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        if (!is_active)
        {
            GameObject display = Instantiate(skill_display, cast_info.origin_pos, Quaternion.identity);
            SkillInstance instance = display.AddComponent<SkillInstance>();
            instance.transform.localScale = new Vector3(effect_area * effect_area_mult, effect_area * effect_area_mult, effect_area * effect_area_mult);
            instance.InitInstance(SkillBehaviour, cast_info);
            armor.Sum_value += armor_add_augment;
        }
        //Don't use instance duration since we want to ONLY have one of this skill buff active at a time
        curr_duration = 0;
        is_active = true;
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {
        curr_duration += Time.deltaTime;
        instance.transform.position = CharacterController.instance.GetPlayerTransform().position;

        Collider[] hit_colliders = Physics.OverlapSphere(instance.transform.position, effect_area * effect_area_mult);
        for (int i = 0; i < hit_colliders.Length; i++)
        {
            if (hit_colliders[i].gameObject.tag == "Enemy")
            {
                hit_colliders[i].GetComponent<EnemySimple>().Hurt((weapon_dmg.Buffed_value * skill_dmg_mult) * Time.deltaTime);
                hit_colliders[i].GetComponent<EnemySimple>().ApplySlow(slow_magnitude, slow_duration);
            }
        }

        if (curr_duration >= (duration * duration_mult))
        {
            armor.Sum_value -= armor_add_augment;
            Destroy(instance);
            is_active = false;
        }
    }

    public override string GetDescription()
    {
        string ret_des = "";

        ret_des += "Surround yourself with ice shards and frozen mist increasing your base armor by ";
        ret_des += armor_add_augment + " units and dealing (";
        ret_des += weapon_dmg.Buffed_value * skill_dmg_mult;
        ret_des += ") ";
        ret_des += skill_dmg_mult * 100;
        ret_des += "% weapon damge per second to nearby enemies and slowing them by " + slow_magnitude * 100 + "%";

        return ret_des;
    }
}
