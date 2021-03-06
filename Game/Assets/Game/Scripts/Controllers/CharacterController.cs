﻿using System.Collections;
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
    public GameObject main_menu_canvas;
    public GameObject game_over_canvas;
    public bool ui_open;

    //Debuf
    public bool invincible;
    public bool no_mana_cost;

    public GameObject invincible_display;
    public GameObject no_mana_display;

    // Start is called before the first frame update
    void Start()
    {
        dmg_half_reduction = 100;
        ui_open = false;
        invincible = false;
        no_mana_cost = false;
        inventory.InitInventory();
        inventory.ActivateEquipBuffs();
        inventory_canvas.SetActive(false);
        character_stats_canvas.SetActive(false);
        game_over_canvas.SetActive(false);

        curr_health = variables_stats[(int)StatId.MaxHealth].Buffed_value;
        curr_resource = variables_stats[(int)StatId.MaxResource].Buffed_value;

        invincible_display.SetActive(invincible);
        no_mana_display.SetActive(no_mana_cost);
    }

    public void RecoverAll()
    {
        curr_health = variables_stats[(int)StatId.MaxHealth].Buffed_value;
        curr_resource = variables_stats[(int)StatId.MaxResource].Buffed_value;
        game_over_canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (game_over_canvas != null && game_over_canvas.activeSelf)
           return;

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


        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (skill_controller.GetSkillSelectionUIStatus())
            {
                skill_controller.ForceSkillSelectioUIStatus(false);
                return;
            }
            if (inventory_canvas.activeSelf)
            {
                inventory_canvas.SetActive(false);
                return;
            }
            if (character_stats_canvas.activeSelf)
            {
                character_stats_canvas.SetActive(false);
                return;
            }

            if (main_menu_canvas != null)
                main_menu_canvas.SetActive(!main_menu_canvas.activeSelf);
        }

        if (main_menu_canvas.activeSelf)
            return;

        if (Input.GetKeyUp(KeyCode.I))
            inventory_canvas.SetActive(!inventory_canvas.activeSelf);
        if (Input.GetKeyUp(KeyCode.C))
            character_stats_canvas.SetActive(!character_stats_canvas.activeSelf);


        //Don't read inputs if UI active
        if (inventory_canvas.activeSelf || character_stats_canvas.activeSelf || skill_controller.skill_selection_UI.activeSelf)
            return;

        //Read game inputs
        if (Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftShift))
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
                    if (!skill_controller.UseBaseAttack(ray_hit))
                        move_controller.MoveToEnemy(ray_hit.go_hit.transform);                    
                }
                else if (ray_hit.layer_hit == LayerMask.NameToLayer("Item"))
                {
                    Debug.Log("picking the item");
                    ray_hit.go_hit.GetComponent<ItemWorld>().to_pick = true;
                    move_controller.MoveToPosition(ray_hit.hit_point);
                }
            }                   
        }

        if(Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            RayHitInfo ray_hit = RayCastHandle(Input.mousePosition, LayerMask.GetMask("Floor", "Enemy"));
            if (ray_hit.hitted)
            {
                skill_controller.CastSkill(SkillButton.LMB, ray_hit);
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


        if (Input.GetKeyDown(KeyCode.F1))
        {
            invincible = !invincible;
            invincible_display.SetActive(invincible);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            no_mana_cost = !no_mana_cost;
            no_mana_display.SetActive(no_mana_cost);
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
        Vector3 offset_correction = new Vector3(0, 1.5f, 0);
        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            ret.hitted = true;
            ret.layer_hit = hit.collider.gameObject.layer;
            if (ret.layer_hit == LayerMask.NameToLayer("Enemy"))
                ret.hit_point = hit.collider.transform.position + offset_correction;
            else
                ret.hit_point = hit.point + offset_correction; 
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
        if (no_mana_cost)
            return true;

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
        if (invincible)
            return;
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
        {
            curr_health = 0;
            game_over_canvas.SetActive(true);
            move_controller.StopMovement();
            //GetComponent<CESceneLoader>().LoadMainMenu();
        }
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


    public string GetHealthNumbers()
    {
        return ((int)curr_health).ToString() + "/" + variables_stats[(int)StatId.MaxHealth].Buffed_value;
    }
    public string GetResourceNumbers()
    {
        return ((int)curr_resource).ToString() + "/" + variables_stats[(int)StatId.MaxResource].Buffed_value;
    }

    public void CharacterScreenStateSwap()
    {
        character_stats_canvas.SetActive(!character_stats_canvas.activeSelf);
    }
    public void InventoryScreenStateSwap()
    {
        inventory_canvas.SetActive(!inventory_canvas.activeSelf);
    }
}
