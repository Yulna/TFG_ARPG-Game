using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RayHitInfo
{
    public bool hitted;
    public Vector3 hit_point;
    public int layer_hit;
    public GameObject enemy_hit; //null if no nemy is hit
}

public class CharacterController : MonoBehaviour
{
    //Singleton
    public static CharacterController instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("More than one character controller detected");
    }

    //Main camera
    public Camera pc_camera;

    //Base Stats
    public float weapon_dmg;
    public int base_max_health;
    public int base_max_resource;
    public int base_move_speed;
    public float base_armor;
    public float base_physic_res;
    public float base_fire_res;
    public float base_water_res;
    public float base_shock_res;
    public float base_earth_res;

    int curr_health;
    int curr_resource;

    //TODO: Add static item bufflist, and dynamicbufflist

    //Controllers
    public MovementController move_controller;
    public SkillController skill_controller;    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Processing Left Click");
            int mask = LayerMask.GetMask("Floor", "Enemy");

            RayHitInfo ray_hit = RayCastHandle(Input.mousePosition, mask);
            if(ray_hit.hitted)
            {
                if (ray_hit.layer_hit == LayerMask.NameToLayer("Floor"))
                {
                    move_controller.MoveToPosition(ray_hit.hit_point);
                }
                else if (ray_hit.layer_hit == LayerMask.NameToLayer("Enemy"))
                {
                    Debug.Log("Enemy Attacked");
                    move_controller.MoveToEnemy(ray_hit.enemy_hit.transform);
                    //TODO: Cast skill LMB(Attack)
                    skill_controller.CastSkill(SkillButton.LMC, ray_hit);
                }
            }                   
        }

        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            RayHitInfo ray_hit = RayCastHandle(Input.mousePosition, LayerMask.GetMask("Floor", "Enemy"));
            if (ray_hit.hitted)
            {
                skill_controller.CastSkill(SkillButton.NUM_1, ray_hit);
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            RayHitInfo ray_hit = RayCastHandle(Input.mousePosition, LayerMask.GetMask("Floor", "Enemy"));
            if (ray_hit.hitted)
            {
                skill_controller.CastSkill(SkillButton.NUM_2, ray_hit);
            }
        }


    }

    public RayHitInfo RayCastHandle(Vector3 screen_point, LayerMask mask)
    {
        RayHitInfo ret;
        RaycastHit hit;
        Ray ray = pc_camera.ScreenPointToRay(screen_point);
        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            ret.hitted = true;
            ret.hit_point = hit.point;
            ret.layer_hit = hit.collider.gameObject.layer;
            if (ret.layer_hit == LayerMask.NameToLayer("Enemy"))
            {
                ret.enemy_hit = hit.collider.gameObject;
            }
            else
                ret.enemy_hit = null;
        }
        else
        {
            ret.hitted = false;
            ret.hit_point = Vector3.zero;
            ret.layer_hit = -1;
            ret.enemy_hit = null;
        }
        return ret;
    }


    //Return true when we have enough resource to spend, false otherwise
    public bool SpendResource(int value)
    {
        if (value > curr_resource)
        {
            Debug.Log("Not enough resource");
            return false;
        }
        else
        {
            curr_resource -= value;
            return true;
        }
    }



}
