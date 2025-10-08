using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class EscapeManager : MonoBehaviour
{
    public GameObject escapeMenu;
    public Button resumeButton;
    public Button optionsButton;
    public Button mainMenuButton;
    public PlayerInput playerInput;
    private InputAction escapeAction;
    void Start()
    {
        escapeMenu.SetActive(false);
        resumeButton.onClick.AddListener(CloseEscapeMenu);
        optionsButton.onClick.AddListener(OpenOptionsMenu);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);

        escapeAction = playerInput.actions["Escape"];
        escapeAction.performed += OnEscapePressed;
    }
    public void OpenEscapeMenu()
    {
        escapeMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseEscapeMenu()
    {
        escapeMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    private void OpenOptionsMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Settings");
    }
    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMeny");
    }

    private void OnEscapePressed(InputAction.CallbackContext context)
    {
        if (escapeMenu.activeSelf)
        {
            CloseEscapeMenu();
        }
        else
        {
            OpenEscapeMenu();
        }
    }

    private void OnDestroy()
    {
        if (escapeAction != null)
        {
            escapeAction.performed -= OnEscapePressed;
        }
    }

}
