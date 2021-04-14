using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // UI objects used to display the dialogue content during gameplay
    public GameObject dialogueCanvas;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public bool tutorialTrigger;

    // A queue that stores sentences to be displayed in the dialogue box
    private Queue<string> _sentences;

    // Start is called before the first frame update
    void Start()
    {
        // At the start of game, the dialogue box is hidden
        dialogueCanvas.SetActive(false);
    }

    // This method is called whenever the player becomes near an NPC to trigger a dialogue
   public void StartDialogue(Dialogue dialogue, bool task)
    {
        // UI is initiated
        dialogueCanvas.SetActive(true);
        nameText.text = dialogue.name;
        _sentences = new Queue<string>();

        // Engueues the dialogue of the NPC that the player is in near proximity of
        foreach (string sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }

        // Calls for the first sentence to be displayed
        DisplayNextSentence(task);
    }

    // This method displays sentences one-by-one in the dialogue box
    public void DisplayNextSentence(bool task)
    {
        // If there are no more sentnences left in the queue, the dialogue is terminated
        if (_sentences.Count == 0)
        {
            EndDialogue();
            if(task)
                GameObject.FindGameObjectWithTag("GameController").GetComponent<DayLevelManager>().TaskCompleted();
            return;
        }

        string sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    // This method enables the dialogue to be typed out letter-by-letter for a more personable interaction with NPCs
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    // This method is called when the dialogue ends, hiding the dialogue UI
    public void EndDialogue()
    {
        dialogueCanvas.SetActive(false);
        tutorialTrigger = true;
    }
}
