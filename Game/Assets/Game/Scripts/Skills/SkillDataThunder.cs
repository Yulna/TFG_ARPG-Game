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
        Destroy(display, display.GetComponent<ParticleSystem>().main.duration);
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {
        Collider[] hit_colliders = Physics.OverlapSphere(cast_info.end_pos, effect_area * effect_area_mult);

     /*   int i = 0;
        while (i < hit_colliders.Length)
        {
            if (hit_colliders[i].gameObject.tag == "Enemy")
                hit_colliders[i].GetComponent<EnemySimple>().Hurt(5);
            i++;
        }
*/

    }
}
