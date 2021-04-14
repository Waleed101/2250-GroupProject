using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PigBoss : MonoBehaviour
{
    // References to the various objects beeded
    private GameObject _player;
    public GameObject healthRef;
    public Animator anim;
    private float _timeSince = 0f, _timeOfMovement = 0f, _currentHealth, _maxHealth = 50f, _timeSinceLast = 0f, _timeToNextKill = 3f;
    private Vector3 _curTarget;
    private bool _enteredPlayerZone = false, _chargedAttack = false, _dead = false, _ftimeDead = true;

    void Start()
    {
        // Reset health
        _currentHealth = _maxHealth;
        FindObjectOfType<EnemyHealthBar>().SetStartingHealth(_maxHealth);

        _player = GameObject.FindGameObjectWithTag("Player");
       // anim = pigModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // If not dead
        if (!_dead)
        {
            // Look at player
            transform.LookAt(_player.transform);

            // Charge towards player
            if ((Time.time - _timeSince) > 5f && _timeOfMovement == 0f)
            {
                _timeOfMovement = Time.time;
                _curTarget = _player.transform.position;
            }

            // Move player for two seconds
            if ((Time.time - _timeOfMovement) < 2f && Time.time > 0f && !_enteredPlayerZone)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, _curTarget, 10f * Time.deltaTime);
                temp.y = -11.31f;
                transform.position = temp;
                print(_curTarget);
                anim.SetBool("Run", true);
            }

            // When reached smash the ground
            else if (_timeOfMovement != 0f && (Time.time - _timeOfMovement) > 2f)
            {
                _timeSince = Time.time;
                _timeOfMovement = 0f;
                anim.SetBool("Run", false);
                _chargedAttack = true;
                StartCoroutine(Attack());
            }

            // If they're close by - attack
            if (_enteredPlayerZone)
            {
                anim.SetBool("Run", false);
                anim.SetBool("Attack", true);

                AffectPlayer();
            }
            else if (!_chargedAttack)
                anim.SetBool("Attack", false);
        }
        // When the final enemy dies, intialize end sequence and freeze movement
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Attack", false);
            anim.SetBool("Death", true);
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            if(_ftimeDead)
            {
                this.GetComponent<StartSequence>().enabled = true;
                this.GetComponent<StartSequence>().OpenDrawBridge();
                _ftimeDead = false;
            }
        }


    }

    // Method to be override by children that dictates how the palyer is affected
    public virtual void AffectPlayer()
    {
        // Make sure they don't do it too much and they're in range of the player
        if (((Time.time - _timeSinceLast) > _timeToNextKill) && anim.GetBool("Attack"))
        {
            // Normal enemy takes off 1 health
            _player.GetComponent<HealthManager>().TakeDamage(5f);
            _timeSinceLast = Time.time;
        }
    }

    IEnumerator Attack()
    {
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + 0.5f);
        anim.SetBool("Attack", false);
        _chargedAttack = false;
    }

    // Method to take damage from player hit
    public void TakeDamage(float deductDamage)
    {
        _currentHealth -= deductDamage; // Deduct appropriate damage
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth); // Ensure not below or above bounds
        healthRef.GetComponent<TextMeshPro>().text = (_currentHealth / _maxHealth) * 100 + "%"; // Adjust health bar

        FindObjectOfType<EnemyHealthBar>().SetHealth(_currentHealth);

        if (_currentHealth <= 0f)
            _dead = true;
    }

    // Methods to mark when the final enemy enters the player
    public void EnteredPlayer() { _enteredPlayerZone = true; }
    public void ExitedPlayer() { _enteredPlayerZone = false; }
}
