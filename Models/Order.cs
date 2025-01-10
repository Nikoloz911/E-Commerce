namespace E_Commerce.Models;
internal class Order
{
    public int Id { get; set; }
    public int CustumerId { get; set; }  /// Key
    public DateTime OrderDate { get; set; }  
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } /*(string - "Pending"/"Confirmed"/"Shipped"/"Delivered")*/
    public string ShippingAddress { get; set; }
    public string PaymentStatus { get; set; }/* (string - "Pending"/"Completed")*/
    public OrderItem OrderItem { get; set; }  /// Navigation Property
    public Custumer Custumer { get; set; }  /// Navigation Property
    public void PrintOrder()
    {
        Console.WriteLine($"Total Amount: {TotalAmount}, Status: {Status}");
        Console.WriteLine($"Shipping Address: {ShippingAddress}, PaymentStatus: {PaymentStatus}");
    }
    public void PrintOrderFirstLine()
    {
        Console.WriteLine($"Total Amount: {TotalAmount}, Status: {Status}");
    }
}
