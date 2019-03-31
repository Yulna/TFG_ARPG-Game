using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Simple struc to simplify Raycast calls
public struct RayHitInfo2
{
    public bool hitted;
    public Vector3 hit_point;
    public int layer_hit;
}

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    //Simple Singleton approach
    public static PlayerController instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("More than one player script detected");
    }

    //Player Data
    public int health;
    public int resource;
    public int move_speed;

    //Movement 
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
            int mask = LayerMask.GetMask("Floor", "Enemy");
            
            RayHitInfo2 rayhit = HandleCameraRay(Input.mousePosition, mask);
            if(rayhit.hitted)
            {
                if (rayhit.layer_hit == LayerMask.NameToLayer("Floor"))
                {
                    Move(rayhit.hit_point);
                }
                else if (rayhit.layer_hit == LayerMask.NameToLayer("Enemy"))
                {
                    Debug.Log("Enemy Attacked");

                    Move(rayhit.hit_point);
                }
            }
        }
    }

    public RayHitInfo2 HandleCameraRay(Vector3 screen_point, LayerMask mask)
    {
        RayHitInfo2 ret;
        ret.hitted = false;
        ret.hit_point = Vector3.zero;
        ret.layer_hit = -1;

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(screen_point);

        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            ret.hitted = true;
            ret.hit_point = hit.point;
            ret.layer_hit = hit.collider.gameObject.layer;    
        }
      
        return ret;
    }


    void Move(Vector3 destination)
    {
        pc_animator.SetBool("Moving", true);
        pc_animator.SetFloat("Velocity Z", 10.0f);

        agent.destination = destination;
    }

    //Return true when we have enough resource to spend, false otherwise
    public bool SpendResource(int value)
    {
        if (value > resource)
        {
            Debug.Log("Not enough resource");
            return false;
        }
        else
        {
            resource -= value;
            return true;
        }
    }
}
