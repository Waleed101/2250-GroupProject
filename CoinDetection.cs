using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDetection : MonoBehaviour


{
    public int scoreInc = 0;

    private void Start()
    {
        
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Coin") {
            // Coin sound effect
            FindObjectOfType<AudioTriggers>().PlayCoinCollect();
            FindObjectOfType<ScoreCounter>().incrementScore(scoreInc);
            Destroy(collision.gameObject);
        } else if(collision.gameObject.tag == "EnemyCoin") {
            FindObjectOfType<ScoreCounter>().incrementScore(scoreInc/2);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "GoldBar")
        {
            FindObjectOfType<AudioTriggers>().PlayCoinCollect();
            FindObjectOfType<ScoreCounter>().incrementScore(scoreInc *2);
            Destroy(collision.gameObject);
        }

        // References items with the tag toy and increases score by set amount of 50 points
        if (collision.gameObject.tag == "Toy")
        {
            FindObjectOfType<ScoreCounter>().incrementScore(scoreInc *5);
            Destroy(collision.gameObject);
        }

    }

}
