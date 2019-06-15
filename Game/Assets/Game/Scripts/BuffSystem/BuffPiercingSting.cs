using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPiercingSting : Buff
{
    public BuffPiercingSting() : base(BuffType.BUFF_SKILL_MOD, 0, null)
    {
        skill_modified = (SkillDataSting)CharacterController.instance.skill_controller.GetSkillByName("Sting");
    }

    //Must be Ice Armor
    public SkillDataSting skill_modified;


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
        return "Sring now pierces enemies, dealing (" + skill_modified.weapon_dmg.Buffed_value.ToString() + ") 100% weapon damage to " +
            "all enemies hit (including the first one)";
    }

    public void SkillCastBehaviourMod(CastInfo cast_info)
    {
        cast_info.origin_pos += Vector3.up * 1.5f;
        cast_info.dir = cast_info.end_pos - cast_info.origin_pos;
        cast_info.dir.Normalize();
        cast_info.end_pos = cast_info.origin_pos + (cast_info.dir * skill_modified.lenght_range);

        Collider[] hit_colliders = Physics.OverlapCapsule(cast_info.origin_pos, cast_info.end_pos, skill_modified.width_range);
        for (int i = 0; i < hit_colliders.Length; i++)
        {
            if (hit_colliders[i].gameObject.tag == "Enemy")
            {
                hit_colliders[i].GetComponent<EnemySimple>().Hurt(skill_modified.weapon_dmg.Buffed_value);
            }
        }
    }
}
