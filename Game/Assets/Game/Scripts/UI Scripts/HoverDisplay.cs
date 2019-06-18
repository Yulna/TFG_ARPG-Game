using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject to_display;


    private void Start()
    {
        to_display.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        to_display.SetActive(true);
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        to_display.SetActive(false);
    }
}
