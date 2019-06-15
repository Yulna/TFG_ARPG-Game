using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffWindPower : Buff
{
    public BuffWindPower() : base(BuffType.BUFF_SKILL_MOD, 0, null)
    {
        skill_modified = (SkillDataWindFury)CharacterController.instance.skill_controller.GetSkillByName("Wind Fury");
        weapon_dmg = CharacterController.instance.GetStat(StatId.WeaponDmg);
        attack_buff_given = false;
    }

    //Must be Ice Armor
    public SkillDataWindFury skill_modified;
    public StatVariable weapon_dmg;

    bool attack_buff_given = false;

    public override void EnableBuff()
    {
        skill_modified.duration_mult *= 10;
        skill_modified.AddCastBehaviour(SkillCastBehaviourMod);
        skill_modified.AddInstanceBehaviour(SkillInstanceBehaviourMod);
    }

    public override void DisableBuff()
    {
        skill_modified.duration_mult *= 0.1f;
        skill_modified.RemoveCastBehaviour(SkillCastBehaviourMod);
        skill_modified.RemoveInstanceBehaviour(SkillInstanceBehaviourMod);
    }

    public override string GetBuffDescription()
    {
        return "Wind Fury lasts 10 times longer, also Wind Fury gives +30 weapon damage while it's active";
    }

    public void SkillCastBehaviourMod(CastInfo cast_info)
    {
        if (!attack_buff_given)
        {
            weapon_dmg.Sum_value += 30;
            attack_buff_given = true;
        }
    }

    public void SkillInstanceBehaviourMod(ref CastInfo cast_info, GameObject instance)
    {
        if (skill_modified.curr_duration >= (skill_modified.duration * skill_modified.duration_mult))
        {
            weapon_dmg.Sum_value -= 30;
            attack_buff_given = false;
        }
    }
}
