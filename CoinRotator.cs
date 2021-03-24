using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotator : MonoBehaviour
{

    public int rotateSpeed;

     void Start()
    {
        rotateSpeed = 2;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, rotateSpeed, 0));
    }
}
