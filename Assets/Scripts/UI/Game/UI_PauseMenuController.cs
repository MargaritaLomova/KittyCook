using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_PauseMenuController : UI_SettingsMenuController
{
    [Space]
    [Header("Specific Pause Components")]
    [SerializeField]
    private Button exitToMainMenuButton;

    protected override void Start()
    {
        base.Start();

        exitToMainMenuButton.onClick.AddListener(OnExitToMainMenuButtonClicked);
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void OnExitToMainMenuButtonClicked()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
    }
}