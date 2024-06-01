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

    private PlayerController player;
    private ProductInfo currentInfo;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        selectedToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    public void Init(ProductInfo info)
    {
        currentInfo = info;

        icon.sprite = currentInfo.Sprite;

        selectedToggle.isOn = false;
    }

    public void OnToggleChanged(bool value)
    {
        if (value) player.AddProductToInventory(currentInfo);
        else player.RemoveProductToInventory(currentInfo);
    }
}