using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{

    public Skill_Ball skill_3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && skill_3 != null)
        {

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
                GameObject test = Instantiate(skill_3.gameObject, transform.position, transform.rotation);
                test.GetComponent<Skill>().CastSkill(transform.position, dest);
            }
        }
    }

}
