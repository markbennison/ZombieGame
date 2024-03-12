using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			ExitButton();
		}
	}

	public void ExitButton()
	{
		//Debug.Log("Application Quits");
		//Application.Quit();
		//SceneManager.LoadScene("MainMenu");

		GameManager.LoadMainMenu();
	}



}
