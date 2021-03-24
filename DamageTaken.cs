using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaken : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        print("Hit");
        if (collision.gameObject.tag == "Sword")
            print("Sword Hit");
    }
}
