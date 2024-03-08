using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartZone : MonoBehaviour
{
	void Start()
	{
		Time.timeScale = 1.0f;
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Player")
		{
			Cursor.visible = false;
			SceneManager.LoadScene("Level");
		}
	}
}
