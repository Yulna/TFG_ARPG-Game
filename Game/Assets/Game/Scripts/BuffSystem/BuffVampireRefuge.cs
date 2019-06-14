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
        skill_modified.AddInstanceBehaviour(SkillInstanceBehaviourMod);
    }

    public override void DisableBuff()
    {
        skill_modified.RemoveInstanceBehaviour(SkillInstanceBehaviourMod);
    }

    public override string GetBuffDescription()
    {
        return "Spirit refuge now damage enemies inside it by 10% weapon damage each second, for each second an enemy is hit reduces spirit refuge cooldown by 1 second";
    }


    public void SkillInstanceBehaviourMod(ref CastInfo cast_info, GameObject instance)
    {

        Collider[] hit_colliders = Physics.OverlapSphere(cast_info.end_pos, skill_modified.effect_area * skill_modified.effect_area_mult);

        for (int i = 0; i < hit_colliders.Length; i++)
        {
            if (hit_colliders[i].gameObject.tag == "Enemy")
            {
                hit_colliders[i].GetComponent<EnemySimple>().Hurt((skill_modified.weapon_dmg.Buffed_value * 0.1f) * Time.deltaTime);
                skill_modified.cd_timer -= Time.deltaTime;
            }
        }
    }
}
