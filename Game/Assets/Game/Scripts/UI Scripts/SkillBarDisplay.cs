using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarDisplay : MonoBehaviour
{
    public SkillController skill_controller;
    public SkillButton button_id;
    public Image skill_icon_display;

    // Start is called before the first frame update
    void Start()
    {
       if(skill_controller == null)
        {
            Debug.LogError("No reference to skill controller");
            return;
        }

        UpdateDisplay();       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDisplay()
    {
        Sprite new_sprite = skill_controller.GetSpriteFromButton(button_id);
        if (new_sprite != null)
        {
            skill_icon_display.sprite = new_sprite;
            skill_icon_display.enabled = true;
        }
        else
            skill_icon_display.enabled = false;
    }
}
