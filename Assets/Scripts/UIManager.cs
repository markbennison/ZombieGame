
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreUI;
    [SerializeField] private TextMeshProUGUI timeValue;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject StatsPanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject GameCompletePanel;

    private static float secondsTimer = 0f;
    private int score;
    bool switchbtn;

    public void ExitButton()
    {
        Debug.Log("Application Quits");
        Application.Quit();

    }

    public void TryAgain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
        StatsPanel.SetActive(true);
        GameOverPanel.SetActive(false);
        GameCompletePanel.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
        StatsPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        GameCompletePanel.SetActive(false);
    }

    void Start()
    {
        score = 0;
        secondsTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreUI.text = "Score: "+score.ToString();

        secondsTimer += Time.deltaTime;
        UpdateTimeUI(secondsTimer);
        if (secondsTimer <= 0)
        {
            GameOver();

        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switchbtn = !switchbtn;

            if(switchbtn)
            {
                Pause();
            }
        }
    }

    public void UpdateScore()
    {
        score++;
    }

    public void UpdateTimeUI(float time)
    {
        int seconds = (int)time;
        timeValue.text = secondsTimer.ToString("00:00");
    }

    void GameOver()
    {
        PausePanel.SetActive(false);
        StatsPanel.SetActive(false);
        GameCompletePanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }
}
