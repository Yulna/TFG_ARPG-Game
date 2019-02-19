using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2 : MonoBehaviour
{
    public Vector3 skill_dir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("Doing skill 2");
        transform.Translate(skill_dir*0.1f, Space.World);   
    }
}
