using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rotates coins
public class CoinRotator : MonoBehaviour

{
    
    // Update is called once per frame
    void Update()
    {
        // Coin is rotated 
        transform.Rotate(0,3,0);
    }
}
