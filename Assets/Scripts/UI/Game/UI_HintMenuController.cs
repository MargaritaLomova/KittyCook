using KittyCook.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HintMenuController : MonoBehaviour
{
    [Header("Components")]
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
    }

    public void Set(RecipeInfo recipeInfo)
    {
        targetIcon.Set(recipeInfo.Sprite);
        //cookingMethodIcon.Set(recipeInfo.CookingMethod);

        foreach (var icon in productsIcons)
            icon.gameObject.SetActive(false);
        for(int i = 0; i < recipeInfo.ProductsForCook.Count; i++)
        {
            productsIcons[i].Set(recipeInfo.ProductsForCook[i].Sprite);
            productsIcons[i].gameObject.SetActive(true);
        }
    }

    private void OnCloseButtonClicked()
    {
        gameObject.SetActive(false);
    }
}