using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject elven_char;   //id=1
    public GameObject goblin_char;  //id=2

    public Camera main_camera;

    public void SpawnPlayer(int id)
    {
        switch(id)
        {
            case 1:
                Instantiate(elven_char);
                PlayerController.instance.camera = main_camera;
                break;
            case 2:
                Instantiate(goblin_char);
                PlayerController.instance.camera = main_camera;
                break;
            default:
                Debug.LogError("No valid character");
                break;
        }
    }

}
