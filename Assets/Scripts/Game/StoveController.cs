using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StoveController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject pot;
    [SerializeField]
    private GameObject fryingPan;

    [Space]
    [Header("World Objects")]
    [SerializeField]
    private UI_StoveController stoveMenu;

    [Space]
    [Header("Variables")]
    [SerializeField]
    private float fryTime = 1f;
    [SerializeField]
    private float boilTime = 1f;
    [SerializeField]
    private float bakeTime = 1f;

    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        pot.SetActive(false);
        fryingPan.SetActive(false);
    }

    public void ShowStoveMenu()
    {
        stoveMenu.Show();
    }

    public void StartToFry()
    {
        fryingPan.SetActive(true);
        StartCoroutine(Cooking("Up_on", fryTime, () =>
        {
            fryingPan.SetActive(false);
            player.GetCookedDish(KittyCook.Data.CookingMethod.Frying);
        }));
    }

    public void StartToBoil()
    {
        pot.SetActive(true);
        StartCoroutine(Cooking("Up_and_down_on", fryTime, () =>
        {
            pot.SetActive(false);
            player.GetCookedDish(KittyCook.Data.CookingMethod.Boiling);
        }));
    }

    public void StartToBake()
    {
        StartCoroutine(Cooking("Down_on", fryTime, () =>
        {
            player.GetCookedDish(KittyCook.Data.CookingMethod.Baking);
        }));
    }

    private IEnumerator Cooking(string animationName, float cookingTime, UnityAction finishCallback = null)
    {
        animator.SetBool(animationName, true);

        yield return new WaitForSeconds(cookingTime);

        animator.SetBool(animationName, false);
        finishCallback?.Invoke();
    }
}