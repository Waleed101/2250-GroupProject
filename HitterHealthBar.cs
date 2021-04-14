using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HitterHealthBar : MonoBehaviour
{


    // Slider so health goes down when damage is taken
    public Slider slider;

    // Health bar changes color as health goes down
    public Gradient gradient;

    // Sets the fill of each health bar 
    public Image fill;

    // Method to set starting health of the health bar
    public void SetStartingHealth(float health)

    {
        // Sliders starting maximum health value
        slider.maxValue = health;

        //Sliders starting health value
        slider.value = health;

        // References health bar gradient 
        fill.color = gradient.Evaluate(1f);
    }


    // Method to set the current health of the health bar 
    public void SetHealth(float health)
    {
        // Sliders current health value
        slider.value = health;

        // Health bars color
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
