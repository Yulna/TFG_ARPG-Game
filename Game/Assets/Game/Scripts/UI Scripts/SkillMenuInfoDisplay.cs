using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SkillMenuInfoDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int skill_list_num;
    public SkillController skill_controller;
    public GameObject skill_info_display;
    public TextMeshProUGUI skill_info_name;
    public TextMeshProUGUI skill_info_cost;
    public TextMeshProUGUI skill_info_description;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        skill_info_name.SetText(skill_controller.GetSkillNameFromSkillList(skill_list_num));
        skill_info_cost.SetText(skill_controller.GetSkillCostFromSkillList(skill_list_num));
        skill_info_description.SetText(skill_controller.GetSkillDescriptionFromSkillList(skill_list_num));
        skill_info_display.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        skill_info_display.SetActive(false);
    }

}
