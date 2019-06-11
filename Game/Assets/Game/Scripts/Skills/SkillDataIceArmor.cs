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

    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        GameObject display = Instantiate(skill_display, cast_info.origin_pos, Quaternion.identity);
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.transform.localScale = new Vector3(effect_area * effect_area_mult, effect_area * effect_area_mult, effect_area * effect_area_mult);
        instance.InitInstance(SkillBehaviour, cast_info);
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {
        cast_info.curr_duration += Time.deltaTime;

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

        if (cast_info.curr_duration >= (duration * duration_mult))
            Destroy(instance);
    }

}
