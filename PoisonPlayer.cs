using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPlayer : MonoBehaviour
{
    public GameObject player;
    // Keeping track of time to see when to dock off health next and how long the enemy has been attacking
    public float timeToNextKill = 3f, timeSinceLast = 0f, damageToGive = 1f;

    public bool inPlayerZone = false;

    // Update is called once per frame
    void Update()
    {
        // Make sure they don't do it too much and they're in range of the player
        if (((Time.time - timeSinceLast) > timeToNextKill) && inPlayerZone)
        {
            // Normal enemy takes off 1 health
            player.GetComponent<HealthManager>().TakeDamage(damageToGive);
            timeSinceLast = Time.time;
        }
    }

    public void SetInPlayerZone() { 
        inPlayerZone = true;
        this.GetComponent<Animator>().SetBool("Attacking", true);
    }

    public void SetOutPlayerZone() { 
        inPlayerZone = false;
        this.GetComponent<Animator>().SetBool("Attacking", false);
    }
}
