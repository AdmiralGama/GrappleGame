using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        // Destroys player on collision
        if (collider.gameObject.name == "Player")
        {
            Destroy(collider.gameObject);
        }
    }
}
