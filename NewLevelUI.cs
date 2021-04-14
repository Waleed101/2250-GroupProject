using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Manages the UI for the new levels
public class NewLevelUI : MonoBehaviour
{
    // Reference to the panels and sprites
    public GameObject UI_one, UI_two;
    public Sprite potions, armor, archer, instantRevive;

    public void UpdateUI(int currentLevel)
    {
        switch(currentLevel)
        {
            case 1: // If completed night 1
                UI_one.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "+10 Health";
                UI_two.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Archery";
                UI_two.transform.GetChild(3).GetComponent<Image>().sprite = archer;
                break;

            case 2: // night 2
                UI_one.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "+20 Health";
                UI_two.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Potions";
                UI_two.transform.GetChild(3).GetComponent<Image>().sprite = potions;
                break;

            case 3: // night 3
                UI_one.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "+30 Health";
                UI_two.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "New Potion";
                UI_two.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Instant Revive";
                UI_two.transform.GetChild(3).GetComponent<Image>().sprite = instantRevive;
                break;
        }
    }

    // Open the UI, close the UI
    public void TurnOnUI() { transform.GetChild(0).gameObject.SetActive(true); }
    public void TurnOffUI() { transform.GetChild(0).gameObject.SetActive(false); }


    // Check if the UI is active
    public bool IsUIActive() { return transform.GetChild(0).gameObject.activeSelf; }
}
