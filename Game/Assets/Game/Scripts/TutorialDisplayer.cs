using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialDisplayer : MonoBehaviour
{
    public GameObject move_control_text;
    public GameObject items_text;
    public GameObject character_text;
    public GameObject skills_text;
    public GameObject looting_enemies_text;

    public GameObject enemy;

    public float help_timer;



    // Start is called before the first frame update
    void Start()
    {
        move_control_text.SetActive(true);
        items_text.SetActive(false);
        character_text.SetActive(false);
        skills_text.SetActive(false);
        looting_enemies_text.SetActive(false);

        items_text.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0, 0, true);
    }

    // Update is called once per frame
    void Update()
    {
        help_timer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && 
            move_control_text.activeSelf &&
            !items_text.activeSelf &&
            !character_text.activeSelf &&
            !skills_text.activeSelf &&
            !looting_enemies_text.activeSelf)
        {
            move_control_text.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0, 1, true);
            items_text.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1, 1, true);
            move_control_text.SetActive(false);
            items_text.SetActive(true);
        }
        

        if (Input.GetKeyDown(KeyCode.I) &&
            !move_control_text.activeSelf &&
            items_text.activeSelf &&
            !character_text.activeSelf &&
            !skills_text.activeSelf &&
            !looting_enemies_text.activeSelf)
        {
            items_text.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0, 1, true);
            items_text.SetActive(false);
            character_text.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.C) &&
            !move_control_text.activeSelf &&
            !items_text.activeSelf &&
            character_text.activeSelf &&
            !skills_text.activeSelf &&
            !looting_enemies_text.activeSelf)
        {
            character_text.SetActive(false);
            skills_text.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.S) &&
            !move_control_text.activeSelf &&
            !items_text.activeSelf &&
            !character_text.activeSelf &&
            skills_text.activeSelf &&
            !looting_enemies_text.activeSelf)
        {
            skills_text.SetActive(false);
            looting_enemies_text.SetActive(true);
        }

        if (enemy == null &&
            !move_control_text.activeSelf &&
            !items_text.activeSelf &&
            !character_text.activeSelf &&
            !skills_text.activeSelf &&
            looting_enemies_text.activeSelf)
        {
            looting_enemies_text.SetActive(false);
        }


        if (!move_control_text.activeSelf && !items_text.activeSelf && !character_text.activeSelf && !skills_text.activeSelf && !looting_enemies_text.activeSelf)
            Destroy(gameObject);
    }



}
