using UnityEngine;

namespace KittyCook.Data
{
    [CreateAssetMenu(fileName = "NewProduct", menuName = "Kitty Cook/Create new product")]
    public class ProductInfo : ScriptableObject
    {
        public string RUName;
        public string ENName;
        public Sprite Sprite;
    }
}