using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public bool collected = false;

    void Update()
    {
        Transform parent = this.gameObject.transform.parent;
        parent.RotateAround(parent.position, Vector3.up, 45 * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        // If the object was the player
        if (collider.gameObject.name == "Player" && !collected)
        {
            collected = true;
            
            // Hide Key
            this.GetComponent<Renderer>().enabled = false;
            this.GetComponent<AudioSource>().Play();
        }
    }
}
