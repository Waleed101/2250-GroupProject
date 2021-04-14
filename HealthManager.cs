using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Script that mangages the health GUI
public class HealthManager : MonoBehaviour
{
    // Reference to death UI
    public GameObject DeathCanvas , QuickReviveUsed;

    // Keep track of current health and starting health
    private float _currentHealth, _startingHealth;
    public bool quickRevive;

    private Animator _anim;

    // Reference to the character info file
    [SerializeField] private CharacterInfo _characterRef;
    
    // Reference to the health UI
    public GameObject healthText;
    public GameObject healthbar;

    private GameObject _gameController;
    public float regenSpeed = 0f;
    private float _timeSinceLastRegen = 0f;
    
    void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController");
        // Set health to appropriate info
        _startingHealth = _characterRef.GetHealth();
        if (_startingHealth == 0)
            _startingHealth = 100;
        _currentHealth = _startingHealth;
        _anim = GetComponent<Animator>();
        UpdateHealthBar();
    }

    public void Update()
    {
        if(_gameController.GetComponent<ManageLevels>().GetIsCurrentlyDay() && _currentHealth < _startingHealth && ((Time.time - _timeSinceLastRegen) > regenSpeed))
        {
            IncrementCurrentHealth(1f);
            _timeSinceLastRegen = Time.time;
        }
    }

    // Method to take damage
    public void TakeDamage(float damage) { 
        // Takeaway provided damage, update UI
        _currentHealth -= damage;
        UpdateHealthBar();

        // If the health of the player reaches below 0, it is respawned to the beginning of the day sequence (game restart)
        if (_currentHealth <= 0)
        {
            _anim.SetBool("Dead", true);
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) Destroy(enemy);
            SpawnEnemy._spawnEnabled = false;
            _currentHealth = _startingHealth;

            ActionAfterDelay.Create(3.0f, () => { 
                DeathCanvas.SetActive(true);
                Screen.lockCursor = false;
            });
            ActionAfterDelay.Create(8.0f, () => { 
                Screen.lockCursor = true;
                DeathCanvas.SetActive(false);

                // An alternate scenario occurs if the player drank a quick revive potion prior to dying
                if (quickRevive)
                {
                    _anim.SetBool("Revive", true);
                    QuickReviveUsed.SetActive(true);
                    ActionAfterDelay.Create(3.0f, () =>
                    {
                        _anim.SetBool("Revive", false);
                        QuickReviveUsed.SetActive(false);

                    });
                    SpawnEnemy._spawnEnabled = true;
                }
                else
                {
                    // If player has not consumed a quick revive potion, then game restarts to initital settings
                    _gameController.GetComponent<ManageLevels>().BackToStart();
                    ScoreCounter._score = 0;
                }
                _anim.SetBool("Dead", false);
            });
        }
    }

    
    // The death sequence is delayed for 5 seconds so that the animation can play out in its entirity
 
    public void ExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    // Updates the health bar to reflect the current health of the player
    public void UpdateHealthBar()
    {
        healthText.GetComponent<TextMeshProUGUI>().text = Mathf.Round((_currentHealth / _startingHealth) * 100) + "%";
        FindObjectOfType<PlayerHealthBar>().SetHealth(_currentHealth);
    }

    // This method is called when the player drinks a health potion
    public void IncrementCurrentHealth(float health)
    {
        _currentHealth += health;
        UpdateHealthBar();
    }

    // To reset player's health and health bar
    public void ResetHealth()
    {
        _currentHealth = _startingHealth;
        UpdateHealthBar();
    }
}
