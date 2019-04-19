using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dragon Breath", menuName = "Chambers of Elrankat/Skills/Dragon Breath")]
public class SkillDataDragonBreath : SkillData
{

    public override void SkillCastBehaviour(CastInfo cast_info)
    {
        GameObject display = Instantiate(skill_display, cast_info.end_pos, Quaternion.identity);
        SkillInstance instance = display.AddComponent<SkillInstance>();
        instance.InitInstance(SkillBehaviour, cast_info);
    }


    public override void SkillBehaviour(ref CastInfo cast_info, GameObject instance)
    {



    }

}
