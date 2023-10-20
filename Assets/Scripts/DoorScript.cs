using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public string color = "Red";
    GameObject door;
    KeyScript key;

    void Start()
    {
        // Gets the key for the door by name
        key = GameObject.Find(color + " Key").GetComponent<KeyScript>();
        door = this.gameObject.transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            // If the correct key has been collected, hide the door and play a sound
            if (key.collected == true)
            {
                door.GetComponent<Collider>().enabled = false;
                door.GetComponent<Renderer>().enabled = false;
                this.GetComponent<AudioSource>().Play();
                this.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
