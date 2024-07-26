using System.Text.Json;
using ExamLibrary;

namespace ExamModul_2.Services
{
    public partial class RestaurantService
    {
        private string jsonPathCategory = Path.Combine(Directory.GetCurrentDirectory(), "categories.json");
        public bool AddCategory()
        {
            Console.Write("Enter Category Name: ");
            string name = Console.ReadLine();
            if (name != "")
            {
                int id = categories.Count > 0 ? categories.Max(k => k.Id) + 1 : 1;
                categories.Add(new Category() { Id = id, Name = name });

                string serialized = JsonSerializer.Serialize(categories);
                using (StreamWriter writer = new StreamWriter(jsonPathCategory))
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


        public void UpdateCategory()
        {
            if (categories.Count > 0)
            {
                int updateTOption = ArrowIndex(categories, "Update Category");
                var Category = categories.FirstOrDefault(k => k.Id == updateTOption + 1);
                Console.Write("Enter Category new Name: ");
                string name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                {
                    Category.Name = name;
                    Console.WriteLine("Successfuly updated");
                }
                else
                {
                    Console.WriteLine("Category not found");
                }
            }
            else
            {
                Console.WriteLine("Category list is empty");
            }
            string serialized = JsonSerializer.Serialize<List<Category>>(categories);
            using (StreamWriter sw = new StreamWriter(jsonPathCategory))
            {
                sw.WriteLine(serialized);
            }
            Console.ReadKey();
        }

        public void DeleteCategory()
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
            Console.ReadKey();
        }

        public bool ListCategories()
        {
            categories = JsonReadCategory();
            if (categories.Count > 0)
            {
                foreach (var Category in categories)
                {
                    Console.WriteLine($"Category: {Category.Id}, Name: {Category.Name}");
                }
                Console.ReadKey();
                Console.ReadKey();
                return true;
            }
            else
            {
                Console.WriteLine("Category not found!");
                Console.ReadKey();

                Console.ReadKey();
                return false;
            }
        }
        public List<Category> JsonReadCategory()
        {
            string json = File.ReadAllText(jsonPathCategory);
            return string.IsNullOrWhiteSpace(json) ? new List<Category>() : JsonSerializer.Deserialize<List<Category>>(json);
        }

        private int ArrowIndex(List<Category> list, string name)
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
