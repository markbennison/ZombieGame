using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
	[SerializeField] float maxHitPoints = 100f;
	float hitPoints;

    Animator animator;
    const string ANIM_TRIG_ONDEATH = "OnDeath";
    const string ANIM_TRIG_ONHIT = "OnHit";

    void Start()
	{
		hitPoints = maxHitPoints;
		GameManager.Instance.UIManager.UpdateHealthSlider(NormalisedHitPoints());
		animator = GetComponent<Animator>();
    }


    void Hit(float rawDamage)
	{
		hitPoints -= rawDamage;
		GameManager.Instance.UIManager.UpdateHealthSlider(NormalisedHitPoints());

		Debug.Log("OUCH: " + hitPoints.ToString());
		AudioManager.Instance.PlayAtPoint("PlayerGrunt");

		if (hitPoints <= 0)
		{
			Debug.Log("TODO: GAME OVER - YOU DIED");
			OnDeath();
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

	float NormalisedHitPoints()
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