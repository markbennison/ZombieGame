using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
	float countdownTimer = 1.5f;

	private void Start()
	{
        GameManager.Instance.UIManager.HideTitle();
        GameManager.SetupMainMenu();

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
			GameManager.Instance.UIManager.ShowTitle();
		}
	}

	public void StartButton()
	{
		GameManager.LoadLevel();
	}
}
