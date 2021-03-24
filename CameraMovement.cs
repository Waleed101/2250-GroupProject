using System.Collections; //Libraries
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //script to define the variables that can be adjusted with the third peron camera movement
    [Header("Settings")] 
    public float cameraSmoothingFactor = 1;//how smooth camera moves around the scene
    public float lookUpMax = 60; //angle
    public float lookUpMin = -60;
    public float sensitivity = 5f;//how quick the camera moves
     
    
 
}







