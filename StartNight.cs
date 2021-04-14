using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages when the night should start (i.e., when the user finishes the day tasks)
public class StartNight : MonoBehaviour
{
    // Reference to the prompt to go to the next level
    public GameObject nextLevelPrompt;

    private void Update()
    {
        // Check to see if they're in the right region and press N
        if (nextLevelPrompt.activeSelf && Input.GetKeyDown(KeyCode.N))
        {
            // Start night if so
            GameObject.FindGameObjectWithTag("GameController").GetComponent<ManageLevels>().NextLevel();
            nextLevelPrompt.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    // If they're in the correct area, display the message
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            nextLevelPrompt.SetActive(true);
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            nextLevelPrompt.SetActive(false);
    }
}
