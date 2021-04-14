using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Method that despawns enemy when they go back to spawn point
public class DespawnEnemy : MonoBehaviour
{
    // Check to see if they're colliding
    public void OnCollisionEnter(Collision collision)
    {
        // Check to make sure enemy and that it has visited the player
        if (collision.gameObject.tag == "Enemy")
            if (collision.gameObject.GetComponent<Enemy>().GetIfEnteredZone())
                Destroy(collision.gameObject); // Destroys enemy
    }

    // Identical method for trigger to eliminate any bugs
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
            if (other.gameObject.GetComponent<Enemy>().GetIfEnteredZone())
                Destroy(other.gameObject);
    }

}
