using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

// This script manages the level transition
public class ManageLevels : MonoBehaviour
{
    // Different assets that need to be active or inactive based on the time
    public GameObject[] day, night, arena, dayWaypoints, armor, finalDayLevel;
    public GameObject player, plants, drawBridge, targetUI, dialogue, tutorial, wife, levelInfo;

    // Reference to needed controls
    private GameObject _newLevelUI, _gameController;

    // Level referneces 
    private int  _maxLevel = 7, _currentDayLevel = 0;
    public int currentLevel = 0;
    
    // Location info
    [System.Serializable]
    public struct LocationInspector
    {
        public string name;
        public Transform loc;
    }

    public LocationInspector[] locations;
    Dictionary<string, Transform> dicLocation = new Dictionary<string, Transform>();

    // Startup method
    public void Start()
    {
        // Setup proper references, start day
        _gameController = GameObject.FindGameObjectWithTag("GameController");
        foreach (LocationInspector indivLoc in locations)
            dicLocation.Add(indivLoc.name, indivLoc.loc);

        ResetAll();
        DaySequence(true);
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        _newLevelUI = GameObject.FindGameObjectWithTag("New Level");
    }

    // Quick methods and demo tools created
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && _newLevelUI.GetComponent<NewLevelUI>().IsUIActive()) {
            if (_currentDayLevel == 1)
                targetUI.SetActive(true);
            if(_currentDayLevel < 3)
                player.GetComponent<WayPointArrowMovement>().SetWaypoint(dayWaypoints[_currentDayLevel - 1].transform.position, "New Waypoint!");
            _newLevelUI.GetComponent<NewLevelUI>().TurnOffUI();
        }

        // Quick debuggers
        if (Input.GetKeyDown(KeyCode.Z))
            MovePlayer(dicLocation["Day"]);

        if (Input.GetKeyDown(KeyCode.M))
            drawBridge.SetActive(false);
    }

    // Method to reset the scene
    private void ResetAll()
    {
        foreach (GameObject dayAsset in day)
            dayAsset.SetActive(false);
        foreach (GameObject nightAsset in night)
            nightAsset.SetActive(false);
        foreach (GameObject arenaAsset in arena)
            arenaAsset.SetActive(false);
        foreach (GameObject finalAsset in finalDayLevel)
            finalAsset.SetActive(false);
        plants.SetActive(false);
        targetUI.SetActive(false);
        dialogue.SetActive(false);
    }

    // Method to set the final level
    public void FinalDayLevelSequence()
    {
        ResetAll();

        foreach (GameObject finalAsset in finalDayLevel)
            finalAsset.SetActive(true);

        // Disable the enemy spawn
        _gameController.GetComponent<SpawnEnemy>().DisableSpawn();
    }


    // Method that controls the night time
    public void NightSequence(float spawnRate, int enemiesToKill)
    {
        ResetAll();
        // Set position to night spot
        MovePlayer(dicLocation["Night"]);

        foreach (GameObject nightAsset in night)
            nightAsset.SetActive(true);

        // Enable the enemy spawn
        _gameController.GetComponent<SpawnEnemy>().EnableSpawn(spawnRate, enemiesToKill);

        float speed = 1f, strength = 1f;

        switch(_currentDayLevel) {
            case 1:
                speed = 1.1f;
                strength = 1.1f;
                break;

            case 2:
                speed = 1.25f;
                strength = 1.25f;
                break;

            case 3:
                speed = 1.5f;
                strength = 1.5f;
                break;
        }

        // Set enemy speed and strength to progress
        _gameController.GetComponent<SpawnEnemy>().SetSpeed(speed);
        _gameController.GetComponent<SpawnEnemy>().SetStrength(strength);
        _gameController.GetComponent<SpawnEnemy>().UpdateCounter();
    }

    // Method that controls the day time
    public void DaySequence(bool teleport)
    {
        ResetAll();

        // Set position to day spot
        if(teleport)
            MovePlayer(dicLocation["Day"]);

        foreach (GameObject dayAsset in day)
            dayAsset.SetActive(true);

        // Disable the enemy spawn
        _gameController.GetComponent<SpawnEnemy>().DisableSpawn();
    }

    public void ArenaSequence()
    {
        ResetAll();

        // Set position to day spot
        MovePlayer(dicLocation["Arena"]);

        foreach (GameObject arenaAsset in arena)
            arenaAsset.SetActive(true);

        // Disable the enemy spawn
        _gameController.GetComponent<SpawnEnemy>().DisableSpawn();

    }

    // Method to teleport the player
    public void MovePlayer(Transform newLoc)
    {
        player.GetComponent<NavMeshAgent>().enabled = false;
        player.transform.position = newLoc.position;
        player.GetComponent<NavMeshAgent>().enabled = true;
    }

    // Method to reset game play
    public void BackToStart()
    {
        DaySequence(true);
        currentLevel = 0;
        player.GetComponent<HealthManager>().ResetHealth();
        _currentDayLevel = 0;
        foreach (GameObject days in dayWaypoints)
            days.SetActive(false);
        foreach (GameObject indivArmor in armor)
            indivArmor.SetActive(false);
        FindObjectOfType<NPCTrigger>().nextDialogueSet(0);
        tutorial.GetComponent<TutorialManager>().ResetTutorial();
    }

    // Method to control transition to following levels
    public void NextLevel() { 
        currentLevel++;
        if (currentLevel > _maxLevel) { 
            currentLevel = _maxLevel;
        }
        levelInfo.GetComponent<TextMeshProUGUI>().text = "Current Level: " + (currentLevel+1) + "/8";
        // Control the current level
        switch(currentLevel) {
            case 1: // Night 1
                ResetAll();
                NightSequence(10f, 5);
                plants.SetActive(false);
                break;

            case 2: // Day 2
                ResetAll();
                _currentDayLevel++;
                dayWaypoints[0].SetActive(true);
                player.GetComponent<HealthManager>().IncrementCurrentHealth(10f);
                _newLevelUI.GetComponent<NewLevelUI>().UpdateUI(_currentDayLevel);
                _newLevelUI.GetComponent<NewLevelUI>().TurnOnUI();
                armor[0].SetActive(true);
                DaySequence(false);
                FindObjectOfType<NPCTrigger>().nextDialogueSet(currentLevel);
                break;

            case 3: // Night 2
                ResetAll();
                NightSequence(7.5f, 10);
                plants.SetActive(true);
                break;

            case 4: // Day 3
                ResetAll();
                _currentDayLevel++;
                dayWaypoints[1].SetActive(true);
                player.GetComponent<HealthManager>().IncrementCurrentHealth(20f);
                _newLevelUI.GetComponent<NewLevelUI>().UpdateUI(_currentDayLevel);
                _newLevelUI.GetComponent<NewLevelUI>().TurnOnUI();
                armor[1].SetActive(true);
                DaySequence(false);
                FindObjectOfType<NPCTrigger>().nextDialogueSet(currentLevel);
                break;

            case 5: // Night 3
                ResetAll();
                NightSequence(5f, 15);
                plants.SetActive(true);
                break;

            case 6: // Day 4
                ResetAll();
                _currentDayLevel++;
                dayWaypoints[2].SetActive(true);
                FinalDayLevelSequence();
                player.GetComponent<HealthManager>().IncrementCurrentHealth(30f);
                _newLevelUI.GetComponent<NewLevelUI>().UpdateUI(_currentDayLevel);
                _newLevelUI.GetComponent<NewLevelUI>().TurnOnUI();
                armor[2].SetActive(true);
                wife.SetActive(false);
                break;

            case 7: // Arena
                ResetAll();
                ArenaSequence();
                break;
        }
    }

    // Get/Set methods as needed
    public int GetCurrentDayLevel() { return _currentDayLevel; }
    public bool GetIsCurrentlyDay() { return currentLevel%2 == 0; }
}
