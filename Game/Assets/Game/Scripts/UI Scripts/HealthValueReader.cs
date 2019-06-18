using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthValueReader : MonoBehaviour
{
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.SetText(CharacterController.instance.GetHealthNumbers());
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText(CharacterController.instance.GetHealthNumbers());
    }
}
