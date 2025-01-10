namespace E_Commerce.Models;
internal class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; } /// Key
    public int ProductId { get; set; } /// Key
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public Order Order { get; set; }    /// Navigation Property
    public List<Product> Products { get; set; } /// Navigation Property
   /* public Product Product { get; set; }*/ /// Navigation Property
    public OrderItem() { }
    public OrderItem(/*int id,*/ int orderId, int productId, int quantity, decimal unitPrice, decimal totalPrice, Order order, Product product)
    {
        //Id = id;
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalPrice = totalPrice;
        Order = order;
        //Product = product;
    }
    public void printOrderItem()
    {
        Console.WriteLine($"Quantity: {Quantity}, UnitPrice: {UnitPrice}, TotalPrice: {TotalPrice}");
    }
}
