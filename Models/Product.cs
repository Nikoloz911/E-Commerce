using E_Commerce.Interfaces;
namespace E_Commerce.Models;
internal class Product : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }  /// Key
    public int StockQuantity { get; set; }
    public bool IsAvailable { get; set; } 
    public Category Category { get; set; } /// Navigation Property
    public List<OrderItem> OrderItems { get; set; } /// Navigation Property
    /*public OrderItem OrderItem { get; set; }*/ /// Navigation Property ||
    public Product() { }
    public Product(string name, string description, decimal price, int categoryId, int stockQuantity, bool isAvailable, Category category, OrderItem orderItem)
    {
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
        StockQuantity = stockQuantity;
        IsAvailable = isAvailable;
        Category = category;
        //OrderItem = orderItem;
    }
    public void PrintProducts()
    {
        Console.WriteLine($"Name: {Name}, Price: {Price}, Quantity: {StockQuantity}, Is Available: {IsAvailable}, Category: {Category.Name}");
    }
}
