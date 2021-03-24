using UnityEngine;

public class SpawnCoin3 : MonoBehaviour
{

    public GameObject Coin3;

    void Start()
    {
        Instantiate(Coin3, new Vector3(-33.29f, -8.324f, 72.325f), Quaternion.identity);
    }

    void Update()
    {

    }

}
