using UnityEngine;
using UnityEngine.UI;

namespace KittyCook.Tech
{
    [RequireComponent(typeof(CanvasGroup))]

    public class BaseView : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField]
        protected Button backButton;

        protected CanvasGroup canvasGroup;

        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        public virtual void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}