using UnityEngine;
using UnityEngine.UI;

public class UI_HintIconController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Image icon;

    public void Set(Sprite iconSprite)
    {
        icon.sprite = iconSprite;
    }
}