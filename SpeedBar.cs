using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBar : MonoBehaviour
{

	public Slider slider;
	public Gradient gradient;
	public Image fill;

	public int maxSpeed = 100;
	public int currentSpeed;


	// Start is called before the first frame update
	void Start()
	{
		currentSpeed = maxSpeed;
		SetMaxSpeed(maxSpeed);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Y))
		{
			TakeDamage(20);
		}
	}


	public void SetMaxSpeed(int speed)
	{
		slider.maxValue = speed;
		slider.value = speed;

		fill.color = gradient.Evaluate(1f);


	}

	public void SetSpeed(int speed)
	{
		slider.value = speed;

		fill.color = gradient.Evaluate(slider.normalizedValue);


	}

	void TakeDamage(int damage)
	{
		currentSpeed -= damage;

		SetSpeed(currentSpeed);
	}
}


