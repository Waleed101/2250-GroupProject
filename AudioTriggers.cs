using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTriggers : MonoBehaviour
{

    public AudioSource run, walk, sHit, sSwing, bow, coin, drink; //different audiosources for sound effects

    public void PlaySwordSwing() //audio plays with sword swing
    {
        if (!sHit.isPlaying && !sSwing.isPlaying)//checks to make sure the sword is NOT hitting anything
            sSwing.Play();
    }
    public void PlaySwordHit() //method for audio when sword hits something 
    {
        if (!sHit.isPlaying) //if the hitting audio isnt playing it will stop playing the normal swinging audio and play the hitting sound
        {
            sSwing.Stop();
            sHit.Play();
        }
    }
    public void PlayShootBow() //audio method for bow shooting
    {
        bow.Play();
    }
    public void PlayWalk() //audio method for when player is walking
    {
        if (!walk.isPlaying && !run.isPlaying)
            walk.Play();
    }
    public void PlayRun() //audio method for when the player is running
    {
        if(!walk.isPlaying && !run.isPlaying)
            run.Play();
    }
    public void PlayCoinCollect() //audio method for when the player picks up a coin
    {
        coin.Play();
    }
    public void PlayDrinking() //audio method for when a player drinks a potion
    {
        drink.Play();
    }
}
