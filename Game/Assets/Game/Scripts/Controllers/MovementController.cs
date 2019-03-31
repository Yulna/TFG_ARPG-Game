using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class MovementController : MonoBehaviour
{
    //Required components
    Animator pc_animator;
    NavMeshAgent pc_agent;
    public Transform to_follow;
    public float _remain;
    public bool pending;

    // Start is called before the first frame update
    void Start()
    {
        pc_animator = GetComponent<Animator>();
        pc_agent = GetComponent<NavMeshAgent>();
        pc_agent.speed = CharacterController.instance.base_move_speed;
        _remain = 0;
    }

    // Update is called once per frame
    void Update()
    {

        _remain = pc_agent.remainingDistance;
        if (pc_agent.remainingDistance <= pc_agent.stoppingDistance && !pc_agent.pathPending) 
        {
            pc_animator.SetBool("Moving", false);
            pc_animator.SetFloat("Velocity Z", 0); //Speed of the moving animation (Z is forward)
            to_follow = null; //set to null in case we are following something
        }
        else
        {
            pc_animator.SetBool("Moving", true);
            pc_animator.SetFloat("Velocity Z", 10.0f);

            if (to_follow != null && to_follow.position != pc_agent.destination)
                pc_agent.SetDestination(to_follow.position);            
        }
    }

    public void MoveToPosition(Vector3 destination)
    {
        pc_agent.destination = destination;   
    }

    public void MoveToEnemy(Transform enemy_transform)
    {
        to_follow = enemy_transform;
        pc_agent.SetDestination(to_follow.position);
    }

}
