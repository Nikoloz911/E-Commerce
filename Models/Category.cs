namespace E_Commerce.Models;
internal class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Product> Products { get; set; } /// Navigation Property
    public Category() { }
    public Category(string name, string description, List<Product> products)
    {
        Name = name;
        Description = description;
        Products = products;
    }
}
