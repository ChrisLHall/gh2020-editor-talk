using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObstacle : MonoBehaviour
{
    public float spinSpeed = 100f; // This will make the Inspector value start at 100. But you can change it.
    

    void Update()
    {
        // Using Time.deltatime here means it will look smooth even if the framerate changes
        transform.Rotate(new Vector3(0f, Time.deltaTime * spinSpeed, 0f));
    }
}
