namespace KittyCook.Tech
{
    public class MainView : BaseView
    {
        private AddRecipeView addRecipeView;
        private AddProductView addProductView;

        protected override void Awake()
        {
            base.Awake();

            addProductView = FindObjectOfType<AddProductView>(true);
            addRecipeView = FindObjectOfType<AddRecipeView>(true);
        }

        public void OnAddProductButtonClick()
        {
            Hide();
            addProductView.Show();
        }

        public void OnAddRecipeButtonClick()
        {
            Hide();
            addRecipeView.Show();
        }
    }
}