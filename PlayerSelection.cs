using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that manages turning the hat on and off
public class PlayerSelection : MonoBehaviour
{
    // Reference to the character script
    [SerializeField] private CharacterInfo _characterRef;

    // Turn on/off hat based on the character selection (could add other things later)
    void Start() => GameObject.FindGameObjectWithTag("Hat").SetActive(_characterRef.GetIfHatEnabled());
}
