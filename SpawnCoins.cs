using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SpawnCoins : MonoBehaviour
{

    public GameObject Coins;
    private int score;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {

        score = 0;
        GameObject temp = Instantiate(Coins);
        temp.transform.position = new Vector3(0f, 0f, 0f);
        SetScoreText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin1"))
        {
            // set games acive state to false
            other.gameObject.SetActive(false);
            score = score + 1;
            SetScoreText();
        }

        if (other.gameObject.CompareTag("Coin2"))
        {
            other.gameObject.SetActive(false);
            score = score + 2;
            SetScoreText();
        }

        if (other.gameObject.CompareTag("Coin3"))
        {
            other.gameObject.SetActive(false);
            score = score + 3;
            SetScoreText();
        }

        if (other.gameObject.CompareTag("Coin3"))
        {
            other.gameObject.SetActive(false);
            score = score + 4;
            SetScoreText();
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();



    }
}