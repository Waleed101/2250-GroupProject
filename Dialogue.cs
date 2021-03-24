using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// This class is used as a non-primitive data type to store dialogue data of NPCs found throughout the game
public class Dialogue
{
    // The name of the NPC
    public string name;

    // This array os sentences represent the whole dialogue that the NPC exhibits during gameplay
    [TextArea(3,10)]
    public string[] sentences;
    
}
