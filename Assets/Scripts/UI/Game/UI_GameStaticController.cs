using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameStaticController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private TMP_Text moneyText;
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private Button hintButton;

    [Space]
    [Header("World Components")]
    [SerializeField]
    private UI_PauseMenuController pauseMenu;
    [SerializeField]
    private UI_HintMenuController hintMenu;

    private void Start()
    {
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
        hintButton.onClick.AddListener(OnHintButtonClicked);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            pauseMenu.gameObject.SetActive(true);
    }

    private void UpdateMoney(int moneyCount)
    {
        moneyText.text = $"{moneyCount}";
    }

    private void OnPauseButtonClicked()
    {
        pauseMenu.gameObject.SetActive(true);
    }

    private void OnHintButtonClicked()
    {
        hintMenu.gameObject.SetActive(true);
        //hintMenu.Set();
    }
}