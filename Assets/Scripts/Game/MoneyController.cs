using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public int Money { get; private set; }
    public static MoneyController Get { get; private set; }

    private void Awake()
    {
        Get = this;

        Money = PlayerPrefs.GetInt("Money");
    }

    public void AddMoney(int count)
    {
        Money += count;
        PlayerPrefs.SetInt("Money", Money);
    }

    public void RemoveMoney(int count)
    {
        Money -= count;
        PlayerPrefs.SetInt("Money", Money);
    }
}