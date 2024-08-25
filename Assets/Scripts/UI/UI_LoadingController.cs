using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UI_LoadingController : UI_PanelController
{
    [Header("Components")]
    [SerializeField]
    private RectTransform loadingIcon;
    [SerializeField]
    private TMP_Text loadingText;

    public static UI_LoadingController Get { get; private set; }

    private Coroutine rotateCoroutine;
    private Coroutine changeTextCoroutine;

    private void Awake()
    {
        Get = this;

        DontDestroyOnLoad(transform.parent.gameObject);

        gameObject.SetActive(false);
    }

    public override void Show()
    {
        base.Show();

        rotateCoroutine = StartCoroutine(RotateIcon());
        changeTextCoroutine = StartCoroutine(ChangeText());
    }

    public override void Hide()
    {
        StartCoroutine(DelayActivate(() => ShowAnimationEnded, () =>
        {
            base.Hide();

            StopCoroutine(rotateCoroutine);
            StopCoroutine(changeTextCoroutine);
        }));
    }

    private IEnumerator RotateIcon()
    {
        var duration = 0.7f;
        while (IsShowen)
        {
            var angles = loadingIcon.transform.eulerAngles;
            loadingIcon.transform.DORotate(new Vector3(angles.x, angles.y, angles.z - 180f), duration, RotateMode.Fast).SetLoops(-1, LoopType.Incremental).SetUpdate(true);

            yield return new WaitForSecondsRealtime(duration + 0.2f);
        }
    }

    private IEnumerator ChangeText()
    {
        var duration = 0.3f;
        while (IsShowen)
        {
            for(int i = 0; i < 3; i++)
            {
                var text = $"Loading";
                for(int j = 0; j < i + 1; j++)
                {
                    text += ".";
                }
                loadingText.text = text;

                yield return new WaitForSecondsRealtime(duration);
            }

            yield return new WaitForSecondsRealtime(duration);
        }
    }

    private IEnumerator DelayActivate(System.Func<bool> predicate, UnityAction callback)
    {
        yield return new WaitUntil(predicate);
        yield return new WaitForSecondsRealtime(0.3f);

        callback?.Invoke();
    }
}