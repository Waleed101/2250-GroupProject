using System.Collections; //Libraries
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //script to define the variables that can be adjusted with the third peron camera movement
    [Header("Settings")] 
    public float cameraSmoothingFactor = 1; //How smooth camera moves around the scene
    public float lookUpMax = 60; //Angle
    public float lookUpMin = -60;
    public float sensitivity = 5f;//How quick the camera moves
     
    
 
}



