
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public delegate void ProjectileBehaviour(ref CastInfo cast_info, GameObject dispaly);
    CastInfo cast_info;
    SphereCollider col;
    // Action behaviour;
    ProjectileBehaviour behaviour = null;

    public void CastProjectile(CastInfo info, float max_range, ProjectileBehaviour skill_behaviour)
    {
        cast_info = info;
        behaviour = skill_behaviour;
    }

    public void ReplaceBehaviour(ProjectileBehaviour new_behaviour)
    {
        behaviour = new_behaviour;
    }

    // Update is called once per frame
    void Update()
    {
        if (behaviour == null)
            return;
        else   
            behaviour(ref cast_info, gameObject);
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Projectile collision");
        if(collision.collider.gameObject.CompareTag("Enemy"))
        {
            collision.collider.gameObject.GetComponent<EnemySimple>().Hurt(2);
        }
    }
}
