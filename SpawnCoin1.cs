using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnCoin1 : MonoBehaviour
{

    public GameObject Coin1;

    void Start()
    {
        Instantiate(Coin1, new Vector3(-64.86f, -11.875f, -44.71f), Quaternion.identity);
    }

    void Update()
    {

    }

}
