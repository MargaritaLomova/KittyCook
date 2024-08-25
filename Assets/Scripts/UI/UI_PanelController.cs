using DG.Tweening;
using UnityEngine;

public class UI_PanelController : MonoBehaviour
{
    [Header("Animation Variables")]
    [SerializeField]
    protected float openCloseAnimationSpeed = 0.2f;

    public bool IsShowen => gameObject.activeInHierarchy;
    public bool ShowAnimationEnded { get; private set; }

    public virtual void Show()
    {
        ShowAnimationEnded = false;

        transform.localScale = new Vector3(0, 0, 0);
        gameObject.SetActive(true);

        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), openCloseAnimationSpeed).OnComplete(() =>
        {
            transform.DOScale(new Vector3(1, 1, 1), openCloseAnimationSpeed / 3).SetUpdate(true);
            ShowAnimationEnded = true;
        }).SetUpdate(true);
    }

    public virtual void Hide()
    {
        transform.localScale = new Vector3(1, 1, 1);

        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), openCloseAnimationSpeed / 3).OnComplete(() =>
        {
            transform.DOScale(new Vector3(0, 0, 0), openCloseAnimationSpeed).OnComplete(() => gameObject.SetActive(false)).SetUpdate(true);
        }).SetUpdate(true);
    }
}