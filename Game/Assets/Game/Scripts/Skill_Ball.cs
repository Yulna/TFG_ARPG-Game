using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Ball : Skill
{
    
    public override void SkillBehaviour()
    {
        transform.Translate(dir * 0.1f, Space.World);
        curr_dist += dir.magnitude * 0.1f;
    }
}
