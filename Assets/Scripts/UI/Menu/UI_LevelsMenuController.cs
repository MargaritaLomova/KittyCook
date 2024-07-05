using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LevelsMenuController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [Header("Components")]
    [SerializeField]
    private ScrollRect levelsView;
    [SerializeField]
    private RectTransform[] levelsHolders;
    [SerializeField]
    private RectTransform dotsHolder;
    [SerializeField]
    private Image[] dots;
    [SerializeField]
    private Button returnButton;

    [Space]
    [Header("Variables")]
    [SerializeField]
    private float snapForce = 100;

    [Space]
    [Header("Other")]
    [SerializeField]
    private UI_MenuController menuController;

    private RectTransform content;
    private RectTransform elementSample;
    private HorizontalLayoutGroup contentLayoutGroup;
    private bool isSnapped = false;
    private bool isDragNow = false;
    private float snapSpeed;

    private void Start()
    {
        content = levelsView.content;
        contentLayoutGroup = content.GetComponent<HorizontalLayoutGroup>();

        var firstElement = levelsHolders.Where(x => x.gameObject.activeInHierarchy).FirstOrDefault();

        elementSample = firstElement;

        returnButton.onClick.AddListener(OnReturnButtonClicked);

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (elementSample)
        {
            levelsView.content.localPosition = new Vector2(
                                               Mathf.MoveTowards(levelsView.content.localPosition.x, 0 - (levelsView.viewport.localPosition.x + elementSample.localPosition.x + (elementSample.rect.width / 19f)), snapSpeed),
                                               levelsView.content.localPosition.y);

            UpdateDots();
        }
    }

    private void Update()
    {
        if (levelsView.velocity.magnitude < 700 && !isSnapped && !isDragNow)
        {
            levelsView.velocity = Vector2.zero;

            snapSpeed += snapForce * Time.fixedDeltaTime;

            var currentBanner = GetClosestHolder();

            levelsView.content.localPosition = new Vector2(
                Mathf.MoveTowards(levelsView.content.localPosition.x, 0 - (levelsView.viewport.localPosition.x + currentBanner.localPosition.x), snapSpeed),
                levelsView.content.localPosition.y);

            int currentItem = levelsHolders.Where(x => x.gameObject.activeInHierarchy).ToList().IndexOf(currentBanner);

            for (int i = 0; i < dots.Where(dot => dot.gameObject.activeInHierarchy).Count(); i++)
            {
                dots[i].GetComponentsInChildren<Image>().LastOrDefault().CrossFadeAlpha(i == currentItem ? 1 : 0, Time.fixedDeltaTime, true);
            }

            if (content.localPosition.x == 0 - (currentItem * (elementSample.rect.width / 2.2f + contentLayoutGroup.spacing)))
            {
                isSnapped = true;
            }
        }
        else
        {
            isSnapped = false;
            snapSpeed = 0;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragNow = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragNow = false;
    }

    private void OnReturnButtonClicked()
    {
        gameObject.SetActive(false);
        menuController.gameObject.SetActive(true);
    }

    private void UpdateDots()
    {
        foreach (var dot in dots)
        {
            dot.gameObject.SetActive(false);
        }

        for (int i = 0; i < levelsHolders.Where(element => element.gameObject.activeInHierarchy).Count(); i++)
        {
            dots[i].gameObject.SetActive(true);
        }

        gameObject.SetActive(levelsHolders.Where(element => element.gameObject.activeInHierarchy).Count() > 0);

        LayoutRebuilder.ForceRebuildLayoutImmediate(dotsHolder);
        Canvas.ForceUpdateCanvases();
    }

    private RectTransform GetClosestHolder()
    {
        RectTransform currentHolder = null;
        float minDistance = Mathf.Infinity;
        foreach (RectTransform holder in levelsHolders.Where(x => x.gameObject.activeInHierarchy))
        {
            float dist = Vector3.Distance(holder.transform.position, levelsView.transform.position);
            if (dist < minDistance)
            {
                currentHolder = holder;
                minDistance = dist;
            }
        }
        return currentHolder;
    }
}