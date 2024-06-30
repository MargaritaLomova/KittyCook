using DG.Tweening;
using UnityEngine;

public class UI_PanelController : MonoBehaviour
{
    [Header("Animation Variables")]
    [SerializeField]
    private float openCloseAnimationSpeed = 0.2f;

    public virtual void Show()
    {
        transform.localScale = new Vector3(0, 0, 0);
        gameObject.SetActive(true);

        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), openCloseAnimationSpeed).OnComplete(() => transform.DOScale(new Vector3(1, 1, 1), openCloseAnimationSpeed / 3));
    }

    public virtual void Hide()
    {
        transform.localScale = new Vector3(1, 1, 1);

        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), openCloseAnimationSpeed / 3).OnComplete(() =>
        {
            transform.DOScale(new Vector3(0, 0, 0), openCloseAnimationSpeed).OnComplete(() => gameObject.SetActive(false));
        });
    }
}