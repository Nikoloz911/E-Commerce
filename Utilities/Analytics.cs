using E_Commerce.Data;
using Microsoft.EntityFrameworkCore;
namespace E_Commerce.Utilities;
internal class Analytics
{ 
    public static void ConsoleLine()
    {
        int length = 47;
        Console.WriteLine(new string('-', length));
    }
    public static void ProductsNoData()
    {
        ConsoleClear();
        SetYellowColor(() =>
        {
            ConsoleLine();
            ColorfulWriteLine("Products Was Not found", ConsoleColor.Yellow);
            ConsoleLine();
        });  
    }
    public static void InvalidChoice()
    {
        ConsoleClear();
        SetRedColor(() =>
        {
            ConsoleLine();
            Console.WriteLine("Invalid Choice");
            ConsoleLine();
        });
    }
    public static void ConsoleLineLong()
    {
        int length = 114;
        Console.WriteLine(new string('-', length));
    }
    public static void ConsoleClear()
    {
        Console.Clear();
    }
    public static void ColorfulWriteLine(string text, ConsoleColor color)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = originalColor;
    }
    /// funqcia ucvlis fers mwvaned /// funqcia ucvlis fers mwvaned 
    public static void SetGreenColor(Action action)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        action();
        Console.ResetColor();
    }
    /// funqcia ucvlis fers mwvaned /// funqcia ucvlis fers mwvaned 
    /// funqcia ucvlis fers yvitlad /// funqcia ucvlis fers yvitlad
    public static void SetYellowColor(Action action)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        action();
        Console.ResetColor();
    }
    /// funqcia ucvlis fers yvitlad /// funqcia ucvlis fers yvitlad 
    public static void SetRedColor(Action action)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        action();
        Console.ResetColor();
    }
    public static void InitializeAnalytics()
    {
        DataContext _context = new DataContext();
        ConsoleClear();
    ColorfulWriteLine("1. Top Products", ConsoleColor.Green);
    ColorfulWriteLine("2. Active Users", ConsoleColor.Yellow);
    ColorfulWriteLine("3. Sales Report", ConsoleColor.Blue);
    ColorfulWriteLine("4. Statistics by Categories", ConsoleColor.Cyan);
    ColorfulWriteLine("5. Sort Orders by Status", ConsoleColor.Magenta);
    ColorfulWriteLine("6. The Best Selling Products", ConsoleColor.DarkRed);
    ColorfulWriteLine("7. Sort Users by number of orders", ConsoleColor.Red);
    string AnalyticsChoice = Console.ReadLine();
        if (AnalyticsChoice == "1")
        {
            bool FilterProducts = true;
            while (FilterProducts)
            {
                ConsoleClear();
                ColorfulWriteLine("1. Top 3 Products", ConsoleColor.DarkRed);
                ColorfulWriteLine("2. Products By Name", ConsoleColor.Red);
                ColorfulWriteLine("3. Products By Quantity (Ascending)", ConsoleColor.DarkYellow);
                ColorfulWriteLine("4. Products By Quantity (Descending)", ConsoleColor.Yellow);
                ColorfulWriteLine("5. Products By Price (Ascending)", ConsoleColor.DarkGreen);
                ColorfulWriteLine("6. Products By Price (Descending)", ConsoleColor.Green);
                ColorfulWriteLine("7. Products By Category", ConsoleColor.DarkCyan);
                ColorfulWriteLine("8. Available Products", ConsoleColor.Cyan);
                ColorfulWriteLine("9. Unavailable Products", ConsoleColor.Blue);
                ColorfulWriteLine("10. Main Menu", ConsoleColor.Magenta);
                string ProductsFilterChoice = Console.ReadLine();
                /// FILTER BY PRICE (FIRST 3) /// FILTER BY PRICE (FIRST 3)
                if (ProductsFilterChoice == "1")
                {
                    ConsoleClear();
    var TopProductsByName = _context.Products
         .Include(p => p.Category)
        .OrderByDescending(p => p.Price)
        .Take(3)
        .ToList();
                    if (!TopProductsByName.Any())
                    {
                        ProductsNoData();
                         FilterProducts = false;
                    }
                    else
                    {
                        ConsoleClear();
                        SetGreenColor(() => { ConsoleLineLong(); });
                        foreach (var product in TopProductsByName)
                        {
                            SetYellowColor(() =>
                            {
                                product.PrintProducts();
                            });    
                        }
                        SetGreenColor(() => { ConsoleLineLong(); });
                        ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                        ColorfulWriteLine("2. Filter Menu", ConsoleColor.DarkCyan);
                        string FilterProductBackChoice = Console.ReadLine();
if (FilterProductBackChoice == "1")
{
    ConsoleClear();
    FilterProducts = false;
}
else if (FilterProductBackChoice == "2")
{
    ConsoleClear();
    FilterProducts = true;
}
else
{
    InvalidChoice();
    FilterProducts = false;
}

                    }
                }
                /// FILTER BY PRICE (FIRST 3) /// FILTER BY PRICE (FIRST 3)
                /// FILTER BY NAME /// FILTER BY NAME /// FILTER BY NAME
                else if (ProductsFilterChoice == "2")
{
    ConsoleClear();
    var TopProductsByName = _context.Products
                        .Include(p => p.Category)
                        .OrderBy(p => p.Name).ToList();
    if (!TopProductsByName.Any())
    {
        ProductsNoData();
        FilterProducts = false;
    }
    else
    {
        ConsoleClear();
        SetGreenColor(() => { ConsoleLineLong(); });
            foreach (var product in TopProductsByName)
        {
            ColorfulWriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.StockQuantity}, Is Available: {product.IsAvailable}, Category: {product.Category?.Name ?? "No Category"}", ConsoleColor.Green);
        }
        SetGreenColor(() => { ConsoleLineLong(); });
        ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
        ColorfulWriteLine("2. Filter Menu", ConsoleColor.DarkCyan);
        string FilterProductBackChoice = Console.ReadLine();
        if (FilterProductBackChoice == "1")
        {
            ConsoleClear();
            FilterProducts = false;
        }
        else if (FilterProductBackChoice == "2")
        {
            ConsoleClear();
            FilterProducts = true;
        }
        else
        {
            InvalidChoice();
            FilterProducts = false;
        }
    }
}
/// FILTER BY NAME /// FILTER BY NAME /// FILTER BY NAME
/// FILTER BY QUANTITY ASCENDING /// FILTER BY QUANTITY ASCENDING
else if (ProductsFilterChoice == "3")
{
    ConsoleClear();
    var TopProductsByQuantity = _context.Products
        .Include(p => p.Category)
        .OrderBy(p => p.StockQuantity)
        .ToList();
    if (!TopProductsByQuantity.Any())
    {
        ProductsNoData();
        FilterProducts = false;
    }
    else
    {
        ConsoleClear();
        SetGreenColor(() => { ConsoleLineLong(); });
        foreach (var product in TopProductsByQuantity)
        {
            ColorfulWriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.StockQuantity}, Is Available: {product.IsAvailable}, Category: {product.Category?.Name ?? "No Category"}", ConsoleColor.Green);
        }
        SetGreenColor(() => { ConsoleLineLong(); });
        ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
        ColorfulWriteLine("2. Filter Menu", ConsoleColor.DarkCyan);
        string FilterProductBackChoice = Console.ReadLine();
        if (FilterProductBackChoice == "1")
        {
            ConsoleClear();
            FilterProducts = false;
        }
        else if (FilterProductBackChoice == "2")
        {
            ConsoleClear();
            FilterProducts = true;
        }
        else
        {
            InvalidChoice();
            FilterProducts = false;
        }
    }
}
/// FILTER BY QUANTITY ASCENDING /// FILTER BY QUANTITY ASCENDING
/// FILTER BY QUANTITY DESCENDING /// FILTER BY QUANTITY DESCENDING
else if (ProductsFilterChoice == "4")
{
    ConsoleClear();
    var TopProductsByQuantity = _context.Products
        .Include(p => p.Category)
        .OrderByDescending(p => p.StockQuantity)
        .ToList();
    if (!TopProductsByQuantity.Any())
    {
        ProductsNoData();
        FilterProducts = false;
    }
    else
    {
        ConsoleClear();
        SetGreenColor(() => { ConsoleLineLong(); });
        foreach (var product in TopProductsByQuantity)
        {
            ColorfulWriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.StockQuantity}, Is Available: {product.IsAvailable}, Category: {product.Category?.Name ?? "No Category"}", ConsoleColor.Green);
        }
        SetGreenColor(() => { ConsoleLineLong(); });
        ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
        ColorfulWriteLine("2. Filter Menu", ConsoleColor.DarkCyan);
        string FilterProductBackChoice = Console.ReadLine();
        if (FilterProductBackChoice == "1")
        {
            ConsoleClear(); 
            FilterProducts = false;
        }
        else if (FilterProductBackChoice == "2")
        {
            ConsoleClear();
            FilterProducts = true;
        }
        else
        {
            InvalidChoice();
            FilterProducts = false;
        }
    }
}
/// FILTER BY QUANTITY DESCENDING /// FILTER BY QUANTITY DESCENDING
/// FILTER BY PRICE ASCENDING /// FILTER BY PRICE ASCENDING
else if (ProductsFilterChoice == "5")
{
    ConsoleClear();
    var TopProductsByPrice = _context.Products
        .Include(p => p.Category)
        .OrderBy(p => p.Price)
        .ToList();
    if (!TopProductsByPrice.Any())
    {
        ProductsNoData();
        FilterProducts = false;
    }
    else
    {
        ConsoleClear();
        SetGreenColor(() => { ConsoleLineLong(); });
        foreach (var product in TopProductsByPrice)
        {
            ColorfulWriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.StockQuantity}, Is Available: {product.IsAvailable}, Category: {product.Category?.Name ?? "No Category"}", ConsoleColor.Green);
        }
        SetGreenColor(() => { ConsoleLineLong(); });
        ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
        ColorfulWriteLine("2. Filter Menu", ConsoleColor.DarkCyan);
        string FilterProductBackChoice = Console.ReadLine();
        if (FilterProductBackChoice == "1")
        {
            ConsoleClear();
            FilterProducts = false;
        }
        else if (FilterProductBackChoice == "2")
        {
            ConsoleClear();
            FilterProducts = true;
        }
        else
        {
            InvalidChoice();
            FilterProducts = false;
        }
    }
}
/// FILTER BY PRICE ASCENDING /// FILTER BY PRICE ASCENDING
/// FILTER BY PRICE DESCENDING /// FILTER BY PRICE DESCENDING
else if (ProductsFilterChoice == "6")
{
    ConsoleClear();
    var TopProductsByPrice = _context.Products
        .Include(p => p.Category)
        .OrderByDescending(p => p.Price)
        .ToList();
    if (!TopProductsByPrice.Any())
    {
        ProductsNoData();
        FilterProducts = false;
    }
    else
    {
        ConsoleClear();
         SetGreenColor(() => { ConsoleLineLong(); });
        foreach (var product in TopProductsByPrice)
        {
            ColorfulWriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.StockQuantity}, Is Available: {product.IsAvailable}, Category: {product.Category?.Name ?? "No Category"}", ConsoleColor.Green);
        }
        SetGreenColor(() => { ConsoleLineLong(); });
        ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
        ColorfulWriteLine("2. Filter Menu", ConsoleColor.DarkCyan);
        string FilterProductBackChoice = Console.ReadLine();
        if (FilterProductBackChoice == "1")
        {
            ConsoleClear();
            FilterProducts = false;
        }
        else if (FilterProductBackChoice == "2")
        {
            ConsoleClear();
            FilterProducts = true;
        }
        else
        {
            InvalidChoice();
            FilterProducts = false;
        }
    }
}
/// FILTER BY PRICE DESCENDING /// FILTER BY PRICE DESCENDING
/// FILTER BY CATEGORIES /// FILTER BY CATEGORIES /// FILTER BY CATEGORIES
else if (ProductsFilterChoice == "7")
{
    ConsoleClear();
    var TopProductsByCategory = _context.Products
                         .Include(p => p.Category)
             .Join(_context.Categories, // join
                     p => p.CategoryId,
                     c => c.Id,
                     (p, c) => new { p, c })

                    .OrderBy(pc => pc.c.Name)
                    .Select(pc => pc.p)
                    .ToList();
    if (!TopProductsByCategory.Any())
    {
        ProductsNoData();
        FilterProducts = false;
    }
    else
    {
        ConsoleClear();
         SetGreenColor(() => { ConsoleLineLong(); });
        foreach (var product in TopProductsByCategory)
        {
            ColorfulWriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.StockQuantity}, Is Available: {product.IsAvailable}, Category: {product.Category?.Name ?? "No Category"}", ConsoleColor.Green);
        }
        SetGreenColor(() => { ConsoleLineLong(); });
        ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
        ColorfulWriteLine("2. Filter Menu", ConsoleColor.DarkCyan);
        string FilterProductBackChoice = Console.ReadLine();
        if (FilterProductBackChoice == "1")
        {
            ConsoleClear();
            FilterProducts = false;
        }
        else if (FilterProductBackChoice == "2")
        {
            ConsoleClear();
            FilterProducts = true;
        }
        else
        {
            InvalidChoice();
            FilterProducts = false;
        }
    }
}
/// FILTER BY CATEGORIES /// FILTER BY CATEGORIES /// FILTER BY CATEGORIES
/// FILTER BY - IS AVAILABLE - /// FILTER BY - IS AVAILABLE -
else if (ProductsFilterChoice == "8")
{
    ConsoleClear();
    var TopProductsByPrice = _context.Products
         .Include(p => p.Category)
        .Where(p => p.IsAvailable)
        .ToList();
    if (!TopProductsByPrice.Any())
    {
        ProductsNoData();
        FilterProducts = false;
    }
    else
    {
        ConsoleClear();
         SetGreenColor(() => { ConsoleLineLong(); });
        foreach (var product in TopProductsByPrice)
        {
            ColorfulWriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.StockQuantity}, Is Available: {product.IsAvailable}, Category: {product.Category?.Name ?? "No Category"}", ConsoleColor.Green);
        }
        SetGreenColor(() => { ConsoleLineLong(); });
        ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
        ColorfulWriteLine("2. Filter Menu", ConsoleColor.DarkCyan);
        string FilterProductBackChoice = Console.ReadLine();
        if (FilterProductBackChoice == "1")
        {
            ConsoleClear();
            FilterProducts = false;
        }
        else if (FilterProductBackChoice == "2")
        {
            ConsoleClear();
            FilterProducts = true;
        }
        else
        {
            InvalidChoice();
            FilterProducts = false;
        }
    }
}
/// FILTER BY - IS AVAILABLE - /// FILTER BY - IS AVAILABLE -
/// FILTER BY - IS UNAVAILABLE - /// FILTER BY - IS UNAVAILABLE -
else if (ProductsFilterChoice == "9")
{
    ConsoleClear();
    var TopProductsByPrice = _context.Products
        .Include(p => p.Category)
        .Where(p => !p.IsAvailable)
        .ToList();
    if (!TopProductsByPrice.Any())
    {
        ProductsNoData();
        FilterProducts = false;
    }
    else
    {
        ConsoleClear();
        SetGreenColor(() => { ConsoleLineLong(); });
        foreach (var product in TopProductsByPrice)
        {
            ColorfulWriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.StockQuantity}, Is Available: {product.IsAvailable}, Category: {product.Category?.Name ?? "No Category"}", ConsoleColor.Green);
        }
        SetGreenColor(() => { ConsoleLineLong(); });
        ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
        ColorfulWriteLine("2. Filter Menu", ConsoleColor.DarkCyan);
        string FilterProductBackChoice = Console.ReadLine();
        if (FilterProductBackChoice == "1")
        {
            ConsoleClear();
            FilterProducts = false;
        }
        else if (FilterProductBackChoice == "2")
        {
            ConsoleClear();
            FilterProducts = true;
        }
        else
        {
            InvalidChoice();
            FilterProducts = false;
        }
    }
}
/// FILTER BY - IS UNAVAILABLE - /// FILTER BY - IS UNAVAILABLE -
/// BACK /// BACK /// BACK /// BACK /// BACK /// BACK /// BACK
else if (ProductsFilterChoice == "10")
{
    ConsoleClear();
    FilterProducts = false;
}
/// BACK /// BACK /// BACK /// BACK /// BACK /// BACK /// BACK
else
{
    InvalidChoice();
    FilterProducts = false;
}
            }
        }
        /// ACTIVE USERS /// ACTIVE USERS /// ACTIVE USERS /// ACTIVE USERS
        else if (AnalyticsChoice == "2")
{
    ConsoleClear();
    var custumersFromDataBase = _context.Custumers
.Include(c => c.CustumerDetails)
.ToList();
    if (!custumersFromDataBase.Any())
    {
        ConsoleClear();
        SetRedColor(() =>
        {
            ConsoleLine();
            Console.WriteLine("No User data found!");
            ConsoleLine();
        });       
    }
    else
    {
        ConsoleClear();
        SetGreenColor(() => { ConsoleLine(); });
        foreach (var custumer in custumersFromDataBase)
        {
            ColorfulWriteLine($"User ID: {custumer.Id}, Name: {custumer.FirstName}, LastName: {custumer.LastName}", ConsoleColor.DarkCyan);
        }
        SetGreenColor(() => { ConsoleLine(); });
    }
}
/// ACTIVE USERS /// ACTIVE USERS /// ACTIVE USERS /// ACTIVE USERS
/// SALES REPORT /// SALES REPORT /// SALES REPORT /// SALES REPORT
else if (AnalyticsChoice == "3")
{
    ConsoleClear();
    var orderItems = _context.OrderItems
        .Where(oi => oi.ProductId != null)
        .ToList();

    if (orderItems.Count == 0)
    {
        ConsoleClear();
                SetRedColor(() =>
                {
                    ConsoleLine();
                    Console.WriteLine("No products have been sold.");
                    ConsoleLine();
                });        
    }
            SetGreenColor(() => { ConsoleLine(); });
            ColorfulWriteLine("List of Sold Products:", ConsoleColor.Yellow);
            SetGreenColor(() => { ConsoleLine(); });
            int productCounter = 1;
    foreach (var orderItem in orderItems)
    {
        var product = _context.Products
            .FirstOrDefault(p => p.Id == orderItem.ProductId);
        if (product != null)
        {
            ColorfulWriteLine($"Product {productCounter}: '{product.Name}'", ConsoleColor.Yellow);
            productCounter++;
        }
    }
            SetGreenColor(() => { ConsoleLineLong(); });
        }

/// SALES REPORT /// SALES REPORT /// SALES REPORT /// SALES REPORT
else if (AnalyticsChoice == "4")
{
    bool statisticCategories = true;
    while (statisticCategories)
    {
                ConsoleClear();
                var categories = _context.Categories.ToList();
                ConsoleColor[] colors = new ConsoleColor[]
                {
                     ConsoleColor.Green,
                     ConsoleColor.Blue,
                     ConsoleColor.Cyan,
                     ConsoleColor.Yellow,
                     ConsoleColor.Magenta,
                     ConsoleColor.DarkCyan
                };
                SetGreenColor(() => { ConsoleLine(); });
                int colorIndex = 0;
                foreach (var category in categories)
                {
                    ConsoleColor color = colors[colorIndex % colors.Length];
                    ColorfulWriteLine($"{category.Id}. {category.Name}", color);
                    colorIndex++;
                }
                SetGreenColor(() => { ConsoleLine(); });
                string StatisticCategoriesChoice = Console.ReadLine();
                if (int.TryParse(StatisticCategoriesChoice, out int categoryId))
                {
                    var selectedCategory = categories.FirstOrDefault(c => c.Id == categoryId);
                    if (selectedCategory != null)
                    {
                        ConsoleClear();
                        var products = _context.Products
                            .Include(p => p.Category)
                            .Where(p => p.Category.Id == selectedCategory.Id)
                            .OrderBy(p => p.Name)
                            .ToList();

                        if (!products.Any())
                        {
                            ProductsNoData();
                            statisticCategories = false;
                        }
                        else
                        {
                            ColorfulWriteLine($"{selectedCategory.Name}:", ConsoleColor.Green);
                            SetGreenColor(() => { ConsoleLineLong(); });

                            foreach (var product in products)
                            {
                                ColorfulWriteLine(
                                    $"Name: {product.Name}, Price: {product.Price}, Quantity: {product.StockQuantity}, Is Available: {product.IsAvailable}, Category: {product.Category?.Name ?? "No Category"}",
                                    ConsoleColor.Green
                                );
                                SetGreenColor(() => { ConsoleLineLong(); });
                            }
                            ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                            ColorfulWriteLine("2. Statistics By Categories Menu", ConsoleColor.Yellow);
                            string StatisticCategoriesBackChoice = Console.ReadLine();
                            SetGreenColor(() => { ConsoleLine(); });
                            if (StatisticCategoriesBackChoice == "1")
                            {
                                statisticCategories = false;
                                ConsoleClear();
                            }
                            else if (StatisticCategoriesBackChoice == "2")
                            {
                                
                            }
                            else
                            {
                                InvalidChoice();
                                statisticCategories = false;
                            }
                        }
                    }
                    else
                    {
                        InvalidChoice();
                    }
                }
        else
        {
            InvalidChoice();
            statisticCategories = false;
        }
    }
}
/// Group orders by their status  /// Group orders by their status
else if (AnalyticsChoice == "5")
{
            ConsoleClear();
            var ordersByStatus = _context.Orders
                .Include(o => o.Custumer) 
                .GroupBy(o => o.Status)
                .Select(g => new
                {
                    Status = g.Key,
                    Orders = g.Select(o => new
                    {
                        o.Id,
                        CustomerFirstName = o.Custumer.FirstName,
                        CustomerLastName = o.Custumer.LastName,
                        o.TotalAmount
                    }).ToList()
                })
                .ToList();
            if (!ordersByStatus.Any())
            {
                SetRedColor(() =>
                {
                    ConsoleLine();
                    Console.WriteLine("No order was found in the Database.");
                    ConsoleLine();
                });
            }
            else
            {
                SetGreenColor(() =>
                {
                    ConsoleLine();
                });
                ColorfulWriteLine("Orders grouped by status:", ConsoleColor.Magenta);
                foreach (var group in ordersByStatus)
                {
                    SetGreenColor(() =>
                    {
                        ConsoleLine();
                    });
                    ColorfulWriteLine($"Status: {group.Status}", ConsoleColor.DarkCyan);

                    foreach (var order in group.Orders)
                    {
                        ColorfulWriteLine($"Order ID: {order.Id}, User: {order.CustomerFirstName} {order.CustomerLastName}, Total: {order.TotalAmount}", ConsoleColor.Yellow);
                    }
                }
                SetGreenColor(() =>
                {
                    ConsoleLine();
                });
            }
        }
        /// Group orders by their status  /// Group orders by their status
        /// The Most Saled Products Products  /// The Most Saled Products Products
        else if (AnalyticsChoice == "6")
{
    ConsoleClear();
    var productSales = _context.OrderItems
        .GroupBy(oi => oi.ProductId)
        .Select(group => new
        {
            ProductId = group.Key,
            TotalSold = group.Sum(oi => oi.Quantity)
        })
        .OrderByDescending(p => p.TotalSold)
        .FirstOrDefault();
    if (productSales == null)
    {
        ConsoleClear();
                SetYellowColor(() =>
                {
                    ConsoleLine();
                    Console.WriteLine("No sales data available.");
                    ConsoleLine();
                });     
    }
    else
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == productSales.ProductId);
        if (product != null)
        {
            ConsoleLine();
            ColorfulWriteLine("The Most Sold Products:", ConsoleColor.Yellow);
                    SetGreenColor(() => { ConsoleLine(); });
                    ColorfulWriteLine($"Product: {product.Name}, Total Sold: {productSales.TotalSold}", ConsoleColor.Green);
                    SetGreenColor(() => { ConsoleLine(); });
        }
        else
        {
            ConsoleClear();
                    SetYellowColor(() =>
                    {
                        ConsoleLine();
                        Console.WriteLine($"No product was found for the most sold item.");
                        ConsoleLine();
                    });         
        }
    }
}
/// The Most Saled Products Products  /// The Most Saled Products Products
/// Grouping Users by Number of orders  /// Grouping Users by Number of orders
else if (AnalyticsChoice == "7")
{
    ConsoleClear();
    var customersWithOrderCounts = _context.Custumers
        .Select(c => new
        {
            CustomerId = c.Id,
            Name = $"{c.FirstName} {c.LastName}",
            OrderCount = _context.Orders.Count(o => o.CustumerId == c.Id)
        })
        .OrderByDescending(c => c.OrderCount)
        .ToList();
    if (!customersWithOrderCounts.Any())
    {
                SetRedColor(() =>
                {
                    ConsoleLine();
                    Console.WriteLine("No Users was found in the database.");
                    ConsoleLine();
                });     
    }
            SetGreenColor(() => { ConsoleLine(); });
             ColorfulWriteLine("Users Grouped by Number of Orders:", ConsoleColor.Yellow);
            SetGreenColor(() => { ConsoleLine(); });
    int rank = 1;
    foreach (var customer in customersWithOrderCounts)
    {
        ColorfulWriteLine($"{rank}. {customer.Name} - {customer.OrderCount} Orders", ConsoleColor.Green);
        rank++;
    }
            SetGreenColor(() => { ConsoleLine(); });
        }
/// Grouping Users by Number of orders  /// Grouping Users by Number of orders
else
{
    InvalidChoice();
}
    }
}
