using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// DESIGN PATTERN: SINGLETON
	public static GameManager Instance { get; private set; }
	public UIManager UIManager { get; private set; }
	//public HighScoreSystem HighScoreSystem { get; private set; }

	public AudioManager AudioManager { get; private set; }

	[HideInInspector] public static bool IsGamePaused { get; set; } = false;

	static AudioSource backgroundMusic;

	private static float countdownTimerDefault = 121;
	//private static float secondsTimer = countdownTimerDefault;
	private static float secondsTimer = 0;
	private static int score;

	void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
			return;
		}
		Instance = this;

		UIManager = GetComponent<UIManager>();
		AudioManager = GetComponent<AudioManager>();
		//HighScoreSystem = GetComponent<HighScoreSystem>();
	}

	void Start()
	{
		if (SceneManager.GetActiveScene().name == "MainMenu")
		{
			SetupMainMenu();
		}
		else if (SceneManager.GetActiveScene().name == "Level")
		{
			SetupLevel();
		}

		Cursor.lockState = CursorLockMode.Confined;
	}

	void Update()
	{
		secondsTimer += Time.deltaTime;
		Instance.UIManager.UpdateTimeUI(secondsTimer);
		if (secondsTimer <= 0)
		{
			GameOver();

		}

		EscapeKeyAction();

	}

	public void EscapeKeyAction()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (SceneManager.GetActiveScene().name == "MainMenu")
			{
				SetupMainMenu();
			}
			else
			{
				if (IsGamePaused)
				{
					ResumeGameplay();
				}
				else
				{
					PauseGameplay();
				}
			}
		}
	}

	public static string GetScoreText()
    {
        return score.ToString();
    }

    public static void IncrementScore(int value)
    {
        score += value;
        Instance.UIManager.UpdateScoreUI(score);
        Debug.Log("Score: " + score);
    }

    public static void ResetGame()
    {
		ResetPlayerUI();
        backgroundMusic.Play();
		//secondsTimer = countdownTimerDefault;
		secondsTimer = 0;
		Time.timeScale = 1f;
		//Instance.UIManager.Reset();

	}

    private static void ResetPlayerUI()
    {
        score = 0;
        Instance.UIManager.UpdateScoreUI(score);

		secondsTimer = 0;
		Instance.UIManager.UpdateTimeUI(secondsTimer);

		IsGamePaused = false;
		Instance.UIManager.HidePauseMenu();
	}

    public void GameOver()
    {
        Time.timeScale = 0f;
		AudioManager.Instance.StopAllSound();
		Instance.UIManager.ActivateEndGame(secondsTimer);
		//HighScoreSystem.CheckHighScore("Anon", score);
	}

    public static void LoadMainMenu()
    {
        SetupMainMenu();
		SceneManager.LoadScene("MainMenu");
    }

    public static void LoadLevel()
    {
        SetupLevel();
		SceneManager.LoadScene("Level");
    }

    public static void SetupMainMenu()
    {
		Cursor.visible = true;

		Instance.UIManager.HideUIPanel();
        Instance.UIManager.HideGameOver();
		Instance.UIManager.HidePauseMenu();

		AudioManager.Instance.PlayBackgroundMusic();

        ResumeTime();
	}

	public static void SetupLevel()
	{
		ResetPlayerUI();

		Instance.UIManager.ShowUIPanel();
		Instance.UIManager.HideTitle();
		Instance.UIManager.HideGameOver();

		//AudioManager.Instance.PlayAtPoint("LevelMusic");
		AudioManager.Instance.PlayBackgroundMusic();
		//backgroundMusic.Play();

		ResumeGameplay();
	}


	public static void PauseTime()
	{
		Time.timeScale = 0f;
	}

	public static void ResumeTime()
	{
		Time.timeScale = 1f;
	}

    public static void PauseGameplay()
    {
		PauseTime();
		Cursor.visible = true;
		Instance.UIManager.ShowPauseMenu();
		IsGamePaused = true;
	}

	public static void ResumeGameplay()
	{
		ResumeTime();
		Cursor.visible = false;
		Instance.UIManager.HidePauseMenu();
		IsGamePaused = false;
	}

	public void PauseOptionsMenuOpen()
	{
		Instance.UIManager.ShowPauseOptions();
	}

	public void PauseOptionsMenuClose()
	{
		Instance.UIManager.HidePauseOptions();
	}

	public static void QuitGame()
	{
		Debug.Log("QUIT GAME");
		Application.Quit();
	}

}