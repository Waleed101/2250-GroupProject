using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDetection : MonoBehaviour


{
    public int scoreInc = 0; //var to increase score from objects

    
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Coin") { //if player picks up coin score increments
            // Coin sound effect
            FindObjectOfType<AudioTriggers>().PlayCoinCollect();
            FindObjectOfType<ScoreCounter>().incrementScore(scoreInc);
            Destroy(collision.gameObject);
        } else if(collision.gameObject.tag == "EnemyCoin") { //if enemies did not steal coin their coin is worth less
            FindObjectOfType<ScoreCounter>().incrementScore(scoreInc/2);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "GoldBar") //same as coin with 2x value to score
        {
            FindObjectOfType<AudioTriggers>().PlayCoinCollect();
            FindObjectOfType<ScoreCounter>().incrementScore(scoreInc *2);
            Destroy(collision.gameObject);
        }

        // References items with the tag toy and increases score by set amount of 50 points
        if (collision.gameObject.tag == "Toy") //same as coin with 5x value to score
        {
            FindObjectOfType<ScoreCounter>().incrementScore(scoreInc *5);
            Destroy(collision.gameObject);
        }

    }

}
