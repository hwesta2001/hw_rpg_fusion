using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedUpdateCounter : MonoBehaviour
{
    [SerializeField, Range(0.25f, 10)] float fps = 1;
    float lastFps;
    public float Fps
    {
        get => fps; set
        {
            fps = value;
            Time.fixedDeltaTime = 1 / fps;
        }
    }

    private void Start()
    {
        Time.fixedDeltaTime = 1 / fps; ;
    }

    private void Update()
    {
        if (lastFps == fps) return;
        lastFps = fps;
        Fps = lastFps;
    }
    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate Time: " + Time.fixedTime + " - delta Time: " + Time.fixedDeltaTime);
    }
}
