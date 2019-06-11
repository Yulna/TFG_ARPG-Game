using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum StatDisplayMode
{
    OnlyNum,
    OnlyName,
    NameNum
}


public class StatReader : MonoBehaviour
{
    public StatDisplayMode display_mode;
    public StatVariable stat_variable;
    public TextMeshProUGUI stat_display;

    // Start is called before the first frame update
    void Start()
    {
        UpdateStatText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStatText()
    {
        switch (display_mode)
        {
            case StatDisplayMode.OnlyNum:
                stat_display.SetText(stat_variable.Buffed_value.ToString());
                break;
            case StatDisplayMode.OnlyName:
                stat_display.SetText(stat_variable.name);
                break;
            case StatDisplayMode.NameNum:
                stat_display.SetText(stat_variable.name + "\t" + stat_variable.Buffed_value.ToString());
                break;
            default:
                break;
        }
    }
}
