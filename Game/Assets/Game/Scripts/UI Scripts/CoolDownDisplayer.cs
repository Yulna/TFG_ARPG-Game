using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownDisplayer : MonoBehaviour
{
    public SkillController skill_controller;
    public SkillButton button_id;
    public Image skill_cd_display;

    // Start is called before the first frame update
    void Start()
    {
        if (skill_controller == null)
        {
            Debug.LogError("No reference to skill controller");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        skill_cd_display.fillAmount = skill_controller.GetSkillCDDisplayPercentile(button_id);
    }
}
