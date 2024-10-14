using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_WinMenuController : UI_PanelController
{
    [Header("Components")]
    [SerializeField]
    private UI_StarController[] stars;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private Button nextButton;

    private void Start()
    {
        mainMenuButton.onClick.AddListener(OnExitToMainMenuButtonClicked);
        nextButton.onClick.AddListener(OnNextButtonClicked);

        gameObject.SetActive(false);
    }

    public void Show(int countOfStars)
    {
        base.Show();

        KittyCook.Helpers.Timer.Instance.WaitUntil(() => ShowAnimationEnded, () =>
        {
            for (int i = 0; i < countOfStars; i++)
            {
                var currentStar = stars[i];
                KittyCook.Helpers.Timer.Instance.WaitForSeconds((i + 1) * 0.3f, () =>
                {
                    currentStar.ShowStartAnimation();
                });
            }
        });
    }

    private void OnExitToMainMenuButtonClicked()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
    }

    private void OnNextButtonClicked()
    {
        UI_LoadingController.Get.Show();

        GameController gameController = FindObjectOfType<GameController>();
        var currentInfo = gameController.CurrentInfo;

        if (LevelsInfo.Get.Levels.Count > currentInfo.Index)
        {
            var nextInfo = LevelsInfo.Get.Levels[currentInfo.Index];

            if (nextInfo != null)
            {
                gameController.StartNewLevel(nextInfo);
            }
        }

        UI_LoadingController.Get.Hide();

        Hide();
    }
}