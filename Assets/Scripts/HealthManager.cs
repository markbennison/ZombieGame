using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
	[SerializeField] float maxHitPoints = 100f;
	float hitPoints;

	public Slider healthSlider;

	void Start()
	{
		hitPoints = maxHitPoints;
	}


	void Hit(float rawDamage)
	{
		hitPoints -= rawDamage;
		SetHealthSlider();

		Debug.Log("OUCH: " + hitPoints.ToString());
		AudioManager.instance.PlayAtPoint("PlayerGrunt", Camera.main.gameObject);

		if (hitPoints <= 0)
		{
			Debug.Log("TODO: GAME OVER - YOU DIED");
			OnDeath();
		}
	}

	void SetHealthSlider()
	{
		if (healthSlider != null)
		{
			healthSlider.value = NormalisedHitPoint();
		}
	}

	float NormalisedHitPoint()
	{
		return hitPoints / maxHitPoints;
	}

	void OnDeath()
	{
		GameManager.Instance.GameOver();
	}
}