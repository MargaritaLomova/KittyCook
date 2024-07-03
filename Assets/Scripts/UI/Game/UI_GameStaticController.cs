using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameStaticController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private TMP_Text moneyText;
    [SerializeField]
    private TMP_Text plusMoneyText;
    [SerializeField]
    private TMP_Text minusMoneyText;
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

    [Space]
    [Header("Variables")]
    [SerializeField]
    private float animationDuration = 1f;

    public static UI_GameStaticController Get { get; private set; }

    private void Awake()
    {
        Get = this;
    }

    private void Start()
    {
        plusMoneyText.alpha = 0f;
        minusMoneyText.alpha = 0f;

        pauseButton.onClick.AddListener(OnPauseButtonClicked);
        hintButton.onClick.AddListener(OnHintButtonClicked);

        UpdateMoneyImmediatly(MoneyController.Get.Money);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            pauseMenu.gameObject.SetActive(true);
    }

    public void AddMoney(int count)
    {
        plusMoneyText.text = $"+{count}";
        MoneyController.Get.AddMoney(count);
        ChangeMoney(plusMoneyText);
    }

    public void RemoveMoney(int count)
    {
        minusMoneyText.text = $"-{count}";
        MoneyController.Get.RemoveMoney(count);
        ChangeMoney(minusMoneyText);
    }

    private void ChangeMoney(TMP_Text changeText)
    {
        var rectTransform = changeText.GetComponent<RectTransform>();
        changeText.alpha = 0f;

        var upYPos = rectTransform.anchoredPosition.y;
        var downYPos = upYPos - 90f;
        rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x, downYPos);

        rectTransform.DOAnchorPosY(upYPos, animationDuration);
        changeText.DOFade(1f, animationDuration).OnComplete(() =>
        {
            rectTransform.DOAnchorPosY(downYPos, animationDuration);
            changeText.DOFade(0f, animationDuration);

            StartCoroutine(UpdateMoney());
        });
    }

    private void UpdateMoneyImmediatly(int moneyCount)
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
    }

    private IEnumerator UpdateMoney()
    {
        var timeForWait = 0.05f;
        //Adding
        if (MoneyController.Get.Money > Convert.ToInt32(moneyText.text))
        {
            while (Convert.ToInt32(moneyText.text) < MoneyController.Get.Money)
            {
                moneyText.text = $"{Convert.ToInt32(moneyText.text) + 1}";
                yield return new WaitForSecondsRealtime(timeForWait);
            }
        }
        //Minus
        else
        {
            while (Convert.ToInt32(moneyText.text) > MoneyController.Get.Money)
            {
                moneyText.text = $"{Convert.ToInt32(moneyText.text) - 1}";
                yield return new WaitForSecondsRealtime(timeForWait);
            }
        }
    }
}