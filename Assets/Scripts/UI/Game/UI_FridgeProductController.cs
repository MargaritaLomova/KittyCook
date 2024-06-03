using KittyCook.Data;
using UnityEngine;
using UnityEngine.UI;

public class UI_FridgeProductController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Toggle selectedToggle;

    public ProductInfo CurrentInfo { get; private set; }
    public bool ToggleSelection 
    {
        get
        {
            return selectedToggle.isOn;
        }
        set
        {
            selectedToggle.isOn = value;
        }
    }

    public void Init(ProductInfo info)
    {
        CurrentInfo = info;

        icon.sprite = CurrentInfo.Sprite;
    }
}