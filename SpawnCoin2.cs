using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnCoin2 : MonoBehaviour
{

    public GameObject Coin2;

    void Start()
    {
        Instantiate(Coin2, new Vector3(22.219f, 0.5509f, 30.580f), Quaternion.identity);
    }

    void Update()
    {

    }

}
