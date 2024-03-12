using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
	float countdownTimer = 1.5f;
	[SerializeField] GameObject titleObject;

	private void Start()
	{
		titleObject.SetActive(false);
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log("Application Quits");
			Application.Quit();
		}

		countdownTimer -= Time.deltaTime;
		if(countdownTimer < 0)
		{
			countdownTimer = 0;
			titleObject.SetActive(true);
		}
	}

	public void StartButton()
	{
		Cursor.visible = false;
		SceneManager.LoadScene("Level");
	}
}
