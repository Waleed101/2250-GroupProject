using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{

	public Slider slider;
	public Gradient gradient;
	public Image fill;

	public int maxHealth = 100;
	public int currentHealth;


	// Start is called before the first frame update
	void Start()
	{
		currentHealth = maxHealth;
		SetMaxHealth(maxHealth);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.H))
		{
			TakeDamage(20);
		}
	}


	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(1f);


	}

	public void SetHealth(int health)
	{
		slider.value = health;

		fill.color = gradient.Evaluate(slider.normalizedValue);


	}

	void TakeDamage(int damage)
	{
		currentHealth -= damage;

		SetHealth(currentHealth);
	}
}


