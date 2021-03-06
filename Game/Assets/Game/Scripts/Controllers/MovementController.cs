﻿using System.Collections;
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
    public bool move_ended;
    public StatVariable move_speed;

    public GameObject dash_instance;

    // Start is called before the first frame update
    void Start()
    {
        dash_instance = null;
        //dash_instance_id = 0;
        pc_animator = GetComponent<Animator>();
        pc_agent = GetComponent<NavMeshAgent>();
        //pc_agent.speed = CharacterController.instance.base_stats[(int)StatId.MoveSpeed];
        pc_agent.speed = move_speed.Buffed_value;
        move_ended = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (pc_agent.remainingDistance <= pc_agent.stoppingDistance && !pc_agent.pathPending && !move_ended) 
        {
            Debug.Log("stop now!");
            pc_animator.SetBool("Moving", false);
            pc_animator.SetFloat("Velocity Z", 0); //Speed of the moving animation (Z is forward)
            to_follow = null; //set to null in case we are following something
            move_ended = true;
        }
        else if(!move_ended)
        {
            pc_animator.SetBool("Moving", true);
            pc_animator.SetFloat("Velocity Z", 10.0f);
            if (to_follow != null && to_follow.position != pc_agent.destination)
                pc_agent.SetDestination(to_follow.position);            
        }        
    }

    public void StopMovement()
    {
        pc_agent.destination = transform.position;
    }

    public void MoveToPosition(Vector3 destination)
    {
        if (CharacterController.instance.skill_controller.cast_timer > 0)
            return;
        pc_agent.destination = destination;
        move_ended = false;
    }

    public void MoveToEnemy(Transform enemy_transform)
    {
        if (CharacterController.instance.skill_controller.cast_timer > 0)
            return;
        to_follow = enemy_transform;
        pc_agent.SetDestination(to_follow.position);
        move_ended = false;
    }

    public void UpdateMoveSpeed()
    {
        pc_agent.speed = move_speed.Buffed_value;
    }

    public bool StartJump(GameObject instance)
    {
        if(dash_instance != instance)
            Destroy(dash_instance);
        if (dash_instance == instance)
            return false;

        dash_instance = instance;
        return true;

    }
    public void EndJump()
    {
        dash_instance = null;
    }
}
