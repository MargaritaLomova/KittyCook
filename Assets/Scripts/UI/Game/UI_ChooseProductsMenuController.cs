using KittyCook.Data;
using KittyCook.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_ChooseProductsMenuController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Button closeButton;
    [SerializeField]
    private Button acceptButton;
    [SerializeField]
    private List<UI_FridgeProductController> products = new List<UI_FridgeProductController>();

    private List<ProductInfo> currentList = new List<ProductInfo>();
    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        closeButton.onClick.AddListener(Hide);
        acceptButton.onClick.AddListener(OnAcceptButtonClicked);

        GameController.Get.OnNewOrderCreate += SetProducts;

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        foreach(var productIcon in products)
        {
            productIcon.ToggleSelection = player.PlayerInventory.Contains(productIcon.CurrentInfo);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        currentList.Clear();
    }

    public void OnAcceptButtonClicked()
    {
        foreach(var product in products)
        {
            if (product.ToggleSelection)
                currentList.Add(product.CurrentInfo);
        }

        player.PlayerInventory = currentList;

        Hide();
    }

    private void SetProducts(RecipeInfo order)
    {
        List<ProductInfo> productsToShow = new List<ProductInfo>();
        productsToShow.AddRange(order.ProductsForCook);

        if (productsToShow.Count < products.Count)
        {
            while (productsToShow.Count != products.Count)
            {
                var randomIndex = UnityEngine.Random.Range(0, DataConfig.Get.Products.Count);
                if (!productsToShow.Contains(DataConfig.Get.Products[randomIndex]))
                {
                    productsToShow.Add(DataConfig.Get.Products[randomIndex]);
                }
            }
        }

        productsToShow = productsToShow.OrderBy(i => Guid.NewGuid()).ToList();
        for (int i = 0; i < products.Count; i++)
        {
            products[i].Init(productsToShow[i]);
        }
    }
}