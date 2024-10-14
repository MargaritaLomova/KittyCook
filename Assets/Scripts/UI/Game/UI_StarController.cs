using DG.Tweening;
using UnityEngine;

public class UI_StarController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private RectTransform mainStar;

    [Space]
    [Header("Variables")]
    [SerializeField]
    private float animationDuration = 1.7f;

    private void OnEnable()
    {
        mainStar.anchoredPosition = new Vector2(mainStar.anchoredPosition.x, 50);
        mainStar.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void ShowStartAnimation()
    {
        mainStar.anchoredPosition = new Vector2(mainStar.anchoredPosition.x, 50);
        mainStar.transform.localScale = new Vector3(0f, 0f, 0f);

        mainStar.DOAnchorPosY(0, animationDuration).SetUpdate(true);
        mainStar.transform.DOScale(new Vector3(1, 1, 1), animationDuration).SetUpdate(true);
    }
}