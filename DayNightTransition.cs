using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightTransition : MonoBehaviour
{
    Vector3 tempPos;
    Quaternion tempRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void triggerChange()
    {
        tempPos = transform.position;
        tempRotation = transform.rotation;
        tempPos.y = -tempPos.y;
        tempRotation.x = -tempRotation.x;
        transform.position = tempPos;
        transform.rotation = tempRotation;
    }
}
