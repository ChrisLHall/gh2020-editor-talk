using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForthObstacle : MonoBehaviour
{
    public GameObject startPosition;
    public GameObject endPosition;
    public float speed = 10f; // Inspector variable that starts at 10

    private bool movingTowardsEnd = true; // which way am I moving

    void Start()
    {
        // make sure start position and end position are both set!
        if (startPosition == null || endPosition == null)
        {
            Debug.LogError("BackAndForthObstacle needs a start position and an end position!");
        }
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime; // multiply by deltaTime to be remain smooth if framerate changes
        if (movingTowardsEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition.transform.position, moveDistance);
            if (transform.position == endPosition.transform.position)
            {
                // made it to the end, turn around
                movingTowardsEnd = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition.transform.position, moveDistance);
            if (transform.position == startPosition.transform.position)
            {
                // made it back to the start, turn around
                movingTowardsEnd = true;
            }
        }
    }

    // This version of Gizmos only appears when this object is selected in the inspector
    private void OnDrawGizmosSelected()
    {
        if (startPosition != null && endPosition != null)
        {
            Gizmos.color = Color.green;
            // draw small cubes at each side
            Gizmos.DrawCube(startPosition.transform.position, Vector3.one * 0.5f);
            Gizmos.DrawCube(endPosition.transform.position, Vector3.one * 0.5f);
            // draw a line between
            Gizmos.DrawLine(startPosition.transform.position, endPosition.transform.position);
        }
    }
}
