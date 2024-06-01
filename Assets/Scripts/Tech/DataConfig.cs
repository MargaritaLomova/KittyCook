using KittyCook.Data;
using System.Collections.Generic;
using UnityEngine;

namespace KittyCook.Tech
{
    [CreateAssetMenu(fileName = "DataConfig", menuName = "Kitty Cook/Create data config")]
    public class DataConfig : ScriptableObject
    {
        [SerializeField]
        private List<ProductInfo> products = new List<ProductInfo>();
        [SerializeField]
        private List<RecipeInfo> recipes = new List<RecipeInfo>();

        public List<ProductInfo> Products => products;
        public List<RecipeInfo> Recipes => recipes;

        private static DataConfig mInstance;
        public static DataConfig Get
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = Resources.Load("Configs/DataConfig") as DataConfig;
                }

                return mInstance;
            }
            set
            {
                mInstance = value;
            }
        }
    }
}