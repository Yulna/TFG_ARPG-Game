using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SkillInfoDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SkillData skill_data;
    public GameObject skill_info_display;
    public TextMeshProUGUI skill_info_name;
    public TextMeshProUGUI skill_info_cost;
    public TextMeshProUGUI skill_info_description;


    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        skill_info_name.SetText(skill_data.GetName());
        skill_info_cost.SetText(skill_data.GetCostString());
        skill_info_description.SetText(skill_data.GetDescription());
        skill_info_display.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        skill_info_display.SetActive(false);
    }

}
