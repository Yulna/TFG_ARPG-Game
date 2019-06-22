using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialDisplayer : MonoBehaviour
{
    public TextMeshProUGUI[] tutorial_parts;
    public Image[] tutorial_panels;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i< tutorial_parts.Length; i++)
        {
            tutorial_parts[i].CrossFadeAlpha(0, 0, true);
            tutorial_parts[i].gameObject.SetActive(false);
            tutorial_panels[i].CrossFadeAlpha(0, 0, true);
        }
        index = 0;
        tutorial_parts[index].gameObject.SetActive(true);
        tutorial_parts[index].CrossFadeAlpha(1, 0, true);
        tutorial_panels[index].CrossFadeAlpha(1, 0, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (index >= tutorial_parts.Length)
            return;

        if(tutorial_parts[index].GetComponent<TutorialCondition>().ConditionComplete())
        {
            tutorial_parts[index].CrossFadeAlpha(0, 1, true);
            tutorial_panels[index].CrossFadeAlpha(0, 1, true);
            index++;
            if (index < tutorial_parts.Length)
            {
                tutorial_parts[index].gameObject.SetActive(true);
                tutorial_parts[index].CrossFadeAlpha(0, 0, true);
                tutorial_panels[index].CrossFadeAlpha(0, 0, true);
                StartCoroutine(ShowTutorial());
            }
            else
                Destroy(gameObject, 5);
        }

    }



    IEnumerator ShowTutorial()
    {
        yield return new WaitForSecondsRealtime(2);
        tutorial_parts[index].CrossFadeAlpha(1, 1, true);
        tutorial_panels[index].CrossFadeAlpha(1, 1, true);
    }

}
