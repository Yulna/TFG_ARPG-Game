using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Inventory pc_inventory;
    public int item_index;
    public Image slot_image;
    public GameObject item_info_display;
    public TextMeshProUGUI item_info_name;
    public TextMeshProUGUI item_info_description;

    public GameObject equip_item_info_display;
    public TextMeshProUGUI equip_item_info_name;
    public TextMeshProUGUI equip_item_info_description;
    //public GameObject equiped_data;

    private void Start()
    {
        slot_image.enabled = false;
        item_info_display.SetActive(false);       
        slot_image.sprite = pc_inventory.GetSpriteFromIndex(item_index);
        if (slot_image.sprite != null)
            slot_image.enabled = true;
        UpdateInfo();
    }

    //TODO: Make comparasion with equiped items
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (slot_image.IsActive())
        {
            item_info_description.SetText(pc_inventory.GetDescriptionFromIndex(item_index));
            item_info_name.SetText(pc_inventory.GetNameFromIndex(item_index));
            item_info_display.SetActive(true);

            
            
            equip_item_info_description.SetText(pc_inventory.GetDescriptionFromSlot(pc_inventory.GetItemSlot(item_index)));
            equip_item_info_name.SetText(pc_inventory.GetNameFromSlot(pc_inventory.GetItemSlot(item_index)));
            if (equip_item_info_name.text != null )
                equip_item_info_display.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        item_info_display.SetActive(false);
        equip_item_info_display.SetActive(false);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(pointerEventData.button == PointerEventData.InputButton.Right)
        {
            pc_inventory.EquipItem(item_index);
            item_info_display.SetActive(false);
        }

        if (pointerEventData.button == PointerEventData.InputButton.Left && Input.GetKey(KeyCode.LeftControl))
        {
            pc_inventory.RemoveItem(item_index);
            item_info_display.SetActive(false);
        }
    }

    public void UpdateInfo()
    {
        if (slot_image == null)
        {
            Debug.LogError("Inventory display slot has no image assinged");
            return;
        }
        slot_image.sprite = pc_inventory.GetSpriteFromIndex(item_index);
        if (slot_image.sprite != null)
            slot_image.enabled = true;
        else
            slot_image.enabled = false;

        //item_info_description.SetText(pc_inventory.GetDescriptionFromIndex(item_index));
        //item_info_name.SetText(pc_inventory.GetNameFromIndex(item_index));
    }

}
