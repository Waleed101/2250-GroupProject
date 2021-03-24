using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVillagers : MonoBehaviour
{
    public List<GameObject> villagers;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(villagers[0]).transform.position = new Vector3(1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
