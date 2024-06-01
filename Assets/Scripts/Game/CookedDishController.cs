using KittyCook.Data;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CookedDishController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Image icon;

    [Space]
    [Header("Variables")]
    [SerializeField]
    private float startXPosition;
    [SerializeField]
    private float clientOffset;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Sprite unknownSprite;

    private RectTransform rectTransform;

    public RecipeInfo CurrentInfo { get; private set; }

    private void Start()
    {
        gameObject.SetActive(false);

        rectTransform = GetComponent<RectTransform>();
    }

    public void SetAndStartGiveDishAnimation(RecipeInfo info, UnityAction callback)
    {
        CurrentInfo = info;

        rectTransform.anchoredPosition = new Vector2(startXPosition, rectTransform.anchoredPosition.y);

        if (CurrentInfo != null)
        {
            icon.sprite = info.Sprite;
        }
        else
        {
            icon.sprite = unknownSprite;
        }

        gameObject.SetActive(true);

        StartCoroutine(GiveAnimation(callback));
    }

    private IEnumerator GiveAnimation(UnityAction callback)
    {
        while(rectTransform.anchoredPosition.x > clientOffset)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x - Time.fixedDeltaTime * moveSpeed, rectTransform.anchoredPosition.y);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        yield return new WaitForSeconds(Time.fixedDeltaTime);

        gameObject.SetActive(false);

        callback?.Invoke();
    }
}