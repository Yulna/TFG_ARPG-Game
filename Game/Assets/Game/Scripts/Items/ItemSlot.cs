﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Inventory pc_inventory;
    public int item_index;
    public Image slot_image;
    public GameObject item_info_display;
    //public GameObject equiped_data;

    private void Start()
    {
        slot_image.enabled = false;
        item_info_display.SetActive(false);       
        slot_image.sprite = pc_inventory.GetSpriteFromIndex(item_index);
        if (slot_image.sprite != null)
            slot_image.enabled = true;     
    }

    //TODO: Make comparasion with equiped items
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        item_info_display.SetActive(true);
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        item_info_display.SetActive(false);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(pointerEventData.button == PointerEventData.InputButton.Right)
        {
            pc_inventory.EquipItem(item_index);
        }
    }

    public void UpdateInfo()
    {
        if (slot_image == null)
        {
            Debug.LogError("Inventory display slot has no image assinged");
            return;
        }
        Debug.Log("Updating item UI");
        slot_image.sprite = pc_inventory.GetSpriteFromIndex(item_index);
        if (slot_image.sprite != null)
            slot_image.enabled = true;
        else
            slot_image.enabled = false;
    }

}