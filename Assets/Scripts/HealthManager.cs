using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
	[SerializeField] float maxHitPoints = 100f;
	float hitPoints;

	public Slider healthSlider;


    Animator animator;
    const string ANIM_TRIG_ONDEATH = "OnDeath";
    const string ANIM_TRIG_ONHIT = "OnHit";

    void Start()
	{
		hitPoints = maxHitPoints;
        animator = GetComponent<Animator>();
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

	public bool IsDead()
	{
		if (hitPoints <= 0)
		{
			return true;
		}
		return false;
	}

	float NormalisedHitPoint()
	{
		return hitPoints / maxHitPoints;
	}

	void OnDeath()
	{
        animator.SetTrigger(ANIM_TRIG_ONDEATH);
		Invoke("CallGameOver", 2f);
	}

	void CallGameOver()
	{
		GameManager.Instance.GameOver();

    }
}