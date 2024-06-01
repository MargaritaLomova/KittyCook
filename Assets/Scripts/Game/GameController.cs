using KittyCook.Data;
using KittyCook.Tech;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Transform guestsHolder;

    [Space]
    [Header("Variables")]
    [SerializeField]
    private float guestLifetime = 15f;

    [Space]
    [Header("Prefabs")]
    [SerializeField]
    private List<GuestController> guestsPrefabs = new List<GuestController>();

    public static GameController Get { get; private set; }
    public event UnityAction OnNewOrderCreate;
    public GuestController CurrentGuest { get; private set; }
    public RecipeInfo CurrentOrder => CurrentGuest.Order;

    private bool isGame = true;
    private Coroutine guestsCoroutine;

    private void Awake()
    {
        Get = this;
    }

    private void Start()
    {
        guestsCoroutine = StartCoroutine(GuestsCoroutine());
    }

    public void ClientGetCookedDish(RecipeInfo info)
    {
        if (CurrentOrder == info)
        {
            CurrentGuest.ShowPositiveReaction();
        }
        else
        {
            CurrentGuest.ShowNegativeReaction();
        }
    }

    private IEnumerator GuestsCoroutine()
    {
        while (isGame)
        {
            yield return new WaitForSeconds(0.5f);

            var randomIndex = Random.Range(0, guestsPrefabs.Count);
            var randomGuestPrefab = guestsPrefabs[randomIndex];

            CurrentGuest = Instantiate(randomGuestPrefab, guestsHolder);
            CurrentGuest.transform.SetAsFirstSibling();

            yield return new WaitForSeconds(Time.fixedDeltaTime);

            OnNewOrderCreate?.Invoke();

            float timer = 0f;
            while (timer < guestLifetime && CurrentGuest && CurrentGuest.gameObject)
            {
                timer += Time.fixedDeltaTime;
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }

            //yield return new WaitForSeconds(guestLifetime);

            if (CurrentGuest && CurrentGuest.gameObject)
            {
                Destroy(CurrentGuest.gameObject);
                CurrentGuest = null;
            }
        }
    }
}