using UnityEngine;
using UnityEngine.UI;

public class UI_StoveController : UI_PanelController
{
    [Header("Components")]
    [SerializeField]
    private Button closeButton;
    [SerializeField]
    private Button fryButton;
    [SerializeField]
    private Button boilButton;
    [SerializeField]
    private Button bakeButton;

    private StoveController stove;

    private void Start()
    {
        stove = FindObjectOfType<StoveController>();

        fryButton.onClick.AddListener(OnFryClicked);
        boilButton.onClick.AddListener(OnBoilClicked);
        bakeButton.onClick.AddListener(OnBakeClicked);

        closeButton.onClick.AddListener(Hide);

        gameObject.SetActive(false);
    }

    public void OnFryClicked()
    {
        stove.StartToFry();

        Hide();
    }

    public void OnBoilClicked()
    {
        stove.StartToBoil();

        Hide();
    }

    public void OnBakeClicked()
    {
        stove.StartToBake();

        Hide();
    }
}