using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEffects : MonoBehaviour
{
    public GameObject HealthDrank, SpeedDrank, StrengthDrank, QuickReviveDrank, Animation;
     public void AffectPlayer(string potionType) //changes the characters attributes with potion drinking and shows UI specific to each potion
             
    {
        switch (potionType) //determine potion type
        {
            // Health increments by 20
            case "HealthPotion":
                FindObjectOfType<HealthManager>().IncrementCurrentHealth(20); 
                HealthDrank.SetActive(true); 
                Animation.SetActive(true); //play animation while drinking the potion
                ActionAfterDelay.Create(2.0f, () => { //UI and animation last 2 after the potion drinking is called
                    HealthDrank.SetActive(false);
                    Animation.SetActive(false);
                });
                break;

                // Increases stength by 20
            case "StrengthPotion":
                FindObjectOfType<EnemyHit>().IncrementStrength(10);
                StrengthDrank.SetActive(true);
                Animation.SetActive(true);//play animation while drinking the potion
                ActionAfterDelay.Create(2.0f, () => {//UI and animation last 2 after the potion drinking is called
                    StrengthDrank.SetActive(false);
                    Animation.SetActive(false);
                });
                break;

                // Increases speed by 2
            case "SpeedPotion":
                FindObjectOfType<ThirdPersonMovement>().IncrementSpeed(2);
                SpeedDrank.SetActive(true);
                Animation.SetActive(true);//play animation while drinking the potion
                ActionAfterDelay.Create(2.0f, () => { //UI and animation last 2 after the potion drinking is called
                    SpeedDrank.SetActive(false);
                    Animation.SetActive(false);
                });
                break;
            case "QuickRevive": //extra life if the player dies
                FindObjectOfType<HealthManager>().quickRevive = true;
                QuickReviveDrank.SetActive(true);
                Animation.SetActive(true);//play animation while drinking the potion
                ActionAfterDelay.Create(2.0f, () => {//UI and animation last 2 after the potion drinking is called
                    QuickReviveDrank.SetActive(false);
                    Animation.SetActive(true);
                });
                break;
        }
    }
}
