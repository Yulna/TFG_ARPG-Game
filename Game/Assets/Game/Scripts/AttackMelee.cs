using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMelee : Skill
{

    public override void CastSkill(Vector3 org, Vector3 dest)
    {
        Debug.Log("Attac al drac");
        Collider[] hit_colliders = Physics.OverlapSphere(dest, 1.5f);
        int i = 0;
        while (i < hit_colliders.Length)
        {
            if (hit_colliders[i].gameObject.tag == "Enemy")
                hit_colliders[i].GetComponent<EnemySimple>().Hurt(5);
            i++;
        }
    }
    
}
