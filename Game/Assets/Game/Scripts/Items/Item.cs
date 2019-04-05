using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Chambers of Elrankat/Item")]
public class Item : ScriptableObject
{
    public EquipSlot slot_id;


}

public enum EquipSlot { Head, Chest, Arms, Legs, Feet }