using ExamLibrary;
using System.Text.Json;

namespace ExamModul_2.Services
{
    public partial class RestaurantService
    {
        private string jsonPathOrder = Path.Combine(Directory.GetCurrentDirectory(), "orders.json");
        public bool AddOrder()
        {
            if (categories.Count > 0)
            {
                int orderCategory = ArrowIndex(categories, "Order Category");
                var category = categories.FirstOrDefault(k => k.Id == orderCategory + 1);
                int orderProduct = ArrowIndex(products, "Order Product");
                var product = products.FirstOrDefault(k => k.Id == orderProduct + 2);
                if (category == null || product == null)
                {
                    Console.WriteLine("Error null");
                    return false;
                }
                else
                {
                    orders.Add(new Order() { ProductId = product.Id, Product = product, CategoryId = category.Id,Category = category });

                    string serialized = JsonSerializer.Serialize(categories);
                    using (StreamWriter writer = new StreamWriter(jsonPathCategory))
                    {
                        writer.WriteLine(serialized);
                    }
                    Console.Write("Successfuly added...");
                    return true;
                }
            }
            Console.WriteLine("There is no Categories");
            return false;
        }


        public void AcceptOrder()
        {
                int attachCategory = ArrowIndex(categories, "Attach Product to Category");

        }

        public void DeseaseOrder()
        {
            if (categories.Count > 0)
            {
                int updateTOption = ArrowIndex(categories, "Update Category");
                var Category = categories.FirstOrDefault(k => k.Id == updateTOption + 1);
                if (Category != null)
                {
                    categories.Remove(Category);
                    Console.WriteLine("Successfuly deleted");
                }
                else
                    Console.WriteLine("Category not found");
                string serialized = JsonSerializer.Serialize<List<Category>>(categories);
                using (StreamWriter sw = new StreamWriter(jsonPathCategory))
                {
                    sw.WriteLine(serialized);
                }
            }
            else
            {
                Console.WriteLine("Category List Is Empty");
            }
        }

        public bool MyOrder()
        {
            orders = JsonReadOrder();
            if (orders.Count > 0)
            {
                foreach (var order in orders)
                {
                    Console.WriteLine("Order");
                    if(categories.Count>0)
                    Console.WriteLine($"Category: {order.CategoryId}, Name: {order.Category.Name}");
                    if(products.Count>0)
                    Console.WriteLine($"Product: {order.ProductId}, Name: {order.Product.Name}");
                }
                Console.ReadKey();
                return true;
            }
            else
            {
                Console.WriteLine("Orders not found!");
                Console.ReadKey();

                return false;
            }
        }
        public List<Order> JsonReadOrder()
        {
            string json = File.ReadAllText(jsonPathOrder);
            return string.IsNullOrWhiteSpace(json) ? new List<Order>() : JsonSerializer.Deserialize<List<Order>>(json);
        }

        private int ArrowIndex(List<Order> list, string name)
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
                    Console.WriteLine(list[i].CategoryId + ". " + list[i].Category.Name);
                    Console.WriteLine(list[i].ProductId + ". " + list[i].Product.Name);

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
