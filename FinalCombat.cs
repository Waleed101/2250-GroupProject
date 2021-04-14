using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that handles the final fighter game mechanics
public class FinalCombat : MonoBehaviour
{
    // When something enters the players region
    public void OnTriggerEnter(Collider collision)
    {
        // If its the boss, then tell the pig boss that its in the player region and to attack
        if (collision.gameObject.tag == "Final Fighter")
            collision.gameObject.GetComponent<PigBoss>().EnteredPlayer();
    }

    // When sometuing exits the players region
    public void OnTriggerExit(Collider collision)
    {
        // If its the final fighter
        if (collision.gameObject.tag == "Final Fighter")
            // Disable fighting mode for the enemy
            collision.gameObject.GetComponent<PigBoss>().ExitedPlayer();
    }
}
