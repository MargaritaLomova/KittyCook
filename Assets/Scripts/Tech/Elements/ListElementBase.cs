using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KittyCook.Tech
{
    public class ListElementBase : MonoBehaviour
    {
        [Header("Base Components")]
        [SerializeField]
        protected TMP_Text nameText;
        [SerializeField]
        protected Button editButton;
        [SerializeField]
        protected Button deleteButton;

        public virtual void Set(string name, UnityAction onEditButtonAction, UnityAction onDeleteButtonAction)
        {
            nameText.text = name;
            editButton.onClick.AddListener(onEditButtonAction);
            deleteButton.onClick.AddListener(() =>
            {
                onDeleteButtonAction?.Invoke();
                Destroy(gameObject);
            });
        }
    }
}