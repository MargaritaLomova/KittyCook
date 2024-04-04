using KittyCook.Data;
using KittyCook.Helpers;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KittyCook.Tech
{
    public class AddRecipeView : BaseView
    {
        [Space]
        [Header("Components")]
        [SerializeField]
        private ScrollRect existingRecipesScroll;

        [Space]
        [Header("AddEdit Window")]
        [SerializeField]
        private GameObject addWindow;
        [SerializeField]
        private ScrollRect addWindowRecipeScroll;
        [SerializeField]
        private TMP_InputField addWindowENName;
        [SerializeField]
        private TMP_InputField addWindowRUName;
        [SerializeField]
        private TMP_Dropdown addWindowSpriteDropdown;
        [SerializeField]
        private TMP_Dropdown addWindowCookingMethodDropdown;
        [SerializeField]
        private ToggleGroup addWindowToggleGroup;
        [SerializeField]
        private Button addWindowSaveButton;

        [Space]
        [Header("Prefabs")]
        [SerializeField]
        private RecipeElement recipeElementPrefab;
        [SerializeField]
        private ProductForCookElement addWindowProductElementPrefab;

        public override void Show()
        {
            base.Show();

            RedrawExistingRecipes();

            var spritesNames = Resources.LoadAll(@"Images\Recipes").Select(sprite => sprite.name).Where(spriteName => !spriteName.Contains(".meta")).Distinct();
            addWindowSpriteDropdown.options.Clear();
            List<TMP_Dropdown.OptionData> newOptions = new List<TMP_Dropdown.OptionData>
            {
                new TMP_Dropdown.OptionData()
                {
                    text = string.Empty
                }
            };

            foreach (var sName in spritesNames)
            {
                newOptions.Add(new TMP_Dropdown.OptionData()
                {
                    text = $"{sName}.png"
                });
            }

            addWindowSpriteDropdown.AddOptions(newOptions);

            var mainView = FindObjectOfType<MainView>();
            backButton.onClick.AddListener(() =>
            {
                mainView.Show();
                Hide();
            });
        }

        public void AddNewProductClick()
        {
            addWindow.SetActive(true);
            addWindowENName.text = string.Empty;
            addWindowRUName.text = string.Empty;
            addWindowSpriteDropdown.value = 0;
            addWindowCookingMethodDropdown.value = 0;
            foreach (var product in ProductsDataConverter.Instance.GetAllProducts())
            {
                var newProductForCook = Instantiate(addWindowProductElementPrefab, addWindowRecipeScroll.content);
                newProductForCook.Set(product);
                newProductForCook.GetComponent<Toggle>().isOn = false;
            }
            addWindowSaveButton.onClick.RemoveAllListeners();
            addWindowSaveButton.onClick.AddListener(() =>
            {
                RecipesDataConverter.Instance.AddRecipe(new Data.RecipeInfo()
                {
                    RUDishName = addWindowRUName.text,
                    ENDishName = addWindowENName.text,
                    DishSpritePath = addWindowSpriteDropdown.options[addWindowSpriteDropdown.value].text,
                    ProductsForCook = addWindowRecipeScroll.content.GetComponentsInChildren<ProductForCookElement>().Where(element => element.GetComponent<Toggle>().isOn).Select(element => element.Info).ToList(),
                    CookingMethod = (CookingMethod)addWindowCookingMethodDropdown.value
                });

                RedrawExistingRecipes();
                Timer.Instance.WaitForSeconds(0.2f, () => addWindow.SetActive(false));
            });
        }

        private void RedrawExistingRecipes()
        {
            foreach(var oldRecipe in existingRecipesScroll.content.GetComponentsInChildren<RecipeElement>())
            {
                Destroy(oldRecipe.gameObject);
            }

            foreach (var recipe in RecipesDataConverter.Instance.GetAllRecipes())
            {
                var newRecipe = Instantiate(recipeElementPrefab, existingRecipesScroll.content);
                newRecipe.Set(recipe.ENDishName, () =>
                {
                    addWindow.SetActive(true);
                    addWindowENName.text = $"{recipe.ENDishName}";
                    addWindowRUName.text = $"{recipe.RUDishName}";

                    var optionWithCurrentSpriteName = addWindowSpriteDropdown.options.Find(option => option.text == recipe.DishSpritePath);
                    addWindowSpriteDropdown.value = addWindowSpriteDropdown.options.IndexOf(optionWithCurrentSpriteName);

                    addWindowCookingMethodDropdown.value = (int)recipe.CookingMethod;

                    foreach (var oldProduct in addWindowRecipeScroll.content.GetComponentsInChildren<ProductForCookElement>())
                    {
                        Destroy(oldProduct.gameObject);
                    }
                    foreach(var product in ProductsDataConverter.Instance.GetAllProducts())
                    {
                        var newProductForCook = Instantiate(addWindowProductElementPrefab, addWindowRecipeScroll.content);
                        newProductForCook.Set(product);
                        newProductForCook.GetComponent<Toggle>().isOn = recipe.ProductsForCook.Find(p => p.ENName == product.ENName) != null;
                    }

                    foreach(var product in recipe.ProductsForCook)
                    {
                        addWindowRecipeScroll.content
                            .GetComponentsInChildren<ProductForCookElement>()
                            .ToList()
                            .Find(element => element.Info.ENName == product.ENName)
                            .GetComponent<Toggle>().isOn = true;
                    }

                    addWindowSaveButton.onClick.RemoveAllListeners();
                    addWindowSaveButton.onClick.AddListener(() =>
                    {
                        RecipesDataConverter.Instance.RemoveRecipe(recipe);
                        RecipesDataConverter.Instance.AddRecipe(new Data.RecipeInfo()
                        {
                            RUDishName = addWindowRUName.text,
                            ENDishName = addWindowENName.text,
                            DishSpritePath = addWindowSpriteDropdown.options[addWindowSpriteDropdown.value].text,
                            ProductsForCook = addWindowRecipeScroll.content.GetComponentsInChildren<ProductForCookElement>().Where(element => element.GetComponent<Toggle>().isOn).Select(element => element.Info).ToList(),
                            CookingMethod = (CookingMethod)addWindowCookingMethodDropdown.value
                    });
                        RedrawExistingRecipes();
                        Timer.Instance.WaitForSeconds(0.2f, () => addWindow.SetActive(false));
                    });
                }, () =>
                {
                    RecipesDataConverter.Instance.RemoveRecipe(recipe);
                    RedrawExistingRecipes();
                });
            }
        }
    }
}