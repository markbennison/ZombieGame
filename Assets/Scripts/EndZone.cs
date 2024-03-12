using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    private void Start()
    {
       GameManager.SetupLevel();
    }

    void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Player")
		{
			Debug.Log("Win Condition");
			GameManager.Instance.GameOver();
		}
	}
	
}
