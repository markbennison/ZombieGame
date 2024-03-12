using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject uIPanel;
    [SerializeField] TextMeshProUGUI scoreValue;
	[SerializeField] TextMeshProUGUI timeValue;
	[SerializeField] GameObject gameOverPanel;
	[SerializeField] TextMeshProUGUI endScoreValue;
    [SerializeField] GameObject TitleObject;

    int score = 0;

	void Start()
	{
		UpdateScoreUI(0);
		UpdateTimeUI(0);
	}

	bool TrySetField(string objectName, ref TextMeshProUGUI field)
	{
		if(field != null)
		{
			return true;
		}

		field = GameObject.Find(objectName).GetComponent<TextMeshProUGUI>();

		if (field != null)
		{
			return true;
		}

		return false;
	}

	bool TrySetField(string objectName, ref GameObject field)
	{
		if (field != null)
		{
			return true;
		}

		field = GameObject.Find(objectName);

		if (field != null)
		{
			return true;
		}

		return false;
	}

	public void UpdateScoreUI(int value)
	{
		// "D5" - minimum of 5 digits, preceding shorter numbers with 0s
		scoreValue.text = value.ToString("D3");
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

	public void ActivateEndGame(float time)
	{
		endScoreValue.text = System.TimeSpan.FromSeconds(time).ToString("mm':'ss");
		gameOverPanel.SetActive(true);
		Cursor.visible = true;
	}

	public void Reset()
	{
		Debug.Log("RESET UI");
		Debug.Log("1" + TrySetField("Score", ref scoreValue));
		Debug.Log("2" + TrySetField("Time", ref timeValue));
		Debug.Log("3" + TrySetField("GameOverPanel", ref gameOverPanel));
		Debug.Log("4" + TrySetField("ScoreValue", ref endScoreValue));
	}

	public void ShowPanel()
	{
		uIPanel.SetActive(true);
        TitleObject.SetActive(false);
    }

    public void HidePanel()
    {
        uIPanel.SetActive(false);
    }

    public void ShowTitle()
	{
        TitleObject.SetActive(true);
    }

    public void HideTitle()
    {
        TitleObject.SetActive(false);
    }

	public void ShowGameOver()
	{
        gameOverPanel.SetActive(true);
    }

    public void HideGameOver()
    {
        gameOverPanel.SetActive(false);
    }

}