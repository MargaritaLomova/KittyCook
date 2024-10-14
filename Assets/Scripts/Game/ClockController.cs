using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ClockController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private RectTransform clockHand;
    [SerializeField]
    private Image filling;
    public static ClockController Get { get; private set; }

    private void Awake()
    {
        Get = this;
    }

    public void Reset()
    {
        filling.fillAmount = 0;
        clockHand.transform.eulerAngles = Vector3.zero;
    }

    public void UpdateClocks(float maxTime, float currentTime)
    {
        var startFillAmount = filling.fillAmount;
        var startClockHandRotation = clockHand.transform.eulerAngles;

        var percentsOfMaxTime = (currentTime * 1.02f) / maxTime;
        var targetZRotation = -(360 * percentsOfMaxTime) + 12 > 0 ? 0 : -(360 * percentsOfMaxTime) + 12;
        var clockHandTargetRotation = new Vector3(startClockHandRotation.x, startClockHandRotation.y, targetZRotation);

        filling.fillAmount = Mathf.Lerp(startFillAmount, percentsOfMaxTime, Time.fixedDeltaTime);
        clockHand.transform.DORotate(clockHandTargetRotation, Time.fixedDeltaTime);
    }
}