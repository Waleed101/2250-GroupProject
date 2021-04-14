using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToySpawner : MonoBehaviour
{

    // Empty game object locations
    public Transform[] spawnLocations;

    // References coin prefab
    public GameObject[] toy;

    public GameObject[] clone;

    // Start is called before the first frame update


    private void Start()
    // Start is called before the first frame update
    {

        for (int i = 0; i < spawnLocations.Length; i++)
        {
            clone[i] = Instantiate(toy[i], spawnLocations[i].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        }

    }
}