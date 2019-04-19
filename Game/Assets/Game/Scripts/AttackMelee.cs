using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMelee : SkillData
{

    public override void SkillCastBehaviour(CastInfo cast_info)
    {
       /* Debug.Log("Attac al drac");
        Collider[] hit_colliders = Physics.OverlapSphere(cast_info.end_pos, 1.5f);
        int i = 0;
        while (i < hit_colliders.Length)
        {
            if (hit_colliders[i].gameObject.tag == "Enemy")
                hit_colliders[i].GetComponent<EnemySimple>().Hurt(5);
            i++;
        }*/
    }
    
}
