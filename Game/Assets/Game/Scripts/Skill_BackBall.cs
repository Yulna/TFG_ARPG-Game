using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill BackBall", menuName = "Chambers of Elrankat/Skills/Skill Back Ball")]
public class Skill_BackBall : Skill
{
    //Data
    public float damage;            //Damage
    public float speed;             //Speed of the projectile
    public float max_range;         //Max range of the skill

    public GameObject projectile_display;

    public override void SkillBehaviour(ref CastInfo cast_info, GameObject display)
    {
        Debug.Log("skill ball update");
        display.transform.Translate(cast_info.dir * -speed, Space.World);

        cast_info.curr_dist += cast_info.dir.magnitude * 0.1f;
    }

  

    public override void CastSkill(Vector3 org, Vector3 dest)
    {
        CharacterController.instance.SpendResource(cost);

        GameObject projectile = Instantiate(projectile_display, org, Quaternion.identity);
        projectile.AddComponent<Projectile>();
        Projectile pro = projectile.GetComponent<Projectile>();
        pro.CastProjectile(InitCastInfo(org, dest), max_range, SkillBehaviour);
    }
}
