using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield bash", menuName = "Chambers of Elrankat/Skills/Shield Bash")]
public class SkillDataShieldBash : SkillData
{
    public float effect_area;
    public float effect_area_mult;
    public float push_force;
    public float stun_duration;

    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        GameObject display = Instantiate(skill_display, cast_info.origin_pos + cast_info.dir, Quaternion.identity);
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.transform.localScale = new Vector3(effect_area * effect_area_mult, effect_area * effect_area_mult, effect_area * effect_area_mult);
        instance.InitInstance(SkillBehaviour, cast_info);

        //test
        Collider[] hit_colliders = Physics.OverlapSphere(instance.transform.position, effect_area * effect_area_mult);
        for (int i = 0; i < hit_colliders.Length; i++)
        {
            if (hit_colliders[i].gameObject.tag == "Enemy")
            {
                hit_colliders[i].GetComponent<EnemySimple>().Hurt(5);
                hit_colliders[i].GetComponent<EnemySimple>().PushTowards(cast_info.dir, push_force);
                hit_colliders[i].GetComponent<EnemySimple>().Stun(stun_duration);
            }

        }

        Destroy(display, 2.5f);
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {
        //Instant cast/damage spell void behaviour to prevent warnings
    }

}
