﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wind Fury", menuName = "Chambers of Elrankat/Skills/Wind Fury")]
public class SkillDataWindFury : SkillData
{
    public float duration;
    public float duration_mult;
    public StatVariable atk_speed;
    public float as_percent_aug;
    public float curr_duration;
    public bool is_active;

    private void OnEnable()
    {
        skill_cast_del = SkillCastBehaviour;
        skill_instance_del = SkillBehaviour;
        cd_timer = 0;
        is_active = false;
    }

    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        if (!is_active)
        {
            GameObject display = Instantiate(skill_display, cast_info.origin_pos, Quaternion.identity);
            SkillInstance instance = display.AddComponent<SkillInstance>();
            instance.InitInstance(skill_instance_del, cast_info);
            atk_speed.Mult_value *= as_percent_aug;
        }

        //Don't use instance duration since we want to ONLY have one of this skill buff active at a time
        curr_duration = 0;
        is_active = true;        
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {
        curr_duration += Time.deltaTime;
        instance.transform.position = CharacterController.instance.GetPlayerTransform().position;

        if (curr_duration >= (duration * duration_mult))
        {
            Destroy(instance);
            is_active = false;
            atk_speed.Mult_value /= as_percent_aug;
        }
    }

    public override string GetDescription()
    {
        string ret_des = "";

        ret_des += "Call wind spirits to imubue yourself increasing your attack speed by ";
        ret_des += (as_percent_aug * 100) - 100 + "%";

        return ret_des;
    }
}
