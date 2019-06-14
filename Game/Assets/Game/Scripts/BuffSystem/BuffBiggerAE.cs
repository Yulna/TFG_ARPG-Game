using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBiggerAE : Buff
{
    public BuffBiggerAE() : base(BuffType.BUFF_SKILL_MOD, 0, null)
    {
        skill_modified = (SkillDataExplosion)CharacterController.instance.skill_controller.GetSkillByName("Arcane Explosion");
    }

    //Must be Explosion
    public SkillDataExplosion skill_modified;

   
    public override void EnableBuff()
    {
        skill_modified.effect_area_mult *= 2;
        skill_modified.skill_dmg_mult *= 1.75f;
    }

    public override void DisableBuff()
    {
        skill_modified.effect_area_mult *= 0.5f;
        skill_modified.skill_dmg_mult /= 1.75f;
    }

    public override string GetBuffDescription()
    {
        return "Exposion radius from arcane explosion is doubled and damage increased 75%";
    }
}
