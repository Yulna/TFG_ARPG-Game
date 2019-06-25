﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SkillBarInfoDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SkillButton button_id;
    public SkillController skill_controller;
    public GameObject skill_info_display;
    public TextMeshProUGUI skill_info_name;
    public TextMeshProUGUI skill_info_cost;
    public TextMeshProUGUI skill_info_description;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        skill_info_name.SetText(skill_controller.GetSkillNameFromSkillbar(button_id));
        skill_info_cost.SetText(skill_controller.GetSkillCostFromSkillbar(button_id));
        skill_info_description.SetText(skill_controller.GetSkillDescriptionFromSkillbar(button_id));
        skill_info_display.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        skill_info_display.SetActive(false);
    }

}