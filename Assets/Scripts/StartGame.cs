using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log("Application Quits");
			Application.Quit();
		}
	}

	public void StartButton()
	{
		Cursor.visible = false;
		SceneManager.LoadScene("Level");
	}
}
