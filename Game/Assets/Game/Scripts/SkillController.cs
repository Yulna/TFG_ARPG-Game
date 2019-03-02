using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CastInfo
{
    //Casting info --> Need to be set each time it's casted
     public Vector3 origin_pos;
     public Vector3 end_pos;
     public Vector3 dir;
     public float curr_dist;
}

public class SkillController : MonoBehaviour
{

    public Skill skill_3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && skill_3 != null)
        {
            //TODO: Call a Courutine to update the skill, when skill ends kill coroutine


        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && skill_3 != null)
        {
            RayHitInfo hit_info = PlayerController.instance.HandleCameraRay(Input.mousePosition, LayerMask.GetMask("Enemy", "Floor"));

            CastSkill(2, hit_info.hit_point);

        }
    }

    void CastSkill(int index, Vector3 dest)
    {
   
        if (index == 2)
        {
            if (PlayerController.instance.SpendResource(skill_3.cost))
            {
                GameObject skill_display = Instantiate(skill_3.skill_display, transform.position, transform.rotation);

                CastInfo cast_info = InitCastInfo(transform.position, dest);                
                StartCoroutine(DoSkill(skill_3, skill_display, cast_info));

            }
        }
    }



    //CastSkillCourutine
    private IEnumerator DoSkill(Skill skill, GameObject display, CastInfo cast_info)
    {
        CastInfo current_cast = cast_info;
        bool skill_alive = true;
        while (skill_alive)
        {
            skill_alive = skill.SkillUpdate(display, ref current_cast);
            yield return null;
        }
        Destroy(display);
    }



    public CastInfo InitCastInfo(Vector3 origin, Vector3 dest)
    {
        CastInfo ret = new CastInfo();

        ret.origin_pos = origin;
        ret.end_pos = dest;
        ret.dir = ret.end_pos - ret.origin_pos;
        ret.dir.Normalize();

        return ret;
    }

}
