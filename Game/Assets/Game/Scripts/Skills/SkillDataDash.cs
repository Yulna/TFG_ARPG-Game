using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Dash", menuName = "Chambers of Elrankat/Skills/Dash")]
public class SkillDataDash : SkillData
{
    public float range;
    public float range_mult;
    public float precision;
    public float speed;
    public GameObject dash_trail;


    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        if (Vector3.Distance(cast_info.end_pos, cast_info.origin_pos) > range)
        {
            cast_info.end_pos = cast_info.origin_pos + (cast_info.dir * range);
        }
       

        GameObject display = Instantiate(skill_display, cast_info.origin_pos, Quaternion.identity);
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.InitInstance(SkillBehaviour, cast_info);

        if (!CharacterController.instance.move_controller.StartJump(instance.gameObject))
        {
            Debug.Log("Dash failed");
            CharacterController.instance.SpendResource(-cost);
            Destroy(display);
            return;
        }

        Collider[] hit_colliders = Physics.OverlapCapsule(cast_info.origin_pos, cast_info.end_pos, 1);
        for (int i = 0; i < hit_colliders.Length; i++)
        {
            if (hit_colliders[i].gameObject.tag == "Enemy")            
                hit_colliders[i].GetComponent<EnemySimple>().Hurt(weapon_dmg.Buffed_value * skill_dmg_mult);            
        }

        GameObject trail = Instantiate(dash_trail, cast_info.origin_pos + cast_info.dir, Quaternion.LookRotation(cast_info.dir, Vector3.up));
        Destroy(trail, 5);
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {
        //Instant cast/damage spell void behaviour to prevent warnings
        if (Vector3.Distance(instance.transform.position, cast_info.end_pos) > precision)
        {
            instance.transform.position = CharacterController.instance.GetPlayerTransform().position;

            Vector3 push_dir = cast_info.end_pos - instance.transform.position;
            push_dir.Normalize();
            CharacterController.instance.move_controller.GetComponent<NavMeshAgent>().Warp(instance.transform.position + (push_dir * Time.deltaTime * speed));
        }
        else
        {
            CharacterController.instance.move_controller.EndJump();
            Destroy(instance);
        }
    }


    public override string GetDescription()
    {
        string ret_des = "";

        ret_des += "Dashes towards the direction of the cursors, dealing (";
        ret_des += weapon_dmg.Buffed_value * skill_dmg_mult;
        ret_des += ") ";
        ret_des += skill_dmg_mult * 100;
        ret_des += "% weapon damge to enemies in the way";

        return ret_des;
    }
}
