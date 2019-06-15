using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffFourDragonFire : Buff
{
    public BuffFourDragonFire() : base(BuffType.BUFF_SKILL_MOD, 0, null)
    {
        skill_modified = (SkillDataDragonBreath)CharacterController.instance.skill_controller.GetSkillByName("Dragon Breath");
    }

    //Must be Ice Armor
    public SkillDataDragonBreath skill_modified;


    public override void EnableBuff()
    {
        skill_modified.AddCastBehaviour(SkillCastBehaviourModPositive);
        skill_modified.AddCastBehaviour(SkillCastBehaviourModPositiveDouble);
        skill_modified.AddCastBehaviour(SkillCastBehaviourModNegative);
    }

    public override void DisableBuff()
    {
        skill_modified.RemoveCastBehaviour(SkillCastBehaviourModPositive);
        skill_modified.RemoveCastBehaviour(SkillCastBehaviourModPositiveDouble);
        skill_modified.RemoveCastBehaviour(SkillCastBehaviourModNegative);
    }

    public override string GetBuffDescription()
    {
        return "Dragon breath now fires in all direction damaging all enemies in a circle around you";
    }

    public void SkillCastBehaviourModPositive(CastInfo cast_info)
    {
        cast_info.origin_pos += Vector3.up * 1.5f;
        cast_info.dir = cast_info.end_pos - cast_info.origin_pos;
        cast_info.dir.Normalize();

        GameObject display =  GameObject.Instantiate(skill_modified.skill_display, cast_info.origin_pos, Quaternion.LookRotation(Quaternion.AngleAxis(90, Vector3.up) * cast_info.dir, new Vector3(0, 1, 0)));
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.transform.localScale = new Vector3(skill_modified.range * skill_modified.range_mult, skill_modified.range * skill_modified.range_mult, skill_modified.range * skill_modified.range_mult);
        instance.InitInstance(skill_modified.SkillBehaviour, cast_info);
        GameObject.Destroy(display, 1.5f);
    }

    public void SkillCastBehaviourModNegative(CastInfo cast_info)
    {
        cast_info.origin_pos += Vector3.up * 1.5f;
        cast_info.dir = cast_info.end_pos - cast_info.origin_pos;
        cast_info.dir.Normalize();

        GameObject display = GameObject.Instantiate(skill_modified.skill_display, cast_info.origin_pos, Quaternion.LookRotation(Quaternion.AngleAxis(-90, Vector3.up) * cast_info.dir, new Vector3(0, 1, 0)));
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.transform.localScale = new Vector3(skill_modified.range * skill_modified.range_mult, skill_modified.range * skill_modified.range_mult, skill_modified.range * skill_modified.range_mult);
        instance.InitInstance(skill_modified.SkillBehaviour, cast_info);
        GameObject.Destroy(display, 1.5f);
    }

    public void SkillCastBehaviourModPositiveDouble(CastInfo cast_info)
    {
        cast_info.origin_pos += Vector3.up * 1.5f;
        cast_info.dir = cast_info.end_pos - cast_info.origin_pos;
        cast_info.dir.Normalize();

        GameObject display = GameObject.Instantiate(skill_modified.skill_display, cast_info.origin_pos, Quaternion.LookRotation(Quaternion.AngleAxis(180, Vector3.up) * cast_info.dir, new Vector3(0, 1, 0)));
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.transform.localScale = new Vector3(skill_modified.range * skill_modified.range_mult, skill_modified.range * skill_modified.range_mult, skill_modified.range * skill_modified.range_mult);
        instance.InitInstance(skill_modified.SkillBehaviour, cast_info);
        GameObject.Destroy(display, 1.5f);
    }
}
