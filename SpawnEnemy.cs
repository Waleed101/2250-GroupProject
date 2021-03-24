using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Script that controls the spawning of enemies
public class SpawnEnemy : MonoBehaviour
{
    // Get custom info (rate, enemy types, and where they spawn from)
    [Header("Enemy Settings")]
    [SerializeField] private float _enemySpawnRate = 10f;
    [SerializeField] private GameObject[] _enemyArr;
    [SerializeField] private Transform _enemySpawnPoint;

    // Keep track of how many of the target enemies have killed
    private int _enemiesKilled = 0;
    public int targetEnemies = 10;

    // Reference to the blue counter and congrats screen
    public GameObject enemyCounter, congratsScreen;

    // Keeping track of the last spawn, keep track of spawn enabled
    private float _lastSpawn = 0f;
    private bool _spawnEnabled = false;

    void Start()
    {
        // Put correct message on display
        enemyCounter.GetComponent<TextMeshProUGUI>().text = "Enemies: " + _enemiesKilled + "/" + targetEnemies;
    }

    void Update()
    {
        // Spawn enemies in at specificed rate and only when spawning is enabled
        if(Time.time - _lastSpawn >= _enemySpawnRate && _spawnEnabled)
        {
            // Reset clock
            _lastSpawn = Time.time;
            // Spawn random enemy
            GameObject temp = Instantiate(_enemyArr[Random.Range(0, _enemyArr.Length)]);
            // Set proper size, position, point towards Xavier
            temp.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            temp.transform.position = _enemySpawnPoint.position;
            temp.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        }
    }

    // External method to track when an enemy is killed
    public void EnemyKilled() { 
        // Increment enemies killed; display on screen
        _enemiesKilled++;
        enemyCounter.GetComponent<TextMeshProUGUI>().text = "Enemies: " + _enemiesKilled + "/" + targetEnemies;
        // If target enemies reached
        if(_enemiesKilled == targetEnemies)
        {
            // Disable spawn, delete all the active enemies, and turn on the congrats screen
            DisableSpawn();
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                Destroy(enemy);
            congratsScreen.SetActive(true);
        }
    }

    // Enable/Disble spawn methods
    public void EnableSpawn() { _spawnEnabled = true; }
    public void DisableSpawn() { _spawnEnabled = false; }
}
