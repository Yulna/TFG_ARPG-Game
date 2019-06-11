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

    public float curr_duration;
    public bool is_active;

    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        if (!is_active)
        {
            GameObject display = Instantiate(skill_display, cast_info.origin_pos, Quaternion.identity);
            SkillInstance instance = display.AddComponent<SkillInstance>();
            instance.transform.localScale = new Vector3(effect_area * effect_area_mult, effect_area * effect_area_mult, effect_area * effect_area_mult);
            instance.InitInstance(SkillBehaviour, cast_info);
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
                hit_colliders[i].GetComponent<EnemySimple>().Hurt(1 * Time.deltaTime);
                hit_colliders[i].GetComponent<EnemySimple>().ApplySlow(slow_magnitude, slow_duration);
            }
        }

        if (curr_duration >= (duration * duration_mult))
        {
            Destroy(instance);
            is_active = false;
        }
    }

}
