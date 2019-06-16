using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTripleTornado : Buff
{
    public BuffTripleTornado() : base(BuffType.BUFF_SKILL_MOD, 0, null)
    {
        Debug.LogWarning("Creating unique TT");
        skill_modified = (SkillDataWindRush)CharacterController.instance.skill_controller.GetSkillByName("Wind Rush");
        Debug.LogWarning("Skill mode for TT aquired");
    }

    //Must be Wind Rush
    public SkillDataWindRush skill_modified;

    public override void EnableBuff()
    {
        skill_modified.AddCastBehaviour(SkillCastBehaviourModPositive);
        skill_modified.AddCastBehaviour(SkillCastBehaviourModNegative);
    }

    public override void DisableBuff()
    {
        skill_modified.RemoveCastBehaviour(SkillCastBehaviourModPositive);
        skill_modified.RemoveCastBehaviour(SkillCastBehaviourModNegative);
    }

    public override string GetBuffDescription()
    {
        return "Wind rush spawns 3 tornados";
    }

    public void SkillCastBehaviourModPositive(CastInfo cast_info)
    {
        cast_info.origin_pos += Vector3.up * 1.5f;
        cast_info.dir = cast_info.end_pos - cast_info.origin_pos;
        cast_info.dir.Normalize();
        cast_info.dir = Quaternion.Euler(0, 25, 0) * cast_info.dir;

        GameObject display = GameObject.Instantiate(skill_modified.skill_display, cast_info.origin_pos, Quaternion.identity);
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.transform.localScale = new Vector3(skill_modified.effect_area * skill_modified.effect_area_mult, skill_modified.effect_area * skill_modified.effect_area_mult, skill_modified.effect_area * skill_modified.effect_area_mult);
        instance.InitInstance(skill_modified.skill_instance_del, cast_info);
    }

    public void SkillCastBehaviourModNegative(CastInfo cast_info)
    {
        cast_info.origin_pos += Vector3.up * 1.5f;
        cast_info.dir = cast_info.end_pos - cast_info.origin_pos;
        cast_info.dir.Normalize();
        cast_info.dir = Quaternion.Euler(0, -25, 0) * cast_info.dir;

        GameObject display = GameObject.Instantiate(skill_modified.skill_display, cast_info.origin_pos, Quaternion.identity);
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.transform.localScale = new Vector3(skill_modified.effect_area * skill_modified.effect_area_mult, skill_modified.effect_area * skill_modified.effect_area_mult, skill_modified.effect_area * skill_modified.effect_area_mult);
        instance.InitInstance(skill_modified.skill_instance_del, cast_info);
    }
}
