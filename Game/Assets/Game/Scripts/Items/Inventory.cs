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
    _numSlots
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

    private void OnEnable()
    {
        for (int i = 0; i < equiped_items.Length; i++)
        {
            equiped_items[i].ActivateBuffs();
        }
        item_change.Invoke();
    }

    private void OnDisable()
    {
        for (int i = 0; i < equiped_items.Length; i++)
        {
            equiped_items[i].DeactivateBuffs();
        }
    }

    public bool AddItem (Item new_item)
    {        
        if (items.Count < max_space)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].item_name == null || items[i].item_name == "")
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

    public bool RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
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

         if(equiped_items[(int)slot_to_equip].item_name == "" || equiped_items[(int)slot_to_equip].item_name == null)
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
            if (items[i].item_name == "" || items[i].item_name == null)
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
        if (index < max_space && items[index] != null && items[index].item_name != null && items[index].item_name != "")
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
}
