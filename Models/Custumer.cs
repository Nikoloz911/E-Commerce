using E_Commerce.Interfaces;
namespace E_Commerce.Models;
internal class Custumer : IUser
{ 
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }  
    public CustumerDetails CustumerDetails { get; set; } /// Navigation Property
    public List<Order> Orders { get; set; }              /// Navigation Property
    public Custumer() { }
    public Custumer( /*int id,*/ string firstName, string lastName, string email, DateTime registrationDate, CustumerDetails custumerDetails, List<Order> orders)
    {
        //Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        RegistrationDate = registrationDate;
        CustumerDetails = custumerDetails;
        Orders = orders;
    }
    public void PrintUserData()
    {
        Console.WriteLine($"Name: {FirstName}, LastName: {LastName}, Email: {Email}, RegistrationDate: {RegistrationDate}");
    }
}
