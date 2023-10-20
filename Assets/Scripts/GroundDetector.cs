using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        // If it the thing it hit was something other than the player, grapple line, or anything not solid
        if (collider.gameObject.name != "Player" && collider.gameObject.name != "Grapple" && !collider.isTrigger)
        {
            this.GetComponent<AudioSource>().Play();
        }
    }
}
