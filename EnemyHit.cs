using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that controls when the enemy gets hit (attached to sword or arrow)
public class EnemyHit : MonoBehaviour
{
    // Reference to the player and player animator
    public GameObject player;
    private Animator _animator;

    // Preset arrow and sword damage
    private const float _arrowDamage = 10f, _swordDamage = 25f;

    void Start()
    {
        // Get proper referneces
        player = GameObject.FindGameObjectWithTag("Player");
        _animator = player.GetComponent<Animator>();
    }

    // Check if they are attached
    private void OnCollisionEnter(Collision other) => CheckAttackEnemy(other.collider);
    private void OnTriggerEnter(Collider other) => CheckAttackEnemy(other);

    // Method to check if they're properly attacking the enemy
    private void CheckAttackEnemy(Collider other)
    {
        // Ensure they're hitting the enemy, actively fighting, and no cool down
        if (other.gameObject.tag == "Enemy" && (_animator.GetBool("Hit") || _animator.GetBool("Shooting")) && player.GetComponent<AttackCooldown>().GetIfCanAttack()) {
            player.GetComponent<AttackCooldown>().ResetAttack(); // Reset cool down
            other.gameObject.GetComponent<Enemy>().TakeDamage((this.gameObject.tag == "Sword") ? _swordDamage : _arrowDamage); // Set damage based on what's hitting
            if (this.gameObject.tag != "Sword") // If it's not a sword; destroy on impact
                Destroy(this);
        }
    }
}
