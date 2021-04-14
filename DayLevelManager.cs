using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that manages the progress through the individual days
public class DayLevelManager : MonoBehaviour
{
    // Reference to the two teleporters
    public GameObject nightTeleporter, arenaTeleporter;
    
    // Reference to the player to save space/time
    private GameObject _player;

    // Array counting tasks to complete
    public int[] taskToComplete;
    private int _taskCounter = 0;

    // Hide both portals on load
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        DisableArenaTeleport();
        DisableNightTeleport();
    }

    // External reference methods if needed
    public void DisableNightTeleport() { nightTeleporter.SetActive(false); }
    public void EnableNightTeleport() { nightTeleporter.SetActive(true); }

    public void DisableArenaTeleport() { arenaTeleporter.SetActive(false); }
    public void EnableArenaTeleport() { arenaTeleporter.SetActive(true); }

    // Method to be called when a task is completed
    public void TaskCompleted() { 
        _taskCounter++; // Increment counter

        // Check to see if they've completed all tasks
        if (_taskCounter >= taskToComplete[this.GetComponent<ManageLevels>().GetCurrentDayLevel()])
        {
            // Reset if so
            _taskCounter = 0;
            
            // If on final day level, start arena teleporter
            if ((this.GetComponent<ManageLevels>().GetCurrentDayLevel() + 1) == taskToComplete.Length)
            {
                EnableArenaTeleport();
                _player.GetComponent<WayPointArrowMovement>().SetWaypoint(arenaTeleporter.transform.position, "Start the final fight!");
            }

            // If on normal, start night teleporter
            else
            {
                EnableNightTeleport();
                _player.GetComponent<WayPointArrowMovement>().SetWaypoint(nightTeleporter.transform.position, "Start the night!");
            }
        }
    }
}
