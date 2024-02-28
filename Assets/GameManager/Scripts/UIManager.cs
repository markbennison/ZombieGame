using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI scoreValue;
	[SerializeField] TextMeshProUGUI timeValue;
	[SerializeField] GameObject gameOverPanel;
	[SerializeField] TextMeshProUGUI endScoreValue;



	int score = 0;

	void Start()
	{
		UpdateScoreUI(0);
		UpdateTimeUI(0);
	}

	public void UpdateScoreUI(int value)
	{
		// "D5" - minimum of 5 digits, preceding shorter numbers with 0s
		scoreValue.text = value.ToString("D2");
	}

	public void UpdateTimeUI(float time)
	{
		int seconds = (int)time;
		timeValue.text = System.TimeSpan.FromSeconds(seconds).ToString("mm':'ss");
	}

	public void ActivateEndGame(int score)
	{
		endScoreValue.text = score.ToString();
		gameOverPanel.SetActive(true);
		Cursor.visible = true;
	}

}