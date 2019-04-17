using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RayHitInfo
{
    public bool hitted;
    public Vector3 hit_point;
    public int layer_hit;
    public GameObject go_hit; //null if no nemy is hit
}

public enum StatId
{
    WeaponDmg = 0,
    MaxHealth,
    HealthRegen,
    MaxResource,
    ResourceRegen,
    MoveSpeed,
    AttackSpeed,
    Armor,
    PhysicRes,
    FireRes,
    WaterRes,
    ShockRes,
    EarthRes,
    _numId     //Will return number of stats id (doesn't dount self since enum starts with 0)
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

    [SerializeField]
    private float[] base_stats = new float[(int)StatId._numId];
    public StatVariable[] variables_stats = new StatVariable[(int)StatId._numId];
    public StatVariable test_ms;
    
    float curr_health;
    float curr_resource;

    //Controllers
    public MovementController move_controller;
    public SkillController skill_controller;
    public Inventory inventory;

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
            int mask = LayerMask.GetMask("Floor", "Enemy", "Item");

            RayHitInfo ray_hit = RayCastHandle(Input.mousePosition, mask);
            if(ray_hit.hitted)
            {
                if (ray_hit.layer_hit == LayerMask.NameToLayer("Floor"))
                {
                    Debug.Log("moving with the floor");
                    move_controller.MoveToPosition(ray_hit.hit_point);
                }
                else if (ray_hit.layer_hit == LayerMask.NameToLayer("Enemy"))
                {
                    Debug.Log("Enemy Attacked");
                    move_controller.MoveToEnemy(ray_hit.go_hit.transform);
                    //TODO: Cast skill LMB(Attack)
                    skill_controller.CastSkill(SkillButton.LMB, ray_hit);
                }
                else if (ray_hit.layer_hit == LayerMask.NameToLayer("Item"))
                {
                    Debug.Log("picking the item");
                    ray_hit.go_hit.GetComponent<ItemWorld>().to_pick = true;
                    move_controller.MoveToPosition(ray_hit.hit_point);
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

        //Testing space
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RayHitInfo ray_hit = RayCastHandle(Input.mousePosition, LayerMask.GetMask("Floor", "Enemy"));
            if (ray_hit.hitted)
            {
                skill_controller.TestingTEst(ray_hit);
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
            ret.go_hit = hit.collider.gameObject;            
        }
        else
        {
            ret.hitted = false;
            ret.hit_point = Vector3.zero;
            ret.layer_hit = -1;
            ret.go_hit = null;
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

    public Transform GetPlayerTransform()
    {
       return move_controller.gameObject.transform;   
    }

}
