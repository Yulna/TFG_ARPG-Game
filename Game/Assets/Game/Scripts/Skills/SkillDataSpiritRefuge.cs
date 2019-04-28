using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spirit Refuge", menuName = "Chambers of Elrankat/Skills/Spirit Refuge")]
public class SkillDataSpiritRefuge : SkillData
{
    public float range;
    public float duration;
    public float effect_area;
    public float effect_area_mult;

    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        if(Vector3.Distance(cast_info.end_pos, cast_info.origin_pos) > range)
        {
            cast_info.end_pos = cast_info.origin_pos + (cast_info.dir * range);   
        }     
        
        GameObject display = Instantiate(skill_display, cast_info.end_pos, Quaternion.identity);
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.transform.localScale = new Vector3(effect_area * effect_area_mult, effect_area * effect_area_mult, effect_area * effect_area_mult);
        instance.InitInstance(SkillBehaviour, cast_info);
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {        
        Collider[] hit_colliders = Physics.OverlapSphere(cast_info.end_pos, effect_area*effect_area_mult);

        cast_info.curr_duration += Time.deltaTime;
        if (cast_info.curr_duration > duration)
            Destroy(instance);
    }
}
