using Microsoft.EntityFrameworkCore;
using E_Commerce.Models;
namespace E_Commerce.Data;
internal class DataContext : DbContext
{
    public DbSet<Custumer> Custumers { get; set; }
    public DbSet<CustumerDetails> CustumerDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=LENOVO\SQLEXPRESS;Initial Catalog=E-commerce;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }
}
