using ExamModul_2.Services;

namespace ExamModul_2
{
    public class Program
    {
     
        static void Main()
        {
            var restaurantService = new RestaurantService();
            List<string> mainMenu = new List<string>()
            {
                "Admin",
                "Customer",
                "Report",
                "Exit"
            };
            List<string> adminMenu = new List<string>()
            {
                "Categories",
                "Products",
                "Orders",
                "About",
                "Back"
            };
            List<string> customerMenu = new List<string>()
            {
                "Order",
                "My Orders",
                "About",
                "Back"
            };
            List<string> reportMenu = new List<string>()
            {
                "Common Ordered Products",
                "Back"
            };
            List<string> adminCategoriesMenu = new List<string>()
            {
                "Add Category",
                "Update Category",
                "Delete Category",
                "Show Categories",
                "Back"
            };
            List<string> adminProductMenu = new List<string>()
            {
                "Add Product",
                "Update Product",
                "Delete Product",
                "Attach Product To Category",
                "Show Products",
                "Back"
            };
            List<string> adminOrderMenu = new List<string>()
            {
                "Show Orders",
                "Show Accepted Orders",
                "Show Deseased Orders",
                "Back"
            };
            List<string> adminAboutMenu = new List<string>()
            {
                "Add About",
                "Update About",
                "Delete About",
                "Show About",
                "Back"
            };
            List<string> customerOrderMenu = new List<string>()
            {
                "Add Order",
                "Back"
            };

        main:
            int mainOption = ArrowIndex(mainMenu, "Welcome to Restourant");
            switch (mainOption)
            {
                case 0:
                admin:
                    int adminOption = ArrowIndex(adminMenu, "Main:\\Admin");
                    switch (adminOption)
                    {
                        case 0:
                        category:
                            int adminCategory = ArrowIndex(adminCategoriesMenu, "Main:\\Admin\\Category");
                            switch (adminCategory)
                            {
                                case 0:
                                    restaurantService.AddCategory();
                                    goto category;
                                case 1:
                                    restaurantService.UpdateCategory();
                                    goto category;
                                case 2:
                                    restaurantService.DeleteCategory();
                                    goto category;
                                case 3:
                                    restaurantService.ListCategories();
                                    goto category;
                                case 4:
                                    goto admin;
                            }
                            goto admin;
                        case 1:
                        product:
                            int adminProduct = ArrowIndex(adminProductMenu, "Main:\\Admin\\Product");
                            switch (adminProduct)
                            {
                                case 0:
                                    restaurantService.AddProduct();
                                    goto product;
                                case 1:
                                    restaurantService.UpdateProduct();
                                    goto product;
                                case 2:
                                    restaurantService.DeleteProduct();
                                    goto product;
                                case 3:
                                    restaurantService.AttachProductToCategory();
                                    goto product;
                                case 4:
                                    restaurantService.ListProducts();
                                    goto product;
                                case 5:
                                    goto admin;
                            }
                            goto admin;
                        case 2:
                        adminOrder:
                            int adminOrder = ArrowIndex(adminOrderMenu, "Main:\\Admin\\Orders");
                            switch (adminOrder)
                            {
                                case 0:
                                    restaurantService.MyOrder();
                                    goto adminOrder;

                                case 1:
                                    //show acceptedS
                                    goto adminOrder;

                                case 2:
                                    //show deseased
                                    goto adminOrder;
                                case 3:
                                    goto admin;
                            }
                            goto admin;
                        case 3:
                            adminAbout:
                            int adminAbout = ArrowIndex(adminAboutMenu, "Main:\\Admin\\About");
                            switch (adminAbout)
                            {
                                case 0:
                                    //add
                                    goto adminAbout;
                                case 1:
                                    //update
                                    goto adminAbout;
                                case 2:
                                    //delete
                                    goto adminAbout;
                                case 3:
                                    //shpw
                                    goto adminAbout;
                                case 4:
                                    goto admin;
                            }
                            goto admin;
                    }
                    goto main;
                case 1:
                customer:
                    int customerOption = ArrowIndex(customerMenu, "Customer");
                    switch (customerOption)
                    {
                        case 0:
                        customerOrder:
                            int cusomerOrder = ArrowIndex(customerOrderMenu, "Main:\\Customer\\Order");
                            switch (cusomerOrder)
                            {
                                case 0:
                                    restaurantService.AddOrder();
                                    goto customerOrder;
                                case 1:
                                    goto customer;
                            }
                            goto customer;
                        case 1:
                            restaurantService.MyOrder();
                            goto customer;
                        case 2:
                            //about
                            goto customer;
                        case 3:
                            goto main;
                    }
                    goto main;
                case 2:
                report:
                    int reportOption = ArrowIndex(reportMenu, "Main:\\Report");
                    switch (reportOption)
                    {
                        case 0:
                            //commonProducts
                            goto report;
                        case 1:
                            goto main;
                    }
                    goto main;
                case 3:
                    Console.WriteLine("Enjoy your meal:)");
                    break;
            }
        }

        public static int ArrowIndex(List<string> list, string name)
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
                    Console.WriteLine(list[i]);
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
