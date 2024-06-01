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
    private List<UI_FridgeProductController> products = new List<UI_FridgeProductController>();

    private void Start()
    {
        closeButton.onClick.AddListener(Hide);

        GameController.Get.OnNewOrderCreate += SetProducts;

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void SetProducts()
    {
        if (GameController.Get != null && GameController.Get.CurrentOrder != null)
            SetProducts(GameController.Get.CurrentOrder.ProductsForCook);
    }

    private void SetProducts(List<ProductInfo> productsToCook)
    {
        List<ProductInfo> productsToShow = new List<ProductInfo>();
        productsToShow.AddRange(productsToCook);

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