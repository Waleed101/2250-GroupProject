using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script that controls the wakeup/go-to-sleep animation
public class EyeBlink : MonoBehaviour
{
    // Reference to the boxes
    public RectTransform upperBox;
    public RectTransform lowerBox;

    public bool bedroomScene = true;

    // Control the speed to add more to the effect
    [System.Serializable]
    public struct EyeBlinker
    {
        public float upper, lower, speed;
    }

    public EyeBlinker[] blinker;

    // Keep track of positions and where we're currently at
    private bool _opening = true, _ftime = true;
    private Vector3 _originalUpperPosition, _endUpperPosition;
    private Vector3 _originalLowerPosition, _endLowerPosition;
    private int _currentBlink = 0;

    public void Start()
    {
        // Set the size of the "eyelids" so they cover the whole screen
        upperBox.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
        lowerBox.sizeDelta = new Vector2(Screen.width, Screen.height / 2);

        // Set the position of the boxes to start
        _originalUpperPosition = upperBox.localPosition;
        _originalLowerPosition = lowerBox.localPosition;

        // Set the max and min bounds
        _endUpperPosition = _originalUpperPosition; _endLowerPosition = _originalLowerPosition;
        _endUpperPosition.y = blinker[_currentBlink].upper; _endLowerPosition.y = blinker[_currentBlink].lower;

        // Set where the eyelids will end up upon completion

        if(bedroomScene) {
            blinker[blinker.Length - 1].upper = Screen.height / 2 * 1.3f;
            blinker[blinker.Length - 1].lower = -Screen.height / 2 * 1.3f;
        } else {
            blinker[blinker.Length - 1].upper = 105f;
            blinker[blinker.Length - 1].lower = -105f;
        }
    }

    public void Update()
    {
        // Check to see if we've done the max number of blinks
        if(_currentBlink < blinker.Length) {
            _endUpperPosition.y = blinker[_currentBlink].upper; _endLowerPosition.y = blinker[_currentBlink].lower;
            if (_opening) { // If currently opening eyes, move towards open position
                upperBox.localPosition = Vector3.MoveTowards(upperBox.localPosition, _endUpperPosition, blinker[_currentBlink].speed);
                lowerBox.localPosition = Vector3.MoveTowards(lowerBox.localPosition, _endLowerPosition, blinker[_currentBlink].speed);
                if (GetIfAtLocation(upperBox.localPosition, _endUpperPosition) && GetIfAtLocation(lowerBox.localPosition, _endLowerPosition)) { // If at intended location, switch to next
                    _opening = false;
                    _currentBlink++;
                }
            } else { // If currently closing eyes, move towards closing position
                upperBox.localPosition = Vector3.MoveTowards(upperBox.localPosition, _originalUpperPosition, blinker[_currentBlink].speed);
                lowerBox.localPosition = Vector3.MoveTowards(lowerBox.localPosition, _originalLowerPosition, blinker[_currentBlink].speed);
                if (GetIfAtLocation(upperBox.localPosition, _originalUpperPosition) && GetIfAtLocation(lowerBox.localPosition, _originalLowerPosition)) { // If at intended locaiton, switch to next
                    _opening = true;
                    _currentBlink++;
                }
            }
        } else if(_ftime && bedroomScene) { // If done, then turn on wakeup sequence and exit 
            GetComponent<WakeupCamera>().Wakeup();
            _ftime = false;
        } else if(_ftime) {
            SceneManager.LoadScene("Bedroom");
        }
    }

    // Quick method to check if a vector is at a location
    public bool GetIfAtLocation(Vector3 curLoc, Vector3 targetLoc) => (Vector3.Distance(curLoc, targetLoc) < 0.0001f);
}