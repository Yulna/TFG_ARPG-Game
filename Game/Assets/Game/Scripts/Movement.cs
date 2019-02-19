using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour {

    public Vector3 pa_alla;
    public Camera camera;

    private Animator pc_animator;

    NavMeshAgent agent;

    private void Start()
    {
        pc_animator = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update ()
    {
        //Note: Change this outside update
        //Try contronling movement manually (no Navagent)->Coroutine may help
        if (agent.remainingDistance == 0)
        {
            pc_animator.SetBool("Moving", false);
            pc_animator.SetFloat("Velocity Z", 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Skiddale, skiddodle, your mesh is now a noodle");

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                pa_alla = hit.point;
                Move(pa_alla);
            }
        }

      

    }

    void Move (Vector3 pa_alla)
    {
        pc_animator.SetBool("Moving", true);
        pc_animator.SetFloat("Velocity Z", 10.0f);
       
        agent.destination = pa_alla;
    }


}
