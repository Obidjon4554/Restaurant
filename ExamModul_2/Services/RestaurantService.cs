using ExamLibrary;

namespace ExamModul_2.Services
{
    public partial class RestaurantService
    {
        List<Category> categories = new List<Category>();
        List<Order> orders = new List<Order>();
        List<Product> products = new List<Product>();
        public RestaurantService()
        {
             EnsureFileExists();
            categories = JsonReadCategory();
            orders = JsonReadOrder();
            products = JsonReadProduct();
        }

        private void EnsureFileExists()
        {
            if (!File.Exists(jsonPathCategory))
            {
                File.WriteAllText(jsonPathCategory, "[]");
            }
            if (!File.Exists(jsonPathProduct))
            {
                File.WriteAllText(jsonPathProduct, "[]");
            }
            if (!File.Exists(jsonPathOrder))
            {
                File.WriteAllText(jsonPathOrder, "[]");
            }
        }
    }
}
