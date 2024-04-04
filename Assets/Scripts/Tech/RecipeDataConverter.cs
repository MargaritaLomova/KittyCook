using KittyCook.Data;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace KittyCook.Helpers
{
    public class RecipesDataConverter : DataConverter
    {
        public static RecipesDataConverter Instance;

        protected override void Awake()
        {
            base.Awake();

            if (Instance == null)
            {
                Instance = this;
                return;
            }

            Destroy(gameObject);
        }

        public void AddRecipe(RecipeInfo info)
        {
            var productForCook = new JArray();

            foreach (var product in info.ProductsForCook)
            {
                productForCook.Add(new JObject()
                {
                    { "Product_RUName", product.RUName },
                    { "Product_ENName", product.ENName },
                    { "Product_SpritePath", product.SpritePath }
                });
            }

            AddDataLine(new JObject()
            {
                { "RUName", info.RUDishName },
                { "ENName", info.ENDishName },
                { "SpritePath", info.DishSpritePath },
                { "ProductsForCook", productForCook },
                { "CookingMethod", (int)info.CookingMethod },
            });
        }

        public void RemoveRecipe(RecipeInfo info)
        {
            RemoveDataLine(info.ENDishName);
        }

        public List<RecipeInfo> GetAllRecipes()
        {
            var list = new List<RecipeInfo>();

            var data = GetAllData();
            foreach (var item in data)
            {
                var recipe = new RecipeInfo()
                {
                    RUDishName = item.Value<string>("RUName"),
                    ENDishName = item.Value<string>("ENName"),
                    DishSpritePath = item.Value<string>("SpritePath"),
                    CookingMethod = (CookingMethod)item.Value<int>("CookingMethod")
                };

                var productForCookJArray = item.Value<JArray>("ProductsForCook");

                recipe.ProductsForCook = new List<ProductInfo>();
                foreach (var productForCook in productForCookJArray)
                {
                    recipe.ProductsForCook.Add(new ProductInfo()
                    {
                        RUName = productForCook.Value<string>("Product_RUName"),
                        ENName = productForCook.Value<string>("Product_ENName"),
                        SpritePath = productForCook.Value<string>("Product_SpritePath")
                    });
                }

                list.Add(recipe);
            }

            return list;
        }
    }
}