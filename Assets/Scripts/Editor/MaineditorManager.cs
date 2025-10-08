using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


public class MaineditorManager : MonoBehaviour
{
    [SerializeField] private Button BackButton;
    [SerializeField] private TMP_Dropdown EditorDropdown;
    [SerializeField] private GameObject EnemyEditor;
    [SerializeField] private GameObject UpgradeEditor;
    [SerializeField] private GameObject WeaponsEditor;

    void Start()
    {
        BackButton.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene("MainMeny"));
        EnemyEditor.SetActive(true);
        UpgradeEditor.SetActive(false);
        WeaponsEditor.SetActive(false);
    }

    void Update()
    {
        switch (EditorDropdown.value)
        {
            case 0:
                EnemyEditor.SetActive(true);
                UpgradeEditor.SetActive(false);
                WeaponsEditor.SetActive(false);
                break;
            case 1:
                EnemyEditor.SetActive(false);
                UpgradeEditor.SetActive(true);
                WeaponsEditor.SetActive(false);
                break;
            case 2:
                EnemyEditor.SetActive(false);
                UpgradeEditor.SetActive(false);
                WeaponsEditor.SetActive(true);
                break;
            default:
                break;
        }
    }
}
