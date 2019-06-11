using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Swing", menuName = "Chambers of Elrankat/Skills/Swing")]
public class SkillDataSwing : SkillData
{
    public float effect_area;
    public float effect_area_mult;

    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        GameObject display = Instantiate(skill_display, cast_info.origin_pos, Quaternion.identity);
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.transform.localScale = new Vector3(effect_area * effect_area_mult, effect_area * effect_area_mult, effect_area * effect_area_mult);
        instance.InitInstance(SkillBehaviour, cast_info);

        //test
        Collider[] hit_colliders = Physics.OverlapSphere(cast_info.end_pos, effect_area * effect_area_mult);
        Debug.Log("doing Explosion damage");
        for (int i = 0; i < hit_colliders.Length; i++)
        {
            if (hit_colliders[i].gameObject.tag == "Enemy")
                hit_colliders[i].GetComponent<EnemySimple>().Hurt(5);

        }

        Destroy(display, 3);
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {
        //Instant cast/damage spell void behaviour to prevent warnings
    }

}
