namespace E_Commerce.Models;
internal class CustumerDetails
{
    public int Id { get; set; }
    public int CustumerId { get; set; } /// Key
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; } 
    public int LoyaltyPoits { get; set; }
    public bool IsVipCostumer { get; set; }  // Costumer
    public Custumer Custumer { get; set; }  /// Navigation Property
    public CustumerDetails() {   }
        public CustumerDetails(/*int id,*/ /*int custumerId,*/ string address, string phoneNumber, DateTime birthDate, int loyaltyPoits, bool isVipCostumer, Custumer custumer)
    {
        //Id = id;
        //CustumerId = custumerId;
        Address = address;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
        LoyaltyPoits = loyaltyPoits;
        IsVipCostumer = isVipCostumer;
        Custumer = custumer;
    }
    public void PrintUserDetailsData()
    {
        Console.WriteLine($"Address: {Address}, PhoneNumber: {PhoneNumber}, LoyaltyPoints: {LoyaltyPoits}, BirthDate: {BirthDate}");
    }
}
