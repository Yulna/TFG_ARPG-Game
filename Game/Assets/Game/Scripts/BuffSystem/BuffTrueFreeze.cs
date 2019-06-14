using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTrueFreeze : Buff
{
    public BuffTrueFreeze() : base(BuffType.BUFF_SKILL_MOD, 0, null)
    {
        skill_modified = (SkillDataIceArmor)CharacterController.instance.skill_controller.GetSkillByName("Ice Armor");
    }

    //Must be Ice Armor
    public SkillDataIceArmor skill_modified;



    public override void EnableBuff()
    {
        skill_modified.effect_area_mult *= 2;
        skill_modified.slow_magnitude += 0.3f;
    }

    public override void DisableBuff()
    {
        skill_modified.effect_area_mult *= 0.5f;
        skill_modified.slow_magnitude -= 0.3f;
    }

    public override string GetBuffDescription()
    {
        return "Increase Ice armor radius and now freezes enemies in place instead of slowing them";
    }

}
