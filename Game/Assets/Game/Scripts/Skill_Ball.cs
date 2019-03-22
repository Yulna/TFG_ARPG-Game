using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Ball", menuName = "MyGame/Skills/Skill Ball")]
public class Skill_Ball : Skill
{
    
    public GameObject projectile_display;

    public override void SkillBehaviour(ref CastInfo cast_info, GameObject display)
    {
        Debug.Log("skill ball update");
        display.transform.Translate(cast_info.dir * speed, Space.World);

        cast_info.curr_dist += cast_info.dir.magnitude * 0.1f;
    }

    public void ItemBehaviour(ref CastInfo cast_info, GameObject display)
    {
        Debug.Log("skill ball update");
        display.transform.Translate(cast_info.dir * -speed , Space.World);

        cast_info.curr_dist += cast_info.dir.magnitude * 0.1f * 2;
    }

    public override void CastSkill(Vector3 org, Vector3 dest)
    {
        Debug.Log("No CastSkill() override");
        PlayerController.instance.SpendResource(cost);

        GameObject projectile = Instantiate(projectile_display, org, Quaternion.identity);
        projectile.AddComponent<Projectile>();
        Projectile pro = projectile.GetComponent<Projectile>();
        pro.CastProjectile(InitCastInfo(org, dest), distance, SkillBehaviour);

        //Item Test
        pro.ReplaceBehaviour(ItemBehaviour);
    }

}
