using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is attached to each individual NPC that allows them to start a dialogue with the player once the player draws close to them
public class NPCTrigger : MonoBehaviour
{
    // Each NPC carries two variants of dialogue: one for when the player chooses a "liked" character during character customization 
    // and another for when the player chooses a "hated" character
    public Dialogue dialogue, hatedDialogue;
    public bool playerIsNear, isHatedIdentical = false;
    [SerializeField] private CharacterInfo _characterInfo;

    public void Start()
    {
        playerIsNear = false;
        // Sets the two dialogues to be the same if the NPC is neutral to both types of characters
        if (isHatedIdentical)
            hatedDialogue = dialogue;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && playerIsNear)
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }

    // Once the player triggers the NPC's collider, the dialogue is started
    public void OnTriggerEnter(Collider other)
    {
        if(_characterInfo.GetIfHatEnabled())
            FindObjectOfType<DialogueManager>().StartDialogue(hatedDialogue);
        else
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        playerIsNear = true;
    }

    // The dialogue ends once the player is out of the NPC's vicinity
    public void OnTriggerExit(Collider other)
    {
        FindObjectOfType<DialogueManager>().EndDialogue();
        playerIsNear = false;
    }

}
