using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{

    public Skill skill_1;
    public Skill skill_2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && skill_1 != null)
        {
            skill_1.DoSkill();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && skill_2 != null)
        {
            skill_2.DoSkill();
        }
    }
}
