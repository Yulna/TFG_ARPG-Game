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
public class Inventory : ScriptableObject
{

    public static int max_space = 40;
    public List<Item> items = new List<Item>();
    public Item[] items_array = new Item[max_space];

    //TODO: other list for equiped items
    public Item[] equiped_items = new Item[(int)EquipSlot._numSlots];

    public UnityEvent item_change;

    private void OnEnable()
    {
        for (int i = 0; i < equiped_items.Length; i++)
        {
            equiped_items[i].ActivateBuffs();
        }
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
        //for(int i = 0; i < max_space; i++)
        //{
        //    if(items_array[i] == null)
        //    {
        //        items_array[i] = new_item;
        //        return true;
        //    }
        //}
        //Debug.Log("Inventory full");
        //return false;

        if (items.Count < max_space)
        {
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
        //for (int i = 0; i < max_space; i++)
        //{
        //    if (items_array[i] == item)
        //    {
        //        items_array[i] = null;
        //        return true;
        //    }
        //}
        //Debug.Log("Item not in inventory");
        //return false;


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
        Debug.Log("item equiped");
        if (items.Count < index)
            return;

        EquipSlot slot_to_equip = items[index].equip_slot_id;

         if(equiped_items[(int)slot_to_equip].item_name == "")
         {
            equiped_items[(int)slot_to_equip] = items[index];
            items[index] = null;
            item_change.Invoke();
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

    public Sprite GetSpriteFromIndex(int index)
    {
        if (items[index] != null)
            return items[index].item_icon;
        else
            return null;
    }
}
