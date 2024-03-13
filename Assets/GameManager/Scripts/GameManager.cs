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
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            SetupMainMenu();
		}
        else if (SceneManager.GetActiveScene().name == "Level")
		{
			SetupLevel();
		}
        
	}

	void Update()
    {
		secondsTimer += Time.deltaTime;
        Instance.UIManager.UpdateTimeUI(secondsTimer);
        if(secondsTimer <= 0)
        {
            GameOver();

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

		AudioManager.Instance.PlayBackgroundMusic();

        Resume();
	}

    public static void SetupLevel()
    {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;

		ResetPlayerUI();

		Instance.UIManager.ShowUIPanel();
        Instance.UIManager.HideTitle();
        Instance.UIManager.HideGameOver();

        //AudioManager.Instance.PlayAtPoint("LevelMusic");
        AudioManager.Instance.PlayBackgroundMusic();
		//backgroundMusic.Play();


		Resume();
	}

	public static void Pause()
	{
		Time.timeScale = 0f;
	}

	public static void Resume()
	{
		Time.timeScale = 1f;
	}
}