using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MenuController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button exitButton;

    [Space]
    [Header("Other")]
    [SerializeField]
    private UI_SettingsMenuController settings;

    private void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
    }

    private void OnSettingsButtonClicked()
    {
        settings.gameObject.SetActive(true);
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }
}