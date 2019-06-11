using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceReader : MonoBehaviour
{
    public Image resource_bar;
    // Update is called once per frame
    void Update()
    {
        resource_bar.fillAmount = CharacterController.instance.GetResourcePercentile();
    }
}
