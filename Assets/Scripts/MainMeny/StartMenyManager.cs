using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenyManager : MonoBehaviour
{
    public Button StartButton;
    public Button QuitButton;
    public Button EditorsButton;
    public Button SettingsButton;
    public TMP_Text[] highScoreTexts;

    void Start()
    {
        StartButton.onClick.AddListener(StartGame);
        QuitButton.onClick.AddListener(QuitGame);
        EditorsButton.onClick.AddListener(Editors);
        SettingsButton.onClick.AddListener(Settings);
        Time.timeScale = 1f;
        updateScoreBoard();
    }
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Editors()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Editor");
    }
    public void Settings()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Settings");
        Time.timeScale = 1f;
    }

    public void updateScoreBoard()
    {
        //load in json file, sort it, and dispay top 10 scores(only save top 10 scores, and it has name, score, time survived)
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "highscores.json");
        if (!System.IO.File.Exists(path))
        {
            Debug.Log("No high score file found");
            return;
        }
        string json = System.IO.File.ReadAllText(path);
        HighScoreList highScores = JsonUtility.FromJson<HighScoreList>(json);
        highScores.scores.Sort((x, y) => y.score.CompareTo(x.score));
        for (int i = 0; i < highScoreTexts.Length; i++)
        {
            if (i < highScores.scores.Count)
            {
                HighScoreEntry entry = highScores.scores[i];
                highScoreTexts[i].text = $"{i + 1}. {entry.name} - Score: {entry.score}, Time: {FormatTime(entry.time)}";
            }
            else
            {
                highScoreTexts[i].text = $"{i + 1}. ---";
            }
        }

    }
    private string FormatTime(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        return $"{minutes:D2}:{seconds:D2}";
    }
}
