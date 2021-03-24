using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightTransition : MonoBehaviour
{
    private Vector3 _tempPos;
    private Quaternion _tempRotation;

    // This method is used to swap the directional lights of the moon and sun to simulate a day/night change
    public void triggerChange()
    {
        _tempPos = transform.position;
        _tempRotation = transform.rotation;
        _tempPos.y = -_tempPos.y;
        _tempRotation.x = -_tempRotation.x;
        transform.position = _tempPos;
        transform.rotation = _tempRotation;
    }
}
