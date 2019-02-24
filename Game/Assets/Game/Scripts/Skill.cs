using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Skill", menuName = "MyGame/Skill")]
public class Skill : ScriptableObject
{

    public int magnitude;
    public LayerMask obj_mask;

    public CastType cast_type;

    public GameObject skill_display;

    public void DoSkill()
    {
        if(cast_type == CastType.SkillShoot)
        {

            RayHitInfo hit_info = PlayerController.instance.HandleCameraRay(Input.mousePosition, obj_mask);

            Vector3 dir = hit_info.hit_point - PlayerController.instance.gameObject.transform.position;

            GameObject to_fire = Instantiate(skill_display, PlayerController.instance.gameObject.transform.position, PlayerController.instance.gameObject.transform.rotation);
            to_fire.AddComponent<Skill_2>().skill_dir = dir;                    
        }
            
        if(cast_type == CastType.PointAndClick)
        {

            RayHitInfo hit_info = PlayerController.instance.HandleCameraRay(Input.mousePosition, obj_mask);

            Vector3 dir = hit_info.hit_point - PlayerController.instance.gameObject.transform.position;

            Debug.Log("Start the courotine");
            PlayerController.instance.StartCoroutine(PlayerController.instance.DoPointAndClick(dir, Instantiate(skill_display, PlayerController.instance.gameObject.transform.position, PlayerController.instance.transform.rotation)));
        }
    }

/*    private IEnumerator DoPointAndClick(Vector3 skill_dir, GameObject skill_disp)
    {
        Debug.Log("Coroutine started");
        skill_disp.transform.Translate(skill_dir * 0.1f, Space.World);
        yield return new WaitForSeconds(0.01f);
    }
    */
}


public enum CastType
{
    SelfAimed,
    SkillShoot,
    PointAndClick
}