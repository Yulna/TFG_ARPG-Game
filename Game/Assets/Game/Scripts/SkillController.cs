using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkillController : MonoBehaviour
{

    public Skill skill_2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && skill_2 != null)
        {
            RayHitInfo hit_info = PlayerController.instance.HandleCameraRay(Input.mousePosition, LayerMask.GetMask("Enemy", "Floor"));
            skill_2.CastSkill(transform.position, hit_info.hit_point);            
        }
    }

}
