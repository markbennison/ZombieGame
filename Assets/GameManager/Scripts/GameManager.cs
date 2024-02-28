using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // DESIGN PATTERN: SINGLETON
    public static GameManager Instance { get; private set; }
    public UIManager UIManager { get; private set; }
	//public HighScoreSystem HighScoreSystem { get; private set; }

	static AudioSource backgroundMusic;

	private static float countdownTimerDefault = 121;
	private static float secondsTimer = countdownTimerDefault;
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
		secondsTimer -= Time.deltaTime;
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
        backgroundMusic.Play();
		secondsTimer = countdownTimerDefault;
		Time.timeScale = 1f;
    }

    private static void ResetScore()
    {
        score = 0;
        Instance.UIManager.UpdateScoreUI(score);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
		backgroundMusic.Stop();
		Instance.UIManager.ActivateEndGame(score);
        //HighScoreSystem.CheckHighScore("Anon", score);
    }
}