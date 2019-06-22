using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public int frames;

    int framecount = 0;
    int dt = 0;

    // Update is called once per frame
    void Update()
    {
        frames = (int)(1f / Time.deltaTime);
    }
}
