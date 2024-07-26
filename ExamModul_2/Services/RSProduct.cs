using System.Text.Json;
using ExamLibrary;

namespace ExamModul_2.Services
{
    public partial class RestaurantService
    {
        private string jsonPathProduct = Path.Combine(Directory.GetCurrentDirectory(), "products.json");
        public bool AddProduct()
        {
            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();
            if (name != "")
            {
                int id = products.Count > 0 ? products.Max(k => k.Id) + 1 : 1;
                products.Add(new Product() { Id = id, Name = name });

                string serialized = JsonSerializer.Serialize(products);
                using (StreamWriter writer = new StreamWriter(jsonPathProduct))
                {
                    writer.WriteLine(serialized);
                }
                Console.Write("Successfuly added...");
                Console.ReadKey();
                return true;
            }
            else
            {
                Console.WriteLine("This field can not be empty!");
                Console.ReadKey();
                return false;
            }
        }


        public void UpdateProduct()
        {
            if (products.Count > 0)
            {
                int updateTOption = ArrowIndex(products, "Update Product");
                var Product = products.FirstOrDefault(k => k.Id == updateTOption + 1);
                Console.Write("Enter Product new Name: ");
                string name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                {
                    Product.Name = name;
                    Console.WriteLine("Successfuly updated");
                }
                else
                {
                    Console.WriteLine("Product not found");
                }
            }
            else
            {
                Console.WriteLine("Product list is empty");
            }
            string serialized = JsonSerializer.Serialize<List<Product>>(products);
            using (StreamWriter sw = new StreamWriter(jsonPathProduct))
            {
                sw.WriteLine(serialized);
            }
            Console.ReadKey();
        }

        public void DeleteProduct()
        {
            if (products.Count > 0)
            {
                int updateTOption = ArrowIndex(products, "Update Product");
                var Product = products.FirstOrDefault(k => k.Id == updateTOption + 1);
                if (Product != null)
                {
                    products.Remove(Product);
                    Console.WriteLine("Successfuly deleted");
                }
                else
                    Console.WriteLine("Product not found");
                string serialized = JsonSerializer.Serialize<List<Product>>(products);
                using (StreamWriter sw = new StreamWriter(jsonPathProduct))
                {
                    sw.WriteLine(serialized);
                }
            }
            else
            {
                Console.WriteLine("Product List Is Empty");
            }
            Console.ReadKey();
        }

        public bool AttachProductToCategory()
        {
            if (categories.Count > 0)
            {
                int attachCategory = ArrowIndex(categories, "Attach Product to Category");
                var category = categories.FirstOrDefault(k => k.Id == attachCategory + 1);
                int attachProduct = ArrowIndex(products, "Attach Product to Category");
                var product = products.FirstOrDefault(k => k.Id == attachProduct +2);

                if (category == null || product == null)
                {
                    Console.WriteLine("Error null");
                    return false;
                }
                var existingProductCategoryForCategory = category.ProductCategory.FirstOrDefault(ts => ts.ProductId == product.Id);
                var existingProductCategoryForProduct = product.ProductCategory.FirstOrDefault(ts => ts.CategoryId == category.Id);

                if (existingProductCategoryForCategory == null && existingProductCategoryForProduct == null)
                {
                    var ProductCategory = new ProductCategory
                    {
                        CategoryId = category.Id,
                        Category = category,
                        ProductId = product.Id,
                        Product = product
                    };

                    category.ProductCategory.Add(ProductCategory);
                    product.ProductCategory.Add(ProductCategory);
                    Console.ReadKey();
                    Console.WriteLine("Successfuly attached!");
                    return true;
                }
                else
                {
                    Console.WriteLine("You can't attach this product twice!");
                    return false;
                }
            }
            else
            {

            Console.WriteLine("Category list is empty!");
            Console.ReadKey();
            return false;
            }
        }
        public void ListProducts()
        {
            products = JsonReadProduct();
            if (categories.Count > 0)
                foreach (var category in categories)
                {
                    Console.WriteLine($"Category: {category.Name}");
                    foreach (var ts in category.ProductCategory)
                    {
                        if (ts.Product.Name != null)
                            Console.WriteLine($"Product: {ts.Product.Name}, ID: {ts.Product.Id}");

                    }
                }
            else
                Console.WriteLine("Product and Category lists are empty.");
            Console.ReadKey();
        }
        public List<Product> JsonReadProduct()
        {
            string json = File.ReadAllText(jsonPathProduct);
            return string.IsNullOrWhiteSpace(json) ? new List<Product>() : JsonSerializer.Deserialize<List<Product>>(json);
        }

        private int ArrowIndex(List<Product> list, string name)
        {
            Console.Clear();
            Console.WriteLine(name);
            int index = 0;
            while (true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (i == index)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.WriteLine(list[i].Id + ". " + list[i].Name);

                    Console.ResetColor();
                }
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                Console.Clear();

                if (consoleKeyInfo.Key == ConsoleKey.UpArrow) index = (index - 1 + list.Count) % list.Count;
                if (consoleKeyInfo.Key == ConsoleKey.DownArrow) index = (index + 1) % list.Count;
                if (consoleKeyInfo.Key == ConsoleKey.Enter) return index;
                Console.WriteLine(name);
            }
        }
    }
}
