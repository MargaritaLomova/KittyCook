using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MenuController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button levelsButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button exitButton;

    [Space]
    [Header("Other")]
    [SerializeField]
    private UI_SettingsMenuController settings;
    [SerializeField]
    private UI_LevelsMenuController levels;

    private void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        levelsButton.onClick.AddListener(OnLevelsButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
    }

    private void OnLevelsButtonClicked()
    {
        gameObject.SetActive(false);
        levels.gameObject.SetActive(true);
    }

    private void OnSettingsButtonClicked()
    {
        settings.Show();
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }
}