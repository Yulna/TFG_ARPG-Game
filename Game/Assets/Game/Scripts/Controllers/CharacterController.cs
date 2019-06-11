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

public enum DamageType
{
    DmgTrue = 0,
    DmgPhysical = StatId.PhysicRes,
    DmgFire = StatId.FireRes,
    DmgWater = StatId.WaterRes,
    DmgShock = StatId.ShockRes,
    DmgEarth = StatId.EarthRes
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
    
    [SerializeField]
    float curr_health;
    [SerializeField]
    float curr_resource;
    [SerializeField]
    float dmg_half_reduction;

    //Controllers
    public MovementController move_controller;
    public SkillController skill_controller;
    public Inventory inventory;

    //UI
    public GameObject inventory_canvas;
    public GameObject character_stats_canvas;

    // Start is called before the first frame update
    void Start()
    {
        dmg_half_reduction = 100;
        curr_health = variables_stats[(int)StatId.MaxHealth].Buffed_value;
        curr_resource = variables_stats[(int)StatId.MaxResource].Buffed_value;

        inventory_canvas.SetActive(false);
        character_stats_canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Resource/health regen
        if (curr_health < variables_stats[(int)StatId.MaxHealth].Buffed_value)
        {
            curr_health += variables_stats[(int)StatId.HealthRegen].Buffed_value * Time.deltaTime;
            if (curr_health > variables_stats[(int)StatId.MaxHealth].Buffed_value)
                curr_health = variables_stats[(int)StatId.MaxHealth].Buffed_value;
        }

        if (curr_resource < variables_stats[(int)StatId.MaxResource].Buffed_value)
        {
            curr_resource += variables_stats[(int)StatId.ResourceRegen].Buffed_value * Time.deltaTime;
            if (curr_resource > variables_stats[(int)StatId.MaxResource].Buffed_value)
                curr_resource = variables_stats[(int)StatId.MaxResource].Buffed_value;
        }

        if (Input.GetKeyUp(KeyCode.I))
            inventory_canvas.SetActive(!inventory_canvas.activeSelf);
        if (Input.GetKeyUp(KeyCode.C))
            character_stats_canvas.SetActive(!character_stats_canvas.activeSelf);
        //Don't read inputs if UI active
        if (inventory_canvas.activeSelf || character_stats_canvas.activeSelf)
            return;

        //Read game inputs
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

        if (Input.GetMouseButtonDown(1))
        {
            RayHitInfo ray_hit = RayCastHandle(Input.mousePosition, LayerMask.GetMask("Floor", "Enemy"));
            if (ray_hit.hitted)
            {
                skill_controller.CastSkill(SkillButton.RMC, ray_hit);
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
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
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            RayHitInfo ray_hit = RayCastHandle(Input.mousePosition, LayerMask.GetMask("Floor", "Enemy"));
            if (ray_hit.hitted)
            {
                skill_controller.CastSkill(SkillButton.NUM_3, ray_hit);
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            RayHitInfo ray_hit = RayCastHandle(Input.mousePosition, LayerMask.GetMask("Floor", "Enemy"));
            if (ray_hit.hitted)
            {
                skill_controller.CastSkill(SkillButton.NUM_4, ray_hit);
            }
        }

        //Testing space
        if (Input.GetKeyDown(KeyCode.Space))
        {
        
        }
      
    }

    public StatVariable GetStat(StatId id)
    {
        return variables_stats[(int)id];
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
    public bool SpendResource(float value)
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


    public void DamagePlayer(float dmg_value, DamageType type)
    {
        float true_damage;
        Debug.Log("Player Damaged");
        if (type != DamageType.DmgTrue)
        {
            float armor_mitigation = dmg_half_reduction / (dmg_half_reduction + variables_stats[(int)StatId.Armor].Buffed_value);
            float res_mitigation = dmg_half_reduction / (dmg_half_reduction + variables_stats[(int)type].Buffed_value);

            true_damage = dmg_value * armor_mitigation * res_mitigation;
        }
        else
        {
            true_damage = dmg_value;
        }

        if (true_damage > curr_health)
            curr_health = 0;
        else
            curr_health -= true_damage;

        //Just to prevent display errors
        //Don't want display to mark 0 health because health value is 0.XX
        if (curr_health < 1 && curr_health > 0)
            curr_health = 1;
    }

    public void HealPlayer(float health_value)
    {
        if (curr_health >= variables_stats[(int)StatId.MaxHealth].Buffed_value)
            return;

        if ((health_value + curr_health) > variables_stats[(int)StatId.MaxHealth].Buffed_value)
            curr_health = variables_stats[(int)StatId.MaxHealth].Buffed_value;
        else
            curr_health += health_value;
    }

    public float GetHealthPercentile()
    {        
        return curr_health / variables_stats[(int)StatId.MaxHealth].Buffed_value;
    }

    public float GetResourcePercentile()
    {
        return curr_resource / variables_stats[(int)StatId.MaxResource].Buffed_value;
    }

}
