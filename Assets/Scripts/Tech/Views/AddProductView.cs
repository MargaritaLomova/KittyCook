using KittyCook.Helpers;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KittyCook.Tech
{
    public class AddProductView : BaseView
    {
        [Header("Components")]
        [SerializeField]
        private ScrollRect existingProductsScroll;

        [Space]
        [Header("AddEdit Window")]
        [SerializeField]
        private GameObject addWindow;
        [SerializeField]
        private TMP_InputField addWindowENName;
        [SerializeField]
        private TMP_InputField addWindowRUName;
        [SerializeField]
        private TMP_Dropdown addWindowSpriteDropdown;
        [SerializeField]
        private Button addWindowSaveButton;

        [Space]
        [Header("Prefabs")]
        [SerializeField]
        private ProductElement productElementPrefab;

        public override void Show()
        {
            base.Show();

            RedrawExistingRecipes();

            var spritesNames = Resources.LoadAll(@"Images\Products").Select(sprite => sprite.name).Where(spriteName=> !spriteName.Contains(".meta")).Distinct();
            addWindowSpriteDropdown.options.Clear();
            List<TMP_Dropdown.OptionData> newOptions = new List<TMP_Dropdown.OptionData>
            {
                new TMP_Dropdown.OptionData()
                {
                    text = string.Empty
                }
            };

            foreach (var sName in  spritesNames)
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

        private void RedrawExistingRecipes()
        {
            foreach (var oldProduct in existingProductsScroll.content.GetComponentsInChildren<ProductElement>())
            {
                Destroy(oldProduct.gameObject);
            }

            foreach(var product in ProductsDataConverter.Instance.GetAllProducts())
            {
                var newProduct = Instantiate(productElementPrefab, existingProductsScroll.content);
                newProduct.Set(product.ENName, () =>
                {
                    addWindow.SetActive(true);
                    addWindowENName.text = $"{product.ENName}";
                    addWindowRUName.text = $"{product.RUName}";

                    var optionWithCurrentSpriteName = addWindowSpriteDropdown.options.Find(option => option.text == product.SpritePath);
                    addWindowSpriteDropdown.value = addWindowSpriteDropdown.options.IndexOf(optionWithCurrentSpriteName);

                    addWindowSaveButton.onClick.RemoveAllListeners();
                    addWindowSaveButton.onClick.AddListener(() =>
                    {
                        ProductsDataConverter.Instance.RemoveProduct(product);
                        ProductsDataConverter.Instance.AddProduct(new Data.ProductInfo()
                        {
                            RUName = addWindowRUName.text,
                            ENName = addWindowENName.text,
                            SpritePath = addWindowSpriteDropdown.options[addWindowSpriteDropdown.value].text
                        });
                        addWindow.SetActive(false);
                        RedrawExistingRecipes();
                    });
                }, () =>
                {
                    ProductsDataConverter.Instance.RemoveProduct(product);
                    RedrawExistingRecipes();
                });
            }
        }

        public void AddNewProductClick()
        {
            addWindow.SetActive(true);
            addWindowENName.text = string.Empty;
            addWindowRUName.text = string.Empty;
            addWindowSpriteDropdown.value = 0;
            addWindowSaveButton.onClick.RemoveAllListeners();
            addWindowSaveButton.onClick.AddListener(() =>
            {
                ProductsDataConverter.Instance.AddProduct(new Data.ProductInfo()
                {
                    RUName = addWindowRUName.text,
                    ENName = addWindowENName.text,
                    SpritePath = addWindowSpriteDropdown.options[addWindowSpriteDropdown.value].text
                });
                addWindow.SetActive(false);
                RedrawExistingRecipes();
            });
        }
    }
}