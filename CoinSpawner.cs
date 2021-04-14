using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    // Empty game object locations
    public Transform[] spawnLocations;

    // References coin prefab
    public GameObject coin, goldBar;

    // Start is called before the first frame update

    
    private void Start()
    {
        // Iterate through array of spawn locations
        for (int i = 0; i < 30; i++)
        {

            // Coin prefabs spawn 
            Instantiate(coin, spawnLocations[i].transform);
        }
        for (int i = 30; i < spawnLocations.Length; i++)
        {
            Instantiate(goldBar, spawnLocations[i].transform);
        }
    }
}