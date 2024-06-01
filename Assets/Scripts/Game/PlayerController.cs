using KittyCook.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Animator playerAnimator;

    [Space]
    [Header("World Objects")]
    [SerializeField]
    private CookedDishController cookedDishController;

    private StoveController stove;
    private List<ProductInfo> playerInventory = new List<ProductInfo>();
    private RecipeInfo cookedDish;
    private bool isDishCooked = false;

    private void Start()
    {
        stove = FindObjectOfType<StoveController>();
    }

    public void AddProductToInventory(ProductInfo product)
    {
        playerInventory.Add(product);
    }

    public void RemoveProductToInventory(ProductInfo product)
    {
        playerInventory.Remove(product);
    }

    public void GetCookedDish(CookingMethod cookingMethod)
    {
        isDishCooked = true;

        var order = GameController.Get.CurrentOrder;
        bool isListsEqual = false;
        if(order.ProductsForCook.Count == playerInventory.Count)
        {
            for(int i = 0; i < order.ProductsForCook.Count; i++)
            {
                if (!order.ProductsForCook.Contains(playerInventory[i]))
                    break;
                if (i == order.ProductsForCook.Count - 1)
                    isListsEqual = true;
            }
        }

        if (isListsEqual && cookingMethod == order.CookingMethod)
            cookedDish = order;
    }

    public void Flip(bool isLookRight)
    {
        if (isLookRight)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else
            transform.eulerAngles = new Vector3(0, 180, 0);
    }

    public void CutProducts()
    {
        
    }

    public void CookDish()
    {
        if (playerInventory.Count > 0)
            stove.ShowStoveMenu();
    }

    public void GiveDishToClient()
    {
        if(isDishCooked)
        {
            cookedDishController.SetAndStartGiveDishAnimation(cookedDish, () =>
            {
                GameController.Get.ClientGetCookedDish(cookedDish);
                cookedDish = null;
            });
        }
    }
}