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

        if (CharacterController.instance != null)
            LoadUniqueItems();
    }



    public GameObject items_display_head;
    public GameObject items_display_chest;
    public GameObject items_display_arms;
    public GameObject items_display_legs;
    public GameObject items_display_feet;
    public GameObject items_display_weapon;

    public Sprite items_sprite_head;
    public Sprite items_sprite_chest;
    public Sprite items_sprite_arms;
    public Sprite items_sprite_legs;
    public Sprite items_sprite_feet;
    public Sprite items_sprite_weapon;

    public Item[] unique_items_table;

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
                i == (int)StatId.EarthRes || i == (int)StatId.AttackSpeed ||
                i == (int)StatId.WeaponDmg)
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
        EquipSlot rand_slot = (EquipSlot)Random.Range(0, (int)EquipSlot._numSlots);

        int rarity_dice = Random.Range(1,6);

        if (rarity_dice != (int)ItemRarity.Unique)
        {
            Item ret_item = new Item();
            ret_item.item_rarity = (ItemRarity)rarity_dice;
            ret_item.equip_slot_id = rand_slot;
            switch (ret_item.equip_slot_id)
            {
                case EquipSlot.Head:
                    ret_item.item_world_display = items_display_head;
                    ret_item.item_icon = items_sprite_head;
                    break;
                case EquipSlot.Chest:
                    ret_item.item_world_display = items_display_chest;
                    ret_item.item_icon = items_sprite_chest;
                    break;
                case EquipSlot.Arms:
                    ret_item.item_world_display = items_display_arms;
                    ret_item.item_icon = items_sprite_arms;
                    break;
                case EquipSlot.Legs:
                    ret_item.item_world_display = items_display_legs;
                    ret_item.item_icon = items_sprite_legs;
                    break;
                case EquipSlot.Feet:
                    ret_item.item_world_display = items_display_feet;
                    ret_item.item_icon = items_sprite_feet;
                    break;
                case EquipSlot.Weapon:
                    ret_item.item_world_display = items_display_weapon;
                    ret_item.item_icon = items_sprite_weapon;
                    break;
                default:
                    break;
            }

            ret_item.item_name = GenItemName((ItemRarity)rarity_dice, ret_item.equip_slot_id);
            ret_item.item_buffs = GetItemBuffs((ItemRarity)rarity_dice, ret_item.equip_slot_id);

            return ret_item;
        }
        else
        {
            Item ret_item = unique_items_table[Random.Range(0,10)];
            ret_item.item_rarity = ItemRarity.Unique;
            return ret_item;
        }
    }

    public Buff[] GetItemBuffs(ItemRarity rarity, EquipSlot slot_id)
    {
        if(slot_id == EquipSlot._numSlots)
        {
            Debug.LogError("Trying to generate an item without known slot");
            return new Buff[1];
        }     

        int buff_num = (int)rarity + 1; //+1 == base stat (armor for armor, damage for weapons)
        Buff[] ret = new Buff[buff_num];

        //Get corresponding loot table
        bool[] available_buffs;
        if (slot_id == EquipSlot.Weapon)
        {
            available_buffs = (bool[])weapon_stat_table.Clone();
            //add a base damage roll
            int damage_roll = Random.Range(20, 120);
            ret[0] = new Buff(BuffType.BUFF_STAT_ADD, damage_roll, CharacterController.instance.GetStat(StatId.WeaponDmg));
        }
        else //All other slots are amror slots
        {
            available_buffs = (bool[])armor_stat_table.Clone();
            //add a base armor roll
            int armor_roll = Random.Range(20, 80);
            ret[0] = new Buff(BuffType.BUFF_STAT_ADD, armor_roll, CharacterController.instance.GetStat(StatId.Armor));
        }

        int buff_dice = Random.Range(0, (int)StatId._numId);
        buff_dice = (int)StatId.AttackSpeed;
        for (int i = 1, loop_stoper = 0; i < buff_num;)
        {
            if (available_buffs[buff_dice])
            {
                available_buffs[buff_dice] = false;

                //Random with int the maxa is EXCLUSIVE
                int buff_type_dice = Random.Range(0, 2);

                
                ret[i] = new Buff((BuffType)buff_type_dice,
                    Random.Range(value_table[buff_dice, buff_type_dice, (int)BuffRange.MinRange], value_table[buff_dice, buff_type_dice, (int)BuffRange.MaxRange]),
                    CharacterController.instance.GetStat((StatId)buff_dice));

                buff_dice = Random.Range(0, (int)StatId._numId);
                i++;
                loop_stoper = 0;
                continue;
            }
            else
            {
                if (buff_dice >= (int)StatId._numId - 1)
                    buff_dice = 0;
                else
                    buff_dice++;
                loop_stoper++;
                if (loop_stoper < (int)StatId._numId)
                    continue;
                else
                    break;
            }
        }
        return ret;
    }

    public string GenItemName(ItemRarity rarity, EquipSlot slot_id)
    {
        string ret = "";

        switch (rarity)
        {
            case ItemRarity.Common:
                ret += "Common ";
                break;
            case ItemRarity.Uncommon:
                ret += "Uncommon ";
                break;
            case ItemRarity.Rare:
                ret += "Rare ";
                break;
            case ItemRarity.Epic:
                ret += "Epic ";
                break;
            case ItemRarity.Unique:
                break;
            default:
                break;
        }

        switch (slot_id)
        {
            case EquipSlot.Head:
                ret += "Helmet";
                break;
            case EquipSlot.Chest:
                ret += "Breastplate";
                break;
            case EquipSlot.Arms:
                ret += "Gloves";
                break;
            case EquipSlot.Legs:
                ret += "Pants";
                break;
            case EquipSlot.Feet:
                ret += "Boots";
                break;
            case EquipSlot.Weapon:
                ret += "Sword";
                break;
            default:
                break;
        }

        return ret;
    }

    public void LoadUniqueItems()
    {

        unique_items_table[0].item_buffs[unique_items_table[0].item_buffs.Length - 1] = new BuffTripleTornado();
        unique_items_table[1].item_buffs[unique_items_table[1].item_buffs.Length - 1] = new BuffBiggerAE();
        unique_items_table[2].item_buffs[unique_items_table[2].item_buffs.Length - 1] = new BuffTrueFreeze();
        unique_items_table[3].item_buffs[unique_items_table[3].item_buffs.Length - 1] = new BuffThunderGod();
        unique_items_table[4].item_buffs[unique_items_table[4].item_buffs.Length - 1] = new BuffVampireRefuge();
        unique_items_table[5].item_buffs[unique_items_table[5].item_buffs.Length - 1] = new BuffWindPower();
        unique_items_table[6].item_buffs[unique_items_table[6].item_buffs.Length - 1] = new BuffPiercingSting();
        unique_items_table[7].item_buffs[unique_items_table[7].item_buffs.Length - 1] = new BuffFourDragonFire();
        unique_items_table[8].item_buffs[unique_items_table[8].item_buffs.Length - 1] = new BuffCircleTornado();
    }

    public Item GetUniqueFromName(string unique_name)
    {
        for(int i=0; i < unique_items_table.Length; i++)
        {
            if (unique_items_table[i].item_name == unique_name)
                return unique_items_table[i];
        }
        return null;
    }

    public Item GetUniqueByID(int index)
    {
        return unique_items_table[index];
    }
}
