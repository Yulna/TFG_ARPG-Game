using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffVampireRefuge : Buff
{
    public BuffVampireRefuge() : base(BuffType.BUFF_SKILL_MOD, 0, null)
    {
        skill_modified = (SkillDataSpiritRefuge)CharacterController.instance.skill_controller.GetSkillByName("Spirit Refuge");
    }

    //Must be SpiritRefge
    public SkillDataSpiritRefuge skill_modified;

    public override void EnableBuff()
    {
        skill_modified.AddCastBehaviour(SkillCastBehaviourMod);
    }

    public override void DisableBuff()
    {
        skill_modified.RemoveCastBehaviour(SkillCastBehaviourMod);
    }

    public override string GetBuffDescription()
    {
        return "Spirit refuge now damage enemies inside it by 10% weapon damage each second, for each second an enemy is hit reduces spirit refuge cooldown by 1 second";
    }


    public void SkillCastBehaviourMod(CastInfo cast_info)
    {
        skill_modified.cd_timer = 0;
    }
}
