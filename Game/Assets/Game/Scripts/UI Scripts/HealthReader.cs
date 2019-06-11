using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthReader : MonoBehaviour
{
    public Image health_bar;
    // Update is called once per frame
    void Update()
    {
        health_bar.fillAmount = CharacterController.instance.GetHealthPercentile();    
    }
}
