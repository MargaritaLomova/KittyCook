using KittyCook.Data;
using System.Collections.Generic;

namespace KittyCook.Helpers
{
    public class ProductsDataConverter : DataConverter
    {
        public static ProductsDataConverter Instance;

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

        public void AddProduct(ProductInfo info)
        {
            AddDataLine(new Newtonsoft.Json.Linq.JObject()
            {
                { "RUName", info.RUName },
                { "ENName", info.ENName },
                { "SpritePath", info.SpritePath }
            });
        }

        public void RemoveProduct(ProductInfo info)
        {
            RemoveDataLine(info.ENName);
        }

        public List<ProductInfo> GetAllProducts()
        {
            var list = new List<ProductInfo>();

            var data = GetAllData();
            foreach(var  item in data)
            {
                list.Add(new ProductInfo()
                {
                    RUName = item.Value<string>("RUName"),
                    ENName = item.Value<string>("ENName"),
                    SpritePath = item.Value<string>("SpritePath")
                });
            }

            return list;
        }
    }
}