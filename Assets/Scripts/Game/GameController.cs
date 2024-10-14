using KittyCook.Data;
using KittyCook.Tech;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Transform guestsHolder;
    [SerializeField]
    private ClockController clocks;

    [Space]
    [Header("Variables")]
    [SerializeField]
    private float guestLifetime = 15f;

    [Space]
    [Header("Menus")]
    [SerializeField]
    private UI_ChooseProductsMenuController chooseProductsMenuController;
    [SerializeField]
    private UI_HintMenuController hintMenuController;
    [SerializeField]
    private UI_StoveController stoveController;
    [SerializeField]
    private UI_WinMenuController winMenuController;
    [SerializeField]
    private UI_LostMenuController lostMenuController;


    public static GameController Get { get; private set; }
    public event UnityAction<RecipeInfo> OnNewOrderCreate;
    public GuestController CurrentGuest { get; private set; }
    public RecipeInfo CurrentOrder => CurrentGuest.Order;
    public LevelInfo CurrentInfo => currentInfo;
    public int CountOfSuccessOrders { get; private set; }
    public int CountOfMistakes { get; private set; }

    private bool isGame = true;
    private LevelInfo currentInfo;
    private TutorialInfo currentTutorialInfo;
    private Coroutine guestsCoroutine;
    private Coroutine timerCoroutine;

    private float currentTimer;

    private void Awake()
    {
        Get = this;
    }

    private async void Start()
    {
        await Task.Delay(10);

        if (currentTutorialInfo != null)
        {
            UI_TutorialMenuController.Get.SetTutorialPreset(currentTutorialInfo);
            UI_TutorialMenuController.Get.Show();
        }
    }

    public void StartNewLevel(LevelInfo info)
    {
        isGame = true;

        if (guestsCoroutine != null)
            StopCoroutine(guestsCoroutine);
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);

        var previousGuests = guestsHolder.GetComponentsInChildren<GuestController>();
        if (previousGuests.Count() > 0)
        {
            foreach (var guest in previousGuests)
            {
                Destroy(guest.gameObject);
            }
        }

        currentInfo = info;

        currentTimer = 0;
        CountOfSuccessOrders = 0;
        CountOfMistakes = 0;

        clocks.Reset();

        CloseAllWindows();

        guestsCoroutine = StartCoroutine(GuestsCoroutine());
        timerCoroutine = StartCoroutine(TimerCoroutine());

        var tutorialsInfo = TutorialsInfo.Get;
        var tutorials = tutorialsInfo.Tutorials;
        currentTutorialInfo = tutorials.Find(x => x.LevelToShowIndex == currentInfo.Index);
    }

    public void ClientGetCookedDish(RecipeInfo info)
    {
        if (CurrentOrder == info)
        {
            CurrentGuest.ShowPositiveReaction();
            CountOfSuccessOrders++;
        }
        else
        {
            CurrentGuest.ShowNegativeReaction();
            CountOfMistakes++;
            if (CountOfMistakes >= 3)
            {
                isGame = false;
            }
        }
    }

    public void CloseAllWindows()
    {
        chooseProductsMenuController.Hide();
        hintMenuController.Hide();
        stoveController.Hide();
        winMenuController.Hide();
        lostMenuController.Hide();
    }

    private IEnumerator GuestsCoroutine()
    {
        while (isGame && currentTimer < currentInfo.TimeOfDayInSeconds)
        {
            yield return new WaitForSeconds(0.5f);

            var randomIndex = Random.Range(0, currentInfo.AvailableGuests.Length);
            var randomGuestPrefab = currentInfo.AvailableGuests[randomIndex];

            CurrentGuest = Instantiate(randomGuestPrefab, guestsHolder);
            CurrentGuest.transform.SetAsFirstSibling();

            yield return new WaitForSeconds(Time.fixedDeltaTime);

            OnNewOrderCreate?.Invoke(CurrentOrder);

            float timer = 0f;
            while (timer < guestLifetime && CurrentGuest && CurrentGuest.gameObject)
            {
                timer += Time.fixedDeltaTime;
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
        }
    }

    private IEnumerator TimerCoroutine()
    {
        currentTimer = 0;

        while (isGame && currentTimer < currentInfo.TimeOfDayInSeconds)
        {
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            currentTimer += Time.fixedDeltaTime;

            clocks.UpdateClocks(currentInfo.TimeOfDayInSeconds, currentTimer);
        }

        clocks.UpdateClocks(currentInfo.TimeOfDayInSeconds, currentInfo.TimeOfDayInSeconds);
        isGame = false;

        if (CountOfSuccessOrders >= currentInfo.MaxGuestsCount && CountOfMistakes < 3)
        {
            if (CountOfSuccessOrders > currentInfo.MaxGuestsCount && CountOfMistakes == 0)
            {
                winMenuController.Show(3);
            }
            else if (CountOfSuccessOrders > currentInfo.MaxGuestsCount && CountOfMistakes != 0)
            {
                winMenuController.Show(2);
            }
            else
            {
                winMenuController.Show(1);
            }
        }
        else
        {
            lostMenuController.Show();
        }
    }
}