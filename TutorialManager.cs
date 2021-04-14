using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // The collection of UI tutorial popups
    public GameObject[] popUps;
    // The index that selects which tutorial popup to display
    private int _popUpIndex;
    // Reference to player objects 
    public GameObject swordHand;
    public GameObject bowHand;

    private void Start()
    {
        _popUpIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // This for loop is used to toggle on and off the popups for game tutorial
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == _popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        // Depending which section of the tutorial is displayed, the player has to fulfill a 
        // certain condition to progress to the next part of the tutorial
        if (_popUpIndex == 0)
        {
            if (Input.GetAxis("Vertical") > 0)
                _popUpIndex++;
        }
        else if (_popUpIndex == 1)
        {
            if (Input.GetAxis("Run") > 0)
                _popUpIndex++;
        }
        else if (_popUpIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _popUpIndex++;
        }
        else if (_popUpIndex == 3)
        {
            if (Input.GetMouseButtonDown(0) && swordHand.activeSelf)
                _popUpIndex++;
        }
        else if (_popUpIndex == 4)
        {
            if (Input.GetMouseButtonDown(0) && bowHand.activeSelf)
                _popUpIndex++;
        }
        else if(_popUpIndex == 5)
        {
            _popUpIndex++;
            // Tutorial is completed
            GameObject.FindGameObjectWithTag("GameController").GetComponent<DayLevelManager>().TaskCompleted();
        }
    }

    // Resets the tutorial
    public void ResetTutorial() => _popUpIndex = 0;
}
