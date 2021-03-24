using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that tracks if an enemy has entered the player bounds
public class EnemyEnter : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")

            // If they do, enable fighting mode for the enemy
            collision.gameObject.GetComponent<Enemy>().SetInPlayerZone();
    }
}
