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
		backgroundMusic = GetComponent<AudioSource>();
		//HighScoreSystem = GetComponent<HighScoreSystem>();
	}

	void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;
		ResetGame();
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
        ResetScore();
        ResetTime();
        backgroundMusic.Play();
		//secondsTimer = countdownTimerDefault;
		secondsTimer = 0;
		Time.timeScale = 1f;
		//Instance.UIManager.Reset();

	}

    private static void ResetScore()
    {
        score = 0;
        Instance.UIManager.UpdateScoreUI(score);
    }

    private static void ResetTime()
    {
        secondsTimer = 0;
        Instance.UIManager.UpdateTimeUI(secondsTimer);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
		backgroundMusic.Stop();
		Instance.UIManager.ActivateEndGame(secondsTimer);
		//HighScoreSystem.CheckHighScore("Anon", score);
	}

    public static void LoadMainMenu()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }

    public static void LoadLevel()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("Level");
    }

    public static void SetupMainMenu()
    {
        Instance.UIManager.HidePanel();
        Instance.UIManager.HideGameOver();


    }

    public static void SetupLevel()
    {
        Instance.UIManager.ShowPanel();
        Instance.UIManager.HideTitle();
        Instance.UIManager.HideGameOver();
        ResetGame();
    }

}