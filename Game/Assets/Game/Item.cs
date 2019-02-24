using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "MyGame/Item")]
public class Item : ScriptableObject
{
    public EquipSlot slot_id;


    public int value;
       

}

public enum EquipSlot { Head, Chest, Arms, Legs, Feet }