using KittyCook.Data;
using TMPro;
using UnityEngine;

namespace KittyCook.Tech
{
    public class ProductForCookElement : MonoBehaviour
    {
        [Header("Base Components")]
        [SerializeField]
        protected TMP_Text nameText;

        public ProductInfo Info { get; private set; }

        public void Set(ProductInfo info)
        {
            Info = info;

            nameText.text = $"{info.ENName}";
        }
    }
}