using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
	[SerializeField] float maxHitPoints = 100f;
	[SerializeField] private GameObject StatsUI;
	[SerializeField] private GameObject DeathUI;
	float hitPoints;

	public Slider healthSlider;
	private Animator animator;
	public bool PlayerAlive;
	public bool MaxHP;

	void Start()
	{
		hitPoints = maxHitPoints;
		animator = GetComponent<Animator>();
        PlayerAlive = true;
    }

    private void Update()
    {
        if(hitPoints >= maxHitPoints)
		{
			hitPoints = maxHitPoints;
			MaxHP = true;
		}
		else
		{
			MaxHP = false;
		}

		SetHealthSlider();

    }

    void Hit(float rawDamage)
	{
		hitPoints -= rawDamage;

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
        PlayerAlive = false;
        animator.SetTrigger("OnDeath");
        StatsUI.SetActive(false);
        DeathUI.SetActive(true);
    }

	public void Heal(float amount)
	{
        hitPoints += amount;
    }

}