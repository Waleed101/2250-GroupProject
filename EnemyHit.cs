using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public GameObject player;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = player.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && (animator.GetBool("Hit") || animator.GetBool("Shoot")))
        {
            print("Enemy hit");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && (animator.GetBool("Hit") || animator.GetBool("Shoot"))) {
            print("Enemy hit");
        }
    }
}
