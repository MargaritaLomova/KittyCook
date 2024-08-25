using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_TutorialMenuController : UI_PanelController
{
    [Header("Components")]
    [SerializeField]
    private CanvasGroup background;
    [SerializeField]
    private Button dialogueHolder;
    [SerializeField]
    private TMP_Text speakerName;
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private Image speakerView;
    [SerializeField]
    private Animator speakerAnimator;
    [SerializeField]
    private Image highlight;

    public static UI_TutorialMenuController Get { get; private set; }

    private TutorialInfo currentInfo;
    private Coroutine phrasePrintCoroutine;
    private int currentTutorialStep;

    private void Awake()
    {
        Get = this;

        dialogueHolder.onClick.AddListener(NextStepOrEnd);

        gameObject.SetActive(false);
    }

    public override void Show()
    {
        background.alpha = 0;
        dialogueHolder.transform.localScale = new Vector3(0, 1, 1);
        speakerName.text = string.Empty;
        text.text = string.Empty;
        speakerView.color = new Color(speakerView.color.r, speakerView.color.g, speakerView.color.b, 0f);

        gameObject.SetActive(true);

        Time.timeScale = 0;

        background.DOFade(1, openCloseAnimationSpeed).SetUpdate(true);
        dialogueHolder.transform.DOScaleX(1, openCloseAnimationSpeed).OnComplete(() => speakerView.DOFade(1, openCloseAnimationSpeed).SetUpdate(true)).SetUpdate(true);

        PrintPhrase(0, true);
        ChangeHighlight();
    }

    public override void Hide()
    {
        background.alpha = 1;
        dialogueHolder.transform.localScale = new Vector3(1, 1, 1);
        speakerView.color = new Color(speakerView.color.r, speakerView.color.g, speakerView.color.b, 1f);

        speakerView.DOFade(0, openCloseAnimationSpeed).OnComplete(() =>
        {
            background.DOFade(0, openCloseAnimationSpeed).SetUpdate(true);
            dialogueHolder.transform.DOScaleX(0, openCloseAnimationSpeed).SetUpdate(true).OnComplete(() =>
            {
                Time.timeScale = 1;

                gameObject.SetActive(false);
            });
        }).SetUpdate(true);
    }

    public void SetTutorialPreset(TutorialInfo info)
    {
        currentInfo = info;

        speakerView.sprite = currentInfo.StartSpeakerIcon;
        speakerAnimator.runtimeAnimatorController = currentInfo.SpeakerAnimatorController;
    }

    private void NextStepOrEnd()
    {
        if (phrasePrintCoroutine != null)
        {
            StopCoroutine(phrasePrintCoroutine);
            phrasePrintCoroutine = null;

            text.text = currentInfo.Phrases[currentTutorialStep].PhraseText;

            return;
        }

        if (currentTutorialStep < currentInfo.Phrases.Length - 1)
        {
            text.text = string.Empty;

            PrintPhrase(currentTutorialStep + 1);
            ChangeHighlight();
        }
        else
        {
            Hide();
        }
    }

    private void ChangeHighlight()
    {
        var fullAlpha = 0.94f;
        if (currentInfo.Phrases[currentTutorialStep].HasHighlight)
        {
            if (highlight.color.a == 0)
            {
                highlight.DOFade(fullAlpha, openCloseAnimationSpeed).SetUpdate(true);
                background.GetComponent<Image>().DOFade(0, openCloseAnimationSpeed).SetUpdate(true);
            }

            StartCoroutine(DelayActivate(() => highlight.color.a == fullAlpha, () =>
            {
                highlight.transform.DOMove(currentInfo.Phrases[currentTutorialStep].HighlightPosition, openCloseAnimationSpeed).SetUpdate(true);
            }));
        }
        else
        {
            if(highlight.color.a > 0)
            {
                highlight.DOFade(0, openCloseAnimationSpeed).SetUpdate(true);
                background.GetComponent<Image>().DOFade(fullAlpha, openCloseAnimationSpeed).SetUpdate(true);
            }
        }
    }

    private void PrintPhrase(int phraseIndex, bool isFirst = false)
    {
        currentTutorialStep = phraseIndex;

        if (isFirst)
        {
            StartCoroutine(SlowlyPrintText(speakerName, currentInfo.SpeakerName));
        }

        phrasePrintCoroutine = StartCoroutine(SlowlyPrintText(text, currentInfo.Phrases[currentTutorialStep].PhraseText));
    }

    private IEnumerator SlowlyPrintText(TMP_Text textComponent, string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            textComponent.text += text[i];

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
        }

        phrasePrintCoroutine = null;
    }

    private IEnumerator DelayActivate(System.Func<bool> predicate, UnityAction callback)
    {
        yield return new WaitUntil(predicate);

        callback?.Invoke();
    }
}