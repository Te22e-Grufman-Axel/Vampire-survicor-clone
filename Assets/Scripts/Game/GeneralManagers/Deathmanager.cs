using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class Deathmanager : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject newHighScoreText;
    public TMP_Text ScoreText;
    public TMP_Text TimeText;
    public Button Restart1;
    public Button Restart2;
    public Button SaveScoreButton;
    private int minutes;
    private int seconds;
    private bool HasSaved = false;

    [Header("EnterYourName")]
    public GameObject enterYourNamePanel;
    public TMP_InputField nameInputField;
    public Button SubmitNameButton;
    void Start()
    {
        deathScreen.SetActive(false);
        Restart1.onClick.AddListener(RestartGame);
        Restart2.onClick.AddListener(RestartGame);
        SaveScoreButton.onClick.AddListener(OpenEnterYourNamePanel);
        SaveScoreButton.gameObject.SetActive(false);
        Restart1.gameObject.SetActive(false);
        Restart2.gameObject.SetActive(false);
        newHighScoreText.SetActive(false);
        SubmitNameButton.onClick.AddListener(SaveScore);
        enterYourNamePanel.SetActive(false);
        HasSaved = false;

    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        int score = FindFirstObjectByType<LevelManagerr>().GetScore();
        float timeSurvived = Time.timeSinceLevelLoad;
        ScoreText.text = "Score: " + score.ToString();
        TimeText.text = "Time survived: " + FormatTime(timeSurvived);
        Time.timeScale = 0f;
        CheckIfTop10(score);
    }
    public void OpenEnterYourNamePanel()
    {
        enterYourNamePanel.SetActive(true);
    }

    private string FormatTime(float time)
    {
        minutes = Mathf.FloorToInt(time / 60F);
        seconds = Mathf.FloorToInt(time - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        return niceTime;
    }
    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMeny");
    }
    private void SaveScore()
    {
        if (HasSaved) return;
        HasSaved = true;
        int score = FindFirstObjectByType<LevelManagerr>().GetScore();
        float timeSurvived = Time.timeSinceLevelLoad;
        string playerName = nameInputField.text;
        if (string.IsNullOrWhiteSpace(playerName))
        {
            playerName = "Player";
        }
        SaveHighScoreToFile(playerName, score, (int)timeSurvived);
        RestartGame();
    }


    private void SaveHighScoreToFile(string playerName, int score, int timeSurvived)
    {
        try
        {
            string filePath = Application.streamingAssetsPath + "/highscores.json";


            HighScoreList highScores;

            if (File.Exists(filePath))
            {
                string existingJson = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(existingJson))
                {
                    highScores = JsonUtility.FromJson<HighScoreList>(existingJson);
                }
                else
                {
                    highScores = new HighScoreList();
                }
            }
            else
            {
                highScores = new HighScoreList();
            }

            HighScoreEntry newEntry = new HighScoreEntry
            {
                name = playerName,
                score = score,
                time = timeSurvived
            };

            highScores.scores.Add(newEntry);

            highScores.scores.Sort((x, y) => y.score.CompareTo(x.score));

            if (highScores.scores.Count > 10)
            {
                highScores.scores.RemoveRange(10, highScores.scores.Count - 10);
            }

            string json = JsonUtility.ToJson(highScores, true);
            File.WriteAllText(filePath, json);

        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save high score: {e.Message}");
        }
    }


    private void CheckIfTop10(int score)
    {
        string filePath = Application.streamingAssetsPath + "/highscores.json";

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            HighScoreList highScores = JsonUtility.FromJson<HighScoreList>(existingJson);

            if (highScores.scores.Count < 10 || score > highScores.scores[highScores.scores.Count - 1].score)
            {
                NewHighScore();
            }
            else
            {
                NotNewHighScore();
            }
        }
        else
        {
            NewHighScore();
        }
    }
    private void NewHighScore()
    {
        newHighScoreText.SetActive(true);
        SaveScoreButton.gameObject.SetActive(true);
        Restart2.gameObject.SetActive(true);
        Restart1.gameObject.SetActive(false);
    }
    private void NotNewHighScore()
    {
        newHighScoreText.SetActive(false);
        SaveScoreButton.gameObject.SetActive(false);
        Restart2.gameObject.SetActive(false);
        Restart1.gameObject.SetActive(true);
    }
}
