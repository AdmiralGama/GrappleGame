using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    GameObject platform;

    // Start is called before the first frame update
    void Start()
    {
        platform = this.gameObject.transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider collider)
    {
        // Disable collider to let player pass through
        if (collider.gameObject.name == "Player")
        {
            platform.GetComponent<Collider>().enabled = false;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        // Re-enable collider once player is through the platform
        if (collider.gameObject.name == "Player")
        {
            platform.GetComponent<Collider>().enabled = true;
        }
    }
}
