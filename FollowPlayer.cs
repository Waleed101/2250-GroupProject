using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gets it so the villagers always point towards the player
public class FollowPlayer : MonoBehaviour
{
    // Get reference to the player to maintain efficiency 
    private GameObject _player;

    private void Start() => _player = GameObject.FindGameObjectWithTag("Player");
    
    // Turn as needed
    void Update() => transform.LookAt(_player.transform);
}
