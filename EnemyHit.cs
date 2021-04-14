using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that controls when the enemy gets hit (attached to sword or arrow)
public class EnemyHit : MonoBehaviour
{
    // Reference to the character info file
    [SerializeField] private CharacterInfo _characterRef;

    // Reference to the player and player animator
    public GameObject player;
    private Animator _animator;

    // Preset arrow and sword damage
    private float _arrowDamage;
    private float _swordDamage;

    void Start()
    {
        // Get proper referneces
        player = GameObject.FindGameObjectWithTag("Player");
        _animator = player.GetComponent<Animator>();
        
        _swordDamage = _characterRef.GetStrength();
    
        if (_swordDamage == 0)
            _swordDamage = 20;

        _arrowDamage = _swordDamage * 0.5f;
    }

    // Check if they are attached
    private void OnCollisionEnter(Collision other) => CheckAttackEnemy(other.collider);
    private void OnTriggerEnter(Collider other) => CheckAttackEnemy(other);

    // Method to check if they're properly attacking the enemy
    private void CheckAttackEnemy(Collider other)
    {
 
        // Ensure they're hitting the enemy, actively fighting, and no cool down
        if ((_animator.GetBool("Hit") || _animator.GetBool("Shooting")) && player.GetComponent<AttackCooldown>().GetIfCanAttack()) {
            // Sound effect
            if (this.gameObject.tag == "Sword")
                FindObjectOfType<AudioTriggers>().PlaySwordHit();

            if (other.gameObject.tag == "Enemy")
            {
                player.GetComponent<AttackCooldown>().ResetAttack(); // Reset cool down
                other.gameObject.GetComponent<Enemy>().TakeDamage((this.gameObject.tag == "Sword") ? _swordDamage : _arrowDamage); // Set damage based on what's hitting
                if (this.gameObject.tag != "Sword") // If it's not a sword; destroy on impact
                    Destroy(this);
            } else if(other.gameObject.tag == "Final Fighter") {
                player.GetComponent<AttackCooldown>().ResetAttack(); // Reset cool down
                other.gameObject.GetComponent<PigBoss>().TakeDamage((this.gameObject.tag == "Sword") ? _swordDamage : _arrowDamage); // Set damage based on what's hitting
                if (this.gameObject.tag != "Sword") // If it's not a sword; destroy on impact
                    Destroy(this);
            }
        }
    }

    public void IncrementStrength(float strength)
    {
        _arrowDamage += strength;
        _swordDamage += strength;
    }
}
