using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LevelsMenuController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [Header("Components")]
    [SerializeField]
    private ScrollRect levelsView;
    [SerializeField]
    private RectTransform dotsHolder;
    [SerializeField]
    private Button returnButton;

    [Space]
    [Header("Variables")]
    [SerializeField]
    private float snapForce = 100;
    [SerializeField]
    private int countOfLevelsInOneHolder = 15;

    [Space]
    [Header("Prefabs")]
    [SerializeField]
    private Image dotPrefab;
    [SerializeField]
    private RectTransform levelHolderPrefab;
    [SerializeField]
    private UI_LevelButtonController levelButtonPrefab;

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

    private List<RectTransform> levelsHolders = new List<RectTransform>();
    private List<Image> dots = new List<Image>();

    private void Start()
    {
        content = levelsView.content;
        contentLayoutGroup = content.GetComponent<HorizontalLayoutGroup>();

        RedrawLevels();

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

    private void RedrawLevels()
    {
        foreach (var dot in dots)
            Destroy(dot.gameObject);
        foreach (var dot in dotsHolder.GetComponentsInChildren<Image>())
            Destroy(dot.gameObject);
        dots.Clear();

        foreach (var holder in levelsHolders)
            Destroy(holder.gameObject);
        foreach (var holder in content.GetComponentsInChildren<RectTransform>())
        {
            if (holder != content.GetComponent<RectTransform>())
                Destroy(holder.gameObject);
        }
        levelsHolders.Clear();

        var levelsInfo = LevelsInfo.Get.Levels;

        for (int i = 0; i < levelsInfo.Count; i++)
        {
            if (i == 0 || i + 1 % countOfLevelsInOneHolder == 0)
            {
                var newHolder = Instantiate(levelHolderPrefab, content);
                levelsHolders.Add(newHolder);

                var newDot = Instantiate(dotPrefab, dotsHolder);
                dots.Add(newDot);
            }

            var currentHolder = levelsHolders.LastOrDefault();
            var newLevelButton = Instantiate(levelButtonPrefab, currentHolder);
            newLevelButton.Set(levelsInfo[i]);
        }
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