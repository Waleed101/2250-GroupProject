using System.Collections;
using System.Collections.Generic; //imported libraries
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ButtonHandler : MonoBehaviour
{
    public GameObject potionCanvas; //initiating variables and game objects
    public static int potionAmount;
    public static List<string> potionInventory = new List<string>(); //list to store the type of potion
    public int scoreInc = 0;
    
    public void SetTextHealth(string text) //method controlling health btn
    {
        if (ScoreCounter._score >= 100) //verifies number of coins is enough
        {
            GameObject.FindGameObjectWithTag("HealthPurchase").GetComponent<TextMeshProUGUI>().text = text;//change text to "purchased"
            potionAmount++;//add potion
            potionInventory.Add("HealthPotion");//add a health potion to the list
            FindObjectOfType<ScoreCounter>().incrementScore(scoreInc);//decreases the score by the set val
            GameObject.FindGameObjectWithTag("GameController").GetComponent<DayLevelManager>().TaskCompleted();//tells game potion task completed
            gameObject.GetComponent<Button>().interactable = false;

        }
    }

    public void SetTextStrength(string text)//method controlling strength btn
    {
        if (ScoreCounter._score >= 100)//verifies number of coins is enough
        {
            GameObject.FindGameObjectWithTag("StrengthPurchase").GetComponent<TextMeshProUGUI>().text = text;//change text to "purchased"
            potionAmount++;//add potion
            potionInventory.Add("StrengthPotion");//add a strength potion to the list
            print("Number of Potions" + potionAmount);//decreases the score by the set val
            FindObjectOfType<ScoreCounter>().incrementScore(scoreInc);//tells game potion task completed
            gameObject.GetComponent<Button>().interactable = false;

        }
    }
    public void SetTextSpeed(string text)//method controlling speed btn
    {
        if (ScoreCounter._score >= 100)//verifies number of coins is enough
        {
            GameObject.FindGameObjectWithTag("SpeedPurchase").GetComponent<TextMeshProUGUI>().text = text;//change text to "purchased"
            potionAmount++;//add potion
            potionInventory.Add("SpeedPotion");//add a speed potion to the list
            print("Number of Potions" + potionAmount);//decreases the score by the set val
            FindObjectOfType<ScoreCounter>().incrementScore(scoreInc);//tells game potion task completed
            gameObject.GetComponent<Button>().interactable = false;

        }
    }
    public void SetTextQuickRevive(string text)//method controlling QR btn
    {
        if (ScoreCounter._score >= 100)//verifies number of coins is enough
        {

            GameObject.FindGameObjectWithTag("QuickRevivePurchase").GetComponent<TextMeshProUGUI>().text = text;//change text to "purchased"
            potionAmount++;//add potion
            potionInventory.Add("QuickRevive");//add a QR potion to the list
            print("Number of Potions: " + potionAmount);//decreases the score by the set val
            FindObjectOfType<ScoreCounter>().incrementScore(scoreInc);//tells game potion task completed
            gameObject.GetComponent<Button>().interactable = false;

        }

    }
    public void CloseWindow() //closes the potion shop window
    {
        potionCanvas.SetActive(false);
        Screen.lockCursor = true;
    }
    
}
