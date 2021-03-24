using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public GameObject player;
    private Animator animator;
    private const float arrowDamage = 10f, swordDamage = 50f;

    // Start is called before the first frame update
    void Start() => animator = player.GetComponent<Animator>();

    private void OnCollisionEnter(Collision other) => CheckAttackEnemy(other.collider);
    private void OnTriggerEnter(Collider other) => CheckAttackEnemy(other);

    private void CheckAttackEnemy(Collider other)
    {
        print("--------------------------------");
        print(other.gameObject.tag);
        print("Mouse: " + (animator.GetBool("Hit") || animator.GetBool("Shooting")));
        print(player.GetComponent<AttackCooldown>().GetIfCanAttack());
        if (other.gameObject.tag == "Enemy" && (animator.GetBool("Hit") || animator.GetBool("Shooting")) && player.GetComponent<AttackCooldown>().GetIfCanAttack()) {
            player.GetComponent<AttackCooldown>().ResetAttack();
            print("Attackin!");
            other.gameObject.GetComponent<Enemy>().TakeDamage((this.gameObject.tag == "Sword") ? swordDamage : arrowDamage);
        }
    }
}
