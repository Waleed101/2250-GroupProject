using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Coin 1 is spawn 
public class SpawnCoin1 : MonoBehaviour
{

   // Game Object 1
    public GameObject Coin1;

    void Start()
    {
    
        // Position of where coin 1 is spawn 
        Instantiate(Coin1, new Vector3(-64.86f, -11.875f, -44.71f), Quaternion.identity);
    }

    void Update()
    {

    }

}
