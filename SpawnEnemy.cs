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
    public static bool _spawnEnabled = false;

    private float _speedMultiplier = 1f, _strengthMultiplier = 1f;

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
            int type = Random.Range(0, _enemyArr.Length);
            // Spawn random enemy
            GameObject temp = Instantiate(_enemyArr[type]);
            if(type == 0) {
                temp.GetComponent<Enemy>().SetSpeedMultiplier(_speedMultiplier);
                temp.GetComponent<Enemy>().SetStrengthMultiplier(_strengthMultiplier);
            } else {
                temp.GetComponent<EnemyStealer>().SetSpeedMultiplier(_speedMultiplier);
                temp.GetComponent<EnemyStealer>().SetStrengthMultiplier(_strengthMultiplier);
            }
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
        UpdateCounter();
        // If target enemies reached
        if(_enemiesKilled == targetEnemies)
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                Destroy(enemy);
            this.GetComponent<ManageLevels>().NextLevel();
            print("Done Level!");
        }
    }

    public void UpdateCounter() => enemyCounter.GetComponent<TextMeshProUGUI>().text = "Enemies: " + _enemiesKilled + "/" + targetEnemies;

    // Enable/Disble spawn methods
    public void EnableSpawn(float spawnRate, int toKill)
    {
        _enemiesKilled = 0;
        enemyCounter.GetComponent<TextMeshProUGUI>().text = "Enemies: " + _enemiesKilled + "/" + targetEnemies;
        _spawnEnabled = true;
        _enemySpawnRate = spawnRate;
        targetEnemies = toKill;
    }
    public void DisableSpawn() { _spawnEnabled = false; }
    public void SetStrength(float strength) { _strengthMultiplier = strength; }
    public void SetSpeed(float speed) { _speedMultiplier = speed; }
}
