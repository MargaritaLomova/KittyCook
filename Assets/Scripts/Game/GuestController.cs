using KittyCook.Data;
using KittyCook.Tech;
using System.Collections;
using TMPro;
using UnityEngine;

public class GuestController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private GameObject bubble;
    [SerializeField]
    private TMP_Text orderText;

    public RecipeInfo Order { get; private set; }


    private void Start()
    {
        bubble.SetActive(false);

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

        orderText.text = $"I want to order \n {Order.ENName}!";
        bubble.SetActive(true);

        yield return new WaitForSecondsRealtime(2f);

        bubble.SetActive(false);
    }

    private IEnumerator ShowReactionAndGoAway(string reactionText)
    {
        yield return new WaitForSecondsRealtime(0.5f);

        orderText.text = reactionText;
        bubble.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);

        bubble.SetActive(false);

        if (gameObject)
            Destroy(gameObject);
    }
}