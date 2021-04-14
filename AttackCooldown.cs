using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to manage the attack cooldown between hits
public class AttackCooldown : MonoBehaviour
{
    // Track most recent attack
    public float lastAttack = 0f, timeBetween = 0.5f;
    
    // Reset cooldown
    public void ResetAttack() { lastAttack = Time.time; }

    // Get whether cooldown has expired
    public bool GetIfCanAttack() {
        return ((Time.time - lastAttack) > timeBetween); 
    }
}
