using KittyCook.Data;
using KittyCook.Tech;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GuestController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private CanvasGroup bubble;
    [SerializeField]
    private TMP_Text orderText;
    [SerializeField]
    private Animator animator;

    public RecipeInfo Order { get; private set; }

    private void Start()
    {
        bubble.alpha = 0;

        GenerateOrder();

        StartCoroutine(ShowAndHideOrder());
    }

    public void ShowPositiveReaction()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Happy", true);

        StartCoroutine(ShowReactionAndGoAway($"Oh! Thats great, thank you! Bye!", () =>
        {
            animator.SetBool("Happy", false);
            animator.SetBool("Idle", true);
        }));
    }

    public void ShowNegativeReaction()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Sad", true);

        StartCoroutine(ShowReactionAndGoAway($"Oh... Thats not that i wanted...", () =>
        {
            animator.SetBool("Sad", false);
            animator.SetBool("Idle", true);
        }));
    }

    private void GenerateOrder()
    {
        var randomIndex = Random.Range(0, DataConfig.Get.Recipes.Count);
        Order = DataConfig.Get.Recipes[randomIndex];
    }

    private IEnumerator ShowAndHideOrder()
    {
        animator.SetBool("Happy", true);

        yield return new WaitForSecondsRealtime(0.5f);

        animator.SetBool("Happy", false);
        animator.SetBool("Idle", true);

        yield return StartCoroutine(ShowAndHideBubbleWithText($"I want to order \n {Order.ENName}!", 2f));
    }

    private IEnumerator ShowReactionAndGoAway(string reactionText, UnityAction callback = null)
    {
        yield return new WaitForSecondsRealtime(0.5f);

        yield return StartCoroutine(ShowAndHideBubbleWithText(reactionText, 2f));

        callback?.Invoke();

        if (gameObject)
            Destroy(gameObject);
    }

    private IEnumerator ShowAndHideBubbleWithText(string text, float bubbleLifetime)
    {
        orderText.text = text;

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(bubble.GetComponent<RectTransform>());

        yield return new WaitForSeconds(Time.fixedDeltaTime);

        bubble.alpha = 1;

        yield return new WaitForSeconds(bubbleLifetime);

        bubble.alpha = 0;
    }
}