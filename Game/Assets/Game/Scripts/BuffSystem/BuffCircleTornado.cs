using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCircleTornado : Buff
{
    public BuffCircleTornado() : base(BuffType.BUFF_SKILL_MOD, 0, null)
    {
        skill_modified = (SkillDataWindRush)CharacterController.instance.skill_controller.GetSkillByName("Wind Rush");
    }

    //Must be Wind Rush
    public SkillDataWindRush skill_modified;

    public override void EnableBuff()
    {
        skill_modified.AddInstanceBehaviour(SkillInstanceBehaviourMod);
    }

    public override void DisableBuff()
    {
        skill_modified.RemoveInstanceBehaviour(SkillInstanceBehaviourMod);
    }

    public override string GetBuffDescription()
    {
        return "Cricle tornado";
    }

    public void SkillInstanceBehaviourMod(ref CastInfo cast_info, GameObject instance)
    {
        instance.transform.Translate((Quaternion.Euler(0,90, 0) * cast_info.dir) * skill_modified.projectile_speed * 2, Space.World);
        cast_info.dir = instance.transform.position - cast_info.origin_pos;
        cast_info.dir.y = 0;
        cast_info.dir.Normalize();
    }
}
