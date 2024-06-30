using KittyCook.Data;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HintMenuController : UI_PanelController
{
    [Header("Components")]
    [SerializeField]
    private TMP_Text title;
    [SerializeField]
    private UI_HintIconController targetIcon;
    [SerializeField]
    private UI_HintIconController cookingMethodIcon;
    [SerializeField]
    private List<UI_HintIconController> productsIcons = new List<UI_HintIconController>();

    [SerializeField]
    private Button closeButton;

    private void Start()
    {
        gameObject.SetActive(false);

        closeButton.onClick.AddListener(OnCloseButtonClicked);

        GameController.Get.OnNewOrderCreate += Set;
    }

    private void Set(RecipeInfo order)
    {
        title.text = order.ENName;

        targetIcon.Set(order.Sprite);
        //cookingMethodIcon.Set(recipeInfo.CookingMethod);

        foreach (var icon in productsIcons)
            icon.gameObject.SetActive(false);
        for(int i = 0; i < order.ProductsForCook.Count; i++)
        {
            productsIcons[i].Set(order.ProductsForCook[i].Sprite);
            productsIcons[i].gameObject.SetActive(true);
        }
    }

    private void OnCloseButtonClicked()
    {
        Hide();
    }
}