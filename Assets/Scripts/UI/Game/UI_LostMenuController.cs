using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_LostMenuController : UI_PanelController
{
    [Header("Components")]
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private Button restartButton;

    private void Start()
    {
        mainMenuButton.onClick.AddListener(OnExitToMainMenuButtonClicked);
        restartButton.onClick.AddListener(OnRestartButtonClicked);

        gameObject.SetActive(false);
    }

    private void OnExitToMainMenuButtonClicked()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
    }

    private void OnRestartButtonClicked()
    {
        UI_LoadingController.Get.Show();

        GameController gameController = FindObjectOfType<GameController>();
        var currentInfo = gameController.CurrentInfo;

        gameController.StartNewLevel(currentInfo);

        UI_LoadingController.Get.Hide();

        Hide();
    }
}