using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float speed = 10.0f;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 velocity = Vector3.zero;

            // Adjusts for vertical bounds
            if (this.GetComponent<Transform>().position.y - player.GetComponent<Transform>().position.y < 5)
            {
                velocity.y = 0.25f;
            }
            else if (this.GetComponent<Transform>().position.y - player.GetComponent<Transform>().position.y > 7)
            {
                velocity.y = -0.25f;
            }

            // Adjusts for horizontal bounds
            if (player.GetComponent<Transform>().position.x - this.GetComponent<Transform>().position.x > 2.0f)
            {
                velocity.x = 1;
            }
            else if (player.GetComponent<Transform>().position.x - this.GetComponent<Transform>().position.x < -2.0f)
            {
                velocity.x = -1;
            }

            // Applies calculated velocity
            this.GetComponent<Rigidbody>().velocity = speed * velocity.normalized;
        }
    }
}
