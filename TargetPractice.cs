using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TargetPractice : MonoBehaviour
{
    // Amount to increment score (set in inspector)
    public int scoreInc = 0;

    // Target number of hits to initiate further game progress
    public int targetGoal = 5;

    // Local boolean values for progression purposes
    private int _targetHits;
    private bool _allTargetsHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        // Score is incremented if the target is hit by an arrow
        if (collision.gameObject.tag == "Arrow")
        {
            FindObjectOfType<ScoreCounter>().incrementScore(scoreInc);
            if (!_allTargetsHit)
                TargetHit();
            Destroy(collision.gameObject);
        }
    }

    // Updates the target UI everytime a target is hit until target goal is fulfilled
    public void TargetHit()
    {
        // Increment enemies killed; display on screen
        _targetHits++;
        GameObject.FindGameObjectWithTag("TargetUI").GetComponent<TextMeshProUGUI>().text = "Targets: " + _targetHits + "/" + targetGoal;
        // If target goal is reached
        if (_targetHits == targetGoal)
        {
            _allTargetsHit = true;
            // Lets the game controller know that the level's test is completed
            GameObject.FindGameObjectWithTag("GameController").GetComponent<DayLevelManager>().TaskCompleted();
        }
    }
}
