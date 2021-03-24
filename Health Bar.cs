using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
	// Health bar slider
	public Slider slider;

	// Gradient for health bar
	public Gradient gradient;
	public Image fill;


	// Maximum health
	public int maxHealth = 100;
	public int currentHealth;


	// Start is called before the first frame update
	void Start()
	{

		// CurrentHealth is set to maximum health
		currentHealth = maxHealth;
		SetMaxHealth(maxHealth);
	}

	// Update is called once per frame
	void Update()
	{
		// When the key H is pushed 20 goes down 
		if (Input.GetKeyDown(KeyCode.H))
		{
			TakeDamage(20);
		}
	}


	// Maximum health set
	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;

		// color of health bar is set
		fill.color = gradient.Evaluate(1f);


	}

	public void SetHealth(int health)
	{
		slider.value = health;

		fill.color = gradient.Evaluate(slider.normalizedValue);


	}

	// Health bar goes down
	void TakeDamage(int damage)
	{
		currentHealth -= damage;

		SetHealth(currentHealth);
	}
}


