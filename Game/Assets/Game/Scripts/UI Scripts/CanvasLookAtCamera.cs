using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAtCamera : MonoBehaviour
{
    public Camera player_camera;

    private void Start()
    {
        player_camera = CharacterController.instance.pc_camera;
    }

    private void Update()
    {
        transform.LookAt(transform.position + player_camera.transform.rotation * Vector3.back,
            player_camera.transform.rotation * Vector3.up);
    }
}
