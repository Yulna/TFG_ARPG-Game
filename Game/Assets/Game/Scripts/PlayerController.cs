using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    //Player Data
    public int health;
    public int mana;
    public int move_speed;

    Vector3 destination;

    //Camera
    public Camera camera;

    //Animation Data
    Animator pc_animator;

    //Pathfinding Data
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        //Get Components
        pc_animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        //Setting variables
        agent.speed = move_speed;
    }


    // Update is called once per frame
    void Update()
    {
        //Animation Stoper
        //TODO: Look on ways to stop outside update
        if (agent.remainingDistance == 0)
        {
            pc_animator.SetBool("Moving", false);
            pc_animator.SetFloat("Velocity Z", 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Processing Left Click");


            int mask = LayerMask.GetMask("Floor");

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                destination = hit.point;              

                Move(destination);
            }

        }
    }

    //TODO: Think what this has to return
    void HandleCameraRay(Vector3 screen_point, LayerMask mask)
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(screen_point);

        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {

            

        }
    }


    void Move(Vector3 destination)
    {
        pc_animator.SetBool("Moving", true);
        pc_animator.SetFloat("Velocity Z", 10.0f);

        agent.destination = destination;
    }

}
