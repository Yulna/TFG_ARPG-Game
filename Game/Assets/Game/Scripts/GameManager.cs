using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffRange
{
    MinRange = 0,
    MaxRange = 1
}

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("More than one game manager created");
    }


    public GameObject items_display_head;
    public GameObject items_display_chest;
    public GameObject items_display_arms;
    public GameObject items_display_legs;
    public GameObject items_display_feet;
    public GameObject items_display_weapon;

    [SerializeField]
    bool[] armor_stat_table;
    [SerializeField]
    bool[] weapon_stat_table;

    public float[,,] value_table;

    public float[] add_min_roll;
    public float[] add_max_roll;
    public float[] mult_min_roll;
    public float[] mult_max_roll;


    private void Start()
    {
        armor_stat_table = new bool[(int)StatId._numId];
        weapon_stat_table = new bool[(int)StatId._numId];

        for (int i = 0; i < (int)StatId._numId; i++)
        {
            armor_stat_table[i] = false;
            weapon_stat_table[i] = false;

            //Armor valid buffs
            if(i == (int)StatId.MaxHealth || i == (int)StatId.HealthRegen ||
                i == (int)StatId.MaxResource || i == (int)StatId.ResourceRegen ||
                i == (int)StatId.MoveSpeed || i == (int)StatId.Armor ||
                i == (int)StatId.PhysicRes || i == (int)StatId.FireRes ||
                i == (int)StatId.WaterRes || i == (int)StatId.ShockRes ||
                i == (int)StatId.EarthRes)
            {
                armor_stat_table[i] = true;
            }

            //Weapon valid buffs
            if (i == (int)StatId.WeaponDmg || i == (int)StatId.AttackSpeed ||
                i == (int)StatId.MaxHealth || i == (int)StatId.MaxResource ||
                i == (int)StatId.HealthRegen || i == (int)StatId.ResourceRegen)
            {
                weapon_stat_table[i] = true;
            }
        }

        //buff value table
        //For each stat have a min max value for addtion and multip
        value_table = new float[(int)StatId._numId, 2, 2];

        for (int i = 0; i < (int)StatId._numId; i++)
        {
            value_table[i, (int)BuffType.BUFF_STAT_ADD, (int)BuffRange.MinRange] = add_min_roll[i];
            value_table[i, (int)BuffType.BUFF_STAT_ADD, (int)BuffRange.MaxRange] = add_max_roll[i];
            value_table[i, (int)BuffType.BUFF_STAT_MULT, (int)BuffRange.MinRange] = mult_min_roll[i];
            value_table[i, (int)BuffType.BUFF_STAT_MULT, (int)BuffRange.MaxRange] = mult_max_roll[i];
        }

    }

    public Item GetRandomLoot()
    {        
        EquipSlot rand_slot = (EquipSlot)Random.Range(0, (int)EquipSlot._numSlots - 1);

        int rarity_dice = Random.Range(0,5);

        Item ret_item = new Item();
        ret_item.equip_slot_id = rand_slot;
        ret_item.item_world_display = items_display_legs;
        ret_item.item_name = "test item";

        ret_item.item_buffs = GetBuffsFromSlot(ret_item.equip_slot_id ,ItemRarity.Epic);

        return ret_item;
    }


    public Buff[] GetBuffsFromSlot(EquipSlot slot_id, ItemRarity rarity)
    {
        switch(slot_id)
        {
            case EquipSlot.Head:
                return GetArmorBuffs(rarity);
                break;
            case EquipSlot.Chest:
                return GetArmorBuffs(rarity);
                break;
            case EquipSlot.Arms:
                return GetArmorBuffs(rarity);
                break;
            case EquipSlot.Legs:
                return GetArmorBuffs(rarity);
                break;
            case EquipSlot.Feet:
                 return GetArmorBuffs(rarity);
                break;
            case EquipSlot.Weapon:
                return GetArmorBuffs(rarity);
                break;
            default:
                return new Buff[1];
                break;
        }

    }
/*
    public Buff[] GetItemBuffs(ItemRarity rarity, EquipSlot slot_id)
    {
        if(slot_id == EquipSlot._numSlots)
        {
            Debug.LogError("Trying to generate an item without known slot");
            return new Buff[1];
        }

        //Get corresponding loot table
        bool[] available_buffs;
        if (slot_id == EquipSlot.Weapon)
        {
            available_buffs = (bool[])weapon_stat_table.Clone();
        }
        else //All other slots are amror slots
        {
            available_buffs = (bool[])armor_stat_table.Clone();
        }




    }*/

    public Buff[] GetArmorBuffs(ItemRarity rarity)
    {   

        bool[] available_buffs = (bool[])armor_stat_table.Clone(); 
        int buff_num = (int)rarity + 1; //+1 == base armor
        Buff[] ret = new Buff[buff_num];
  

        //add a base armor roll
        int armor_roll = Random.Range(20, 80);
        ret[0] = new Buff(BuffType.BUFF_STAT_ADD, armor_roll, CharacterController.instance.GetStat(StatId.Armor));


        int buff_dice = Random.Range(0, (int)StatId._numId - 1);
        for (int i = 1; i < buff_num;)
        {            
            if(available_buffs[buff_dice])
            {
                available_buffs[buff_dice] = false;

                //Random with int the maxa is EXCLUSIVE
                int buff_type_dice = Random.Range(0, 2);

                //TODO randomize add and mult
                ret[i] = new Buff((BuffType)buff_type_dice, 
                    Random.Range(value_table[buff_dice,buff_type_dice,(int)BuffRange.MinRange], value_table[buff_dice, buff_type_dice, (int)BuffRange.MaxRange]),
                    CharacterController.instance.GetStat((StatId)buff_dice));

                buff_dice = Random.Range(0, (int)StatId._numId);
                i++;
                continue;
            }
            else
            {
                if (buff_dice >= (int)StatId._numId - 1)
                    buff_dice = 0;
                else
                    buff_dice++;
                continue;
            }
        }
        
        return ret;
    }

}
