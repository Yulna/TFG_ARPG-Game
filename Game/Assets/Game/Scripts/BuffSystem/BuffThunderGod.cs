using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffThunderGod : Buff
{
    public BuffThunderGod() : base(BuffType.BUFF_SKILL_MOD, 0, null)
    {
        skill_modified = (SkillDataThunder)CharacterController.instance.skill_controller.GetSkillByName("Thunder");
    }

    //Must be Thunder
    public SkillDataThunder skill_modified;

    public override void EnableBuff()
    {
        skill_modified.cost += 150;
        skill_modified.effect_area_mult *= 2;
        skill_modified.AddCastBehaviour(SkillCastBehaviourMod);
    }

    public override void DisableBuff()
    {
        skill_modified.cost -= 150;
        skill_modified.effect_area_mult *= 0.5f;
        skill_modified.RemoveCastBehaviour(SkillCastBehaviourMod);
    }

    public override string GetBuffDescription()
    {
        return "Increase Thunder cost to 150 mana but remove cooldown and increases the damage area";
    }


    public void SkillCastBehaviourMod(CastInfo cast_info)
    {
        skill_modified.cd_timer = 0;
    }
}
