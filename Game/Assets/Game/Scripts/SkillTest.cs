using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTest : MonoBehaviour
{

    public GameObject magic1;
    public GameObject magic2;

    public Camera camera;

    bool toogle_loop = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            toogle_loop = !toogle_loop;


        //Test 1: Coroutine 
        if(Input.GetKey(KeyCode.Alpha1) && magic1 != null)
        {
            Debug.Log("Using skill 1");

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            int layermask = LayerMask.NameToLayer("Floor");
            layermask = ~layermask;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
            {

                Vector3 dir = hit.point - gameObject.transform.position;
                dir.Normalize();

                StartCoroutine(DoSkill_1(dir, Instantiate(magic1, gameObject.transform.position, gameObject.transform.rotation)));
            }
            else
            {
                Debug.Log("Very bad patates al sac");
            }

        }


        //Test 2: Skill prefab instance
        if (Input.GetKey(KeyCode.Alpha2) && magic2 != null)
        {
            Debug.Log("Using Skill 2");
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            int layermask = LayerMask.NameToLayer("Floor");
            layermask = ~layermask;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
            {
                Vector3 dir = hit.point - gameObject.transform.position;
                dir.Normalize();

                GameObject new_skill = Instantiate(magic2, transform.position, transform.rotation);
                new_skill.GetComponent<Skill_2>().skill_dir = dir;
            }
        }
        
    }


    private IEnumerator DoSkill_1(Vector3 skill_dir, GameObject magic)
    {
        while (toogle_loop)
        {
          //  Debug.Log("Doing skill 1");

            magic.transform.Translate(skill_dir*0.1f, Space.World);

            // yield return new WaitForSeconds(0.1f);
            yield return null;
        }
    }

}
