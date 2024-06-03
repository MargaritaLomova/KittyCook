using KittyCook.Data;
using KittyCook.Tech;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuestController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private CanvasGroup bubble;
    [SerializeField]
    private TMP_Text orderText;

    public RecipeInfo Order { get; private set; }

    private void Start()
    {
        bubble.alpha = 0;

        GenerateOrder();

        StartCoroutine(ShowAndHideOrder());
    }

    public void ShowPositiveReaction()
    {
        StartCoroutine(ShowReactionAndGoAway($"Oh! Thats great, thank you! Bye!"));
    }

    public void ShowNegativeReaction()
    {
        StartCoroutine(ShowReactionAndGoAway($"Oh... Thats not that i wanted..."));
    }

    private void GenerateOrder()
    {
        var randomIndex = Random.Range(0, DataConfig.Get.Recipes.Count);
        Order = DataConfig.Get.Recipes[randomIndex];
    }

    private IEnumerator ShowAndHideOrder()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        yield return StartCoroutine(ShowAndHideBubbleWithText($"I want to order \n {Order.ENName}!", 2f));
    }

    private IEnumerator ShowReactionAndGoAway(string reactionText)
    {
        yield return new WaitForSecondsRealtime(0.5f);

        yield return StartCoroutine(ShowAndHideBubbleWithText(reactionText, 2f));

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