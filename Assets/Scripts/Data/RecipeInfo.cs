using System.Collections.Generic;
using UnityEngine;

namespace KittyCook.Data
{
    [CreateAssetMenu(fileName = "NewRecipe", menuName = "Kitty Cook/Create new recipe")]
    public class RecipeInfo : ScriptableObject
    {
        public string RUName;
        public string ENName;
        public Sprite Sprite;
        public List<ProductInfo> ProductsForCook = new List<ProductInfo>();
        public CookingMethod CookingMethod;
        public int Cost;
    }

    public enum CookingMethod
    {
        Nothing,
        Frying,
        Boiling,
        Baking
    }

    public class RecipeProductInfo
    {
        public ProductInfo Product { get; set; }
        public int Count { get; set; }
    }
}