using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EquipSlot
{
    Head = 0,
    Chest,
    Arms,
    Legs,
    Feet,
    Weapon,
    _numSlots
}

//Rarity holds the value of the buff the rarity receives
public enum ItemRarity
{
    Undefined = 0,
    Common = 1,
    Uncommon = 2,
    Rare = 3,
    Epic = 4,
    Unique = 5 
}

[CreateAssetMenu(fileName = "New Item", menuName = "Chambers of Elrankat/Inventory")]
[System.Serializable]
public class Inventory : ScriptableObject
{
    public static int max_space = 40;
    public List<Item> items = new List<Item>();
    //public Item[] items = new Item[max_space];

    public Item[] equiped_items = new Item[(int)EquipSlot._numSlots];

    public UnityEvent item_change;

    private void OnDisable()
    {
        for (int i = 0; i < equiped_items.Length; i++)
        {
            equiped_items[i].DeactivateBuffs();
        }
    }

    public void ActivateEquipBuffs()
    {
        for (int i = 0; i < equiped_items.Length; i++)
        {
            equiped_items[i].ActivateBuffs();
        }
        item_change.Invoke();
    }

    public bool AddItem (Item new_item)
    {        
        if (items.Count < max_space)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == null || items[i].item_name == null || items[i].item_name == "")
                {
                    items[i] = new_item;
                    item_change.Invoke();
                    return true;
                }
            }
            items.Add(new_item);
            item_change.Invoke();
            return true;
        }
        else
        {
            Debug.Log("Invetory full");
            return false;
        }
    }

    public bool RemoveItem(int item_index)
    {
        if(item_index > items.Count)
            return false;
        

        if (items[item_index] != null || items[item_index].item_name != null || items[item_index].item_name != "")
        {
            Item to_drop = items[item_index];
            GameObject to_drop_world= Instantiate(to_drop.item_world_display, CharacterController.instance.GetPlayerTransform().position, Quaternion.identity);
            to_drop_world.GetComponent<ItemWorld>().item_data = to_drop;

            //items.Remove(items[item_index]);
            items[item_index] = null;

            item_change.Invoke();

            return true;
        }
        else
        {
            Debug.LogWarning("Item is not in inventory");
            return false;
        }  
    }

    public void EquipItem(int index)
    {
        if (items.Count < index || items[index].item_name == null || items[index].item_name == "")
            return;


        Debug.Log("item equiped");
        EquipSlot slot_to_equip = items[index].equip_slot_id;

         if(equiped_items[(int)slot_to_equip] == null || equiped_items[(int)slot_to_equip].item_name == "" || equiped_items[(int)slot_to_equip].item_name == null)
         {
            equiped_items[(int)slot_to_equip] = items[index];
            items[index] = null;
         }
         else
         {
            Item temp = equiped_items[(int)slot_to_equip];
            equiped_items[(int)slot_to_equip] = items[index];
            temp.DeactivateBuffs();
            items[index] = temp;
         }
        equiped_items[(int)slot_to_equip].ActivateBuffs();
        item_change.Invoke();
        return;
    }

    public void UnEquipItem(EquipSlot slot_id)
    {
        if (equiped_items[(int)slot_id].item_name == "" || equiped_items[(int)slot_id].item_name == null)
        {
            Debug.Log("No item equiped");
            return;
        }

        //Iterate item list and replace the fisrt void item with the current equiped one
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null || items[i].item_name == "" || items[i].item_name == null)
            {
                equiped_items[(int)slot_id].DeactivateBuffs();
                items[i] = equiped_items[(int)slot_id];
                equiped_items[(int)slot_id] = null;
                item_change.Invoke();
                return;
            }
        }      
        
        Debug.Log("Not enough space in the inventory");      
        return;
    }

    public Sprite GetSpriteFromIndex(int index)
    {
        if (index < items.Count && index < max_space && items[index] != null && items[index].item_name != null && items[index].item_name != "")
            return items[index].item_icon;
        else
            return null;
    }
    public Sprite GetSpriteFromEquiped(EquipSlot slot_id)
    {
        if (equiped_items[(int)slot_id] != null && equiped_items[(int)slot_id].item_name != null && equiped_items[(int)slot_id].item_name != "")
            return equiped_items[(int)slot_id].item_icon;
        else
            return null;
    }

    public string GetDescriptionFromIndex(int index)
    {
        if (index < items.Count && index < max_space && items[index] != null && items[index].item_name != null && items[index].item_name != "")
            return items[index].GetItemDescription();
        else
            return null;
    }
    public string GetDescriptionFromSlot(EquipSlot slot_id)
    {
        if ((int)slot_id < (int)EquipSlot._numSlots && equiped_items[(int)slot_id] != null && equiped_items[(int)slot_id].item_name != null && equiped_items[(int)slot_id].item_name != "")
            return equiped_items[(int)slot_id].GetItemDescription();
        else
            return null;
    }

    public string GetNameFromIndex(int index)
    {
        if (index < items.Count && index < max_space && items[index] != null && items[index].item_name != null && items[index].item_name != "")
            return items[index].item_name;
        else
            return null;
    }
    public string GetNameFromSlot(EquipSlot slot_id)
    {
        if ((int)slot_id < (int)EquipSlot._numSlots && equiped_items[(int)slot_id] != null && equiped_items[(int)slot_id].item_name != null && equiped_items[(int)slot_id].item_name != "")
            return equiped_items[(int)slot_id].item_name;
        else
            return null;
    }
}
