using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackZone : MonoBehaviour
{
	float countdownTimer = 0f;
	float countdownTimerLimit = 0.2f;
	void Update()
	{
		countdownTimer += Time.deltaTime;
		if(countdownTimer > countdownTimerLimit)
		{
			countdownTimer = 0f;
			gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			Debug.Log("TAKE THAT!");
			collision.SendMessageUpwards("Hit", 10f);
			countdownTimer = 0f;
			gameObject.SetActive(false);
		}
	}


}