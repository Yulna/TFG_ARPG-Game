using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Ball", menuName = "MyGame/Skills/Skill Ball")]
public class Skill_Ball : Skill
{
    
    public override void SkillBehaviour(GameObject display, ref CastInfo cast_info)
    {
        Debug.Log("skill ball update");
        display.transform.Translate(cast_info.dir * speed, Space.World);
        cast_info.curr_dist += cast_info.dir.magnitude * 0.1f;
        if(skill_display.GetComponent<Collider>())
        {
            Debug.Log("Collider Detected");
        }
    }
}
