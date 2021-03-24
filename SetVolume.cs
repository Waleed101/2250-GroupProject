using System.Collections; //Libraries
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour //class that allows for changing the volume 
{
    public AudioMixer mixer;
    public void SetLevel (float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20); //slider value as a log value to base ten to represent the sound on a decibel scale 
    }
}
