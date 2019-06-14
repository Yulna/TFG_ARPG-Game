using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName = "Chambers of Elrankat/Item")]
//Removed scriptable so items generated in runtime can be saved into the inventory
//Maybe: Check is scriptable can still be saved into asset or similar after script creation
[System.Serializable]
public class Item
{
    //If an item has no name (name == "") it will be considered null
    public Item(string name = "")
    {
        item_name = name;
    }

    public string item_name;
    public ItemRarity item_rarity;
    public Sprite item_icon;
    public GameObject item_world_display;                                        //TODO: Change to another place // somewhere scriptable?
    public EquipSlot equip_slot_id;

    public Buff[] item_buffs;

    public void ActivateBuffs()
    {
        for (int i = 0; i < item_buffs.Length; i++)
        {
            item_buffs[i].EnableBuff();
        }
    }

    public void DeactivateBuffs()
    {
        for (int i = 0; i < item_buffs.Length; i++)
        {
            item_buffs[i].DisableBuff();
        }
    }

    //TODO: Item Display GO factory?¿

    public string GetItemDescription()
    {
        string ret = "";


        for (int i = 0; i < item_buffs.Length; i++)
        {
            ret += item_buffs[i].GetBuffDescription() + "\n";       
        }

        return ret;
    }

}

