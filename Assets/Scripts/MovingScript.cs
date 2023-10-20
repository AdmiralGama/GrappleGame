using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class MovingScript : MonoBehaviour
{
    public Vector3[] points;
    public float speed = 2.0f;
    Transform trans;
    Vector3 nextPoint;

    bool waiting = false;
    double time = 0;

    // Start is called before the first frame update
    void Start()
    {
        trans = this.gameObject.GetComponent<Transform>();

        if (points.Length >= 2) {
            trans.position = points[0];
            nextPoint = points[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (nextPoint - trans.position).magnitude;

        // If reached destination, set next destination
        if (distance < 0.1f)
        {
            if (Array.IndexOf(points, nextPoint) == points.Length - 1)
            {
                nextPoint = points[0];
            }
            else
            {
                nextPoint = points[Array.IndexOf(points, nextPoint) + 1];
            }

            waiting = true;
        }
        // Waits for a moment after reaching its destination
        else if (waiting)
        {
            time += Time.deltaTime;

            if (time > 1.5)
            {
                waiting = false; time = 0;
            }
        }
        // Decelerate when close
        else if (distance < 0.5f)
        {
            this.GetComponent<Rigidbody>().velocity = 0.95f * this.GetComponent<Rigidbody>().velocity;
        }
        // Otherwise just keep moving towards the next point
        else
        {
            this.GetComponent<Rigidbody>().velocity = (nextPoint - trans.position).normalized * speed;
        }
    }
}
