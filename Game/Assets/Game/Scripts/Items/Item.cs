using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName = "Chambers of Elrankat/Item")]
//Removed scriptable so items generated in runtime can be saved into the inventory
//Maybe: Check is scriptable can still be saved into asset or similar after script creation
[System.Serializable]
public class Item
{
    public Item()
    {
        item_name = "no_name";
    }

    public string item_name;
    public Sprite item_icon;
    public GameObject item_world_display;
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

}

