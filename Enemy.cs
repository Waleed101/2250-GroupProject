using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

// Main parent enemy to have traditional enemy movement, death, and animations
public class Enemy : MonoBehaviour
{
    // Reference to needed objects
    public GameObject player;
    public GameObject healthRef;
    public Animator anim;
    public GameObject coin;

    // Set movement speed, direction away from the target player
    public float movementSpeed = 5f;
    // Keeping track of time to see when to dock off health next and how long the enemy has been attacking
    public float xDir = -0.8f, zDir = -0.8f, currentHealth, maxHealth = 100f, timeSinceEntry = 0f, timeToNextKill = 3f, timeSinceLast = 0f, currentTime = 0f;
    private float _speedMultiplier = 1f, _strengthMultiplier = 1f;
    // Check to see if they're current animation or if they've "found" the player yet
    private bool _isWalking = true, _fight = false, _dieSequenceStarted = false, _alreadyEnteredPlayerZone = false, _isRunner = false, _enemyCoin = true;

    public virtual void Start()
    {
        // Reset health
        currentHealth = maxHealth;

        // Set their health bar according to what it is
        if(_isRunner)
            FindObjectOfType<RunnerHealthBar>().SetStartingHealth(maxHealth);
        else
            FindObjectOfType<HitterHealthBar>().SetStartingHealth(maxHealth);
        
        player = GameObject.FindGameObjectWithTag("Player");
        anim = this.GetComponent<Animator>();

        // Set random fight type
        anim.SetFloat("Fight Type", UnityEngine.Random.Range(0f, 1f));
    }

    public void SetAsRunner() { _isRunner = true; }

    void Update()
    {
        // If the enemy has been killed, initiate die sequence
        if(currentHealth <= 0f && !_dieSequenceStarted) {
            _dieSequenceStarted = true;
            // Start die squenece
            StartCoroutine(DieSequence());
            // Turn off animations
            anim.SetBool("Walking", false);
            anim.SetBool("Fight", false);
        } else if(!_dieSequenceStarted) { // If not, party on!
            AffectPlayer(); // Run the affect player; could be damaging or stealing
            Move(); // Move player
            // Animate as needed
            anim.SetBool("Walking", _isWalking);
            anim.SetBool("Fight", _fight);
        }
    }

    // Method to be override by children that dictates how the palyer is affected
    public virtual void AffectPlayer()
    {
        // Make sure they don't do it too much and they're in range of the player
        if (((Time.time - timeSinceLast) > timeToNextKill) && anim.GetBool("Fight"))
        {
            // Normal enemy takes off 1 health
            player.GetComponent<HealthManager>().TakeDamage(2.5f * _strengthMultiplier);
            timeSinceLast = Time.time;
        }
    }

    // Method to run die sequence animation and then destroy enemy
    IEnumerator DieSequence()
    {
        // Run death animation until its done
        anim.SetBool("Death", true);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + 0.5f);
        GameObject temp = Instantiate(coin, gameObject.transform.position + new Vector3(0,1,0), Quaternion.Euler(0, 0, 0));
        if(_enemyCoin)
            temp.tag = "EnemyCoin";
        
        // Count enemy killed
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnEnemy>().EnemyKilled();
        Destroy(this.gameObject);
    }

    // Traditional move sequence
    public virtual void Move()
    {
        // Get current target position
        Vector3 playerPos = player.transform.position;

        // Get adjusted target position of where the enemy wants to go
        playerPos.x += xDir; playerPos.z += zDir;

        // Draw the approprate vectors and move towards that position slowly
        Vector3 pos = Vector3.MoveTowards(transform.position, playerPos, movementSpeed * Time.deltaTime * _speedMultiplier);
        Vector3 diff = Absolute(transform.position) - Absolute(pos);

        // Get adjusted height; THIS NEEDS WORK
        diff.y = Terrain.activeTerrain.SampleHeight(transform.position) + Terrain.activeTerrain.GetPosition().y + 0.05f;
        transform.position = pos;

        // Look at target player
        transform.LookAt(player.transform);
    }

    // Get absolute value of a vector easily; no built in function
    public Vector3 Absolute(Vector3 passedOver)
    {
        Vector3 toReturn;
        toReturn.x = Mathf.Abs(passedOver.x);
        toReturn.y = Mathf.Abs(passedOver.y);
        toReturn.z = Mathf.Abs(passedOver.z);
        return toReturn;
    }

    // Method to take damage from player hit
    public void TakeDamage(float deductDamage) { 
        currentHealth -= deductDamage; // Deduct appropriate damage
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ensure not below or above bounds

        // Show in UI
        if (_isRunner)
            FindObjectOfType<RunnerHealthBar>().SetHealth(currentHealth);
        else
            FindObjectOfType<HitterHealthBar>().SetHealth(currentHealth);
      
        healthRef.GetComponent<TextMeshPro>().text = (currentHealth/maxHealth)*100 + "%"; // Adjust health bar
    }

    // Method to externally set they're movement type
    public void SetMotion(bool moving) { _isWalking = moving; _fight = !moving; }

    // Method to set if they're in the player's zone (they're collider)
    public void SetInPlayerZone() {
        if (!_alreadyEnteredPlayerZone)
        {
            timeSinceEntry = Time.time;
            _alreadyEnteredPlayerZone = true;
        }
        _isWalking = false; _fight = true; 
    }

    // Method to get whether or not they're fighting
    public bool GetCurrentAction() { return _fight; }

    // Method to get how long they've been interacting with the character
    public float GetTimeSinceEntry() { 
        if (timeSinceEntry == 0) // If they haven't reached the player yet; its 0
            return 0; 
        return (Time.time - timeSinceEntry); 
    }

    // Get/Set methods for current and max health
    public void SetMaxHealth(float newMaxHealth) { maxHealth = newMaxHealth; }
    public void SetCurrentHealth(float newCurrentHealth) { currentHealth = newCurrentHealth; }
    public float GetCurrentHealth() { return currentHealth; }

    // Method to get if they've entered the player zone
    public bool GetIfEnteredZone() { return _alreadyEnteredPlayerZone; }
    public void StoleCoin() { _enemyCoin = false; }
    public void SetSpeedMultiplier(float nSpeedMultiplier) { _speedMultiplier = nSpeedMultiplier; }
    public void SetStrengthMultiplier(float nStrengthMultiplier) { _strengthMultiplier = nStrengthMultiplier; }
    public float GetSpeedMultiplier() { return _speedMultiplier; }
    public float GetStrengthMultiplier() { return _strengthMultiplier; }
}
