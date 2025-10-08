using UnityEngine;
using UnityEngine.UI;


public class SettingManager : MonoBehaviour
{

    public Button backButton;


    void Start()
    {
        backButton.onClick.AddListener(BackToMainMenu);
    }

    void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMeny");
    }
}
