using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipedItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Inventory pc_inventory;
    public EquipSlot slot_id;
    public Image slot_image;
    public GameObject item_info_display;


    // Start is called before the first frame update
    void Start()
    {
        if (slot_image == null)
        {
            Debug.LogError("Inventory display slot has no image assinged");
            return;
        }
        item_info_display.SetActive(false);
        slot_image.enabled = false;
        slot_image.sprite = pc_inventory.GetSpriteFromEquiped(slot_id);
        if (slot_image.sprite != null)
            slot_image.enabled = true;

    }  
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
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            //Unequip item
            pc_inventory.UnEquipItem(slot_id);
        }
    }

    public void UpdateInfo()
    {
        if(slot_image == null)
        {
            Debug.LogError("Inventory display slot has no image assinged");
            return;
        }
        slot_image.sprite = pc_inventory.GetSpriteFromEquiped(slot_id);
        if (slot_image.sprite != null)
            slot_image.enabled = true;
        else
            slot_image.enabled = false;
    }
}
