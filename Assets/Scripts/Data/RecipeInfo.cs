using System.Collections.Generic;

namespace KittyCook.Data
{
    public enum CookingMethod
    {
        Nothing,
        Frying,
        Boiling,
        Baking
    }

    public class RecipeProductInfo
    {
        public ProductInfo Product { get; set;}
        public int Count { get; set;}
    }

    public class RecipeInfo
    {
        public string RUDishName { get; set; }
        public string ENDishName { get; set; }
        public string DishSpritePath { get; set; }
        public List<ProductInfo> ProductsForCook { get; set; }
        public CookingMethod CookingMethod { get; set; }
    }
}