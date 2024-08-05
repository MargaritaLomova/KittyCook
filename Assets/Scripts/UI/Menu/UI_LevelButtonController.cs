using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_LevelButtonController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private TMP_Text levelNumber;
    [SerializeField]
    private Button button;

    private LevelInfo currentInfo;

    private void Start()
    {
        button.onClick.AddListener(OnLevelClicked);
    }

    public void Set(LevelInfo info)
    {
        currentInfo = info;

        levelNumber.text = $"{currentInfo.Index}";
    }

    private void OnLevelClicked()
    {
        StartCoroutine(OpenGameCoroutine());
    }

    private IEnumerator OpenGameCoroutine()
    {
        UI_LoadingController.Get.Show();

        var gameScene = SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);

        yield return new WaitWhile(() => !gameScene.isDone);

        GameController gameController = FindObjectOfType<GameController>();
        while (gameController == null)
        {
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
            gameController = FindObjectOfType<GameController>();
        }
        gameController.SetCurrentLevelInfo(currentInfo);

        UI_LoadingController.Get.Hide();

        SceneManager.UnloadSceneAsync("Menu");
    }
}