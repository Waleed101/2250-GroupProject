using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is attached to each individual NPC that allows them to start a dialogue with the player once the player draws close to them
public class NPCTrigger : MonoBehaviour
{
    // Each NPC carries two variants of dialogue: one for when the player chooses a "liked" character during character customization 
    // and another for when the player chooses a "hated" character
    public Dialogue currentDialogue, currentHatedDialogue;
    public Dialogue[] dialogues, hatedDialogues;
    public bool playerIsNear, isHatedIdentical = false, advanceLevel = false;
    [SerializeField] private CharacterInfo _characterInfo;

    public void Start()
    {
        // Sets the two dialogues to be the same if the NPC is neutral to both types of characters
        if (isHatedIdentical)
            hatedDialogues = dialogues;
        currentDialogue = dialogues[0];
        currentHatedDialogue = hatedDialogues[0];
        playerIsNear = false;
    }

    private void Update()
    {
        // The next sentence is displayed once "T" is pressed
        if (Input.GetKeyDown(KeyCode.T) && playerIsNear)
            FindObjectOfType<DialogueManager>().DisplayNextSentence(advanceLevel);
    }

    // Once the player triggers the NPC's collider, the dialogue is started
    public void OnTriggerEnter(Collider other)
    {
        if(_characterInfo.GetIfHatEnabled())
            FindObjectOfType<DialogueManager>().StartDialogue(currentHatedDialogue, advanceLevel);
        else
            FindObjectOfType<DialogueManager>().StartDialogue(currentDialogue, advanceLevel);
        playerIsNear = true;
    }

    // The dialogue ends once the player is out of the NPC's vicinity
    public void OnTriggerExit(Collider other)
    {
        FindObjectOfType<DialogueManager>().EndDialogue();
        playerIsNear = false;
    }
    
    // This method is called once the next day level starts (level is divided by 2 to reflect that day levels happen every other level)
    public void nextDialogueSet(int level)
    {
        // The current dialogues shifts to the next set of dialogues for the next level
        currentDialogue = dialogues[level/2];
        currentHatedDialogue = hatedDialogues[level/2];
    }
}
