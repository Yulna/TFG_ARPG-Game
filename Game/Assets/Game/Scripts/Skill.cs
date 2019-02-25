using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill : MonoBehaviour
{

    //Casting info --> Need to be set each time it's casted
    protected Vector3 origin_pos;
    protected Vector3 end_pos;
    protected Vector3 dir;

    //General info  
    public string skill_name;
    public int magnitude;           //Damage or healing of the skill
    public int cost;                //Resource cost of the skill
    public float distance;          //Max range of the skill
    public float speed;             //Speed of the projectile
    public float duration;          //Time skill is active (mainly for buffs)
    public float cooldown;          //Cooldown of the skill
 //   public GameObject skill_display;//Go with the art for the skill <-- Same GO


    public LayerMask obj_mask;


    //Internal variables
    protected float curr_dist; //Current distance form the origin pos
    protected bool casted = false; //Simple protection in case direction isn't defined

    void Update()
    {
        if (casted && curr_dist <= distance)
            SkillBehaviour();
        else
        {
            OnSkillEnd();
            Destroy(gameObject);
        }
    }

    //Return true if the skill can be casted, false otherwise (Still in CD, no mana...)
    public virtual void SkillBehaviour()
    {
        Debug.LogWarning("No SkillBehaviour() override detected");
    }

    public virtual void OnSkillEnd()
    {
        Debug.LogWarning("No OnSkillEnd() override detected");
    }


    public void CastSkill(Vector3 origin, Vector3 dest)
    {
        origin_pos = origin;
        end_pos = dest;
        dir = end_pos - origin_pos;
        dir.Normalize();
        casted = true;
    }

    /*
        public void DoSkill()
        {


            RayHitInfo hit_info = PlayerController.instance.HandleCameraRay(Input.mousePosition, obj_mask);

            Vector3 dir = hit_info.hit_point - PlayerController.instance.gameObject.transform.position;

            GameObject to_fire = Instantiate(skill_display, PlayerController.instance.gameObject.transform.position, PlayerController.instance.gameObject.transform.rotation);
            to_fire.AddComponent<Skill_2>().skill_dir = dir;
        }
    */
}