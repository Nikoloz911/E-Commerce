
using E_Commerce.Models;
using E_Commerce.Data;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Enums;
using E_Commerce.Utilities;
using E_Commerce.SMTP;
using BCrypt.Net;
//using E_Commerce.Interfaces;
//using Twilio;
//using Twilio.Rest.Api.V2010.Account;
//using Twilio.Types;
//using System;
//using System.Net;
//using System.Net.Mail;
//using System.Net.Sockets;
//using System.Management;
//using NAudio.Wave;

DataContext _context = new DataContext();
List<Custumer> Custumers = _context.Custumers.ToList();
List<CustumerDetails> CustumerDetails = _context.CustumerDetails.ToList();
//Dictionary<string, string> encryptedData = new Dictionary<string, string>();
// DeleteData(_context);
InitializeCategories(_context);
InitializeProducts(_context);
string logFileName = "shop_system_log.txt";
// damatebuli kategoria ar chans analitikashi
// validacia saxelze
if (!File.Exists(logFileName))
{
    using (StreamWriter writer = new StreamWriter(logFileName))
    {
        writer.WriteLine("---  Shop System Log  ---");
        writer.WriteLine("");
    }
}

/// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID
/// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID
/// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID
void ConsoleLine()
{
    int length = 47;
    Console.WriteLine(new string('-', length));
}
void Line()
{
    Console.WriteLine("---------------------");
}
void ConsoleLineLong()
{
    int length = 114;
    Console.WriteLine(new string('-', length));
}
void ConsoleClear()
{
    Console.Clear();
}
void InvalidChoice()
{
    Console.ForegroundColor = ConsoleColor.Red;
    ConsoleClear();
    ConsoleLine();
    Console.WriteLine("Invalid Choice");
    ConsoleLine();
    Console.ResetColor();
}
/// funqcua aketebs ferebs wrilinesbistvis /// funqcua aketebs ferebs wrilinesbistvis
void ColorfulWriteLine(string text, ConsoleColor color)
{
    ConsoleColor originalColor = Console.ForegroundColor;
    Console.ForegroundColor = color;
    Console.WriteLine(text);
    Console.ForegroundColor = originalColor;
}
/// funqcua aketebs ferebs wrilinesbistvis /// funqcua aketebs ferebs wrilinesbistvis

/// funqcia ucvlis fers witlad  /// funqcia ucvlis fers witlad
void SetRedColor(Action action)
{
    Console.ForegroundColor = ConsoleColor.Red;
    action();
    Console.ResetColor();
}
/// funqcia ucvlis fers witlad  /// funqcia ucvlis fers witlad
/// funqcia ucvlis fers mwvaned /// funqcia ucvlis fers mwvaned 
void SetGreenColor(Action action)
{
    Console.ForegroundColor = ConsoleColor.Green;
    action();
    Console.ResetColor();
}
/// funqcia ucvlis fers mwvaned /// funqcia ucvlis fers mwvaned 
/// funqcia ucvlis fers yvitlad /// funqcia ucvlis fers yvitlad
void SetYellowColor(Action action)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    action();
    Console.ResetColor();
}
/// funqcia ucvlis fers yvitlad /// funqcia ucvlis fers yvitlad
/// funqcia ucvlis fers lurjad /// funqcia ucvlis fers lurjad
void SetBlueColor(Action action)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    action();
    Console.ResetColor();
}
/// funqcia ucvlis fers lurjad /// funqcia ucvlis fers lurjad
/// funqcia ucvlis fers cisfrad /// funqcia ucvlis fers cisfrad
void SetCyanColor(Action action)
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    action();
    Console.ResetColor();
}
void SetDarkCyanColor(Action action)
{
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    action();
    Console.ResetColor();
}
/// funqcia ucvlis fers cisfrad /// funqcia ucvlis fers cisfrad


// funqcia shlis yvela datas listebshi da databashi
void DeleteData(DataContext _context)
{
    List<Custumer> allCustomers = _context.Custumers.ToList();
    List<CustumerDetails> allCustumerDetails = _context.CustumerDetails.ToList();
    List<Product> allProducts = _context.Products.ToList();
    List<Category> allCategories = _context.Categories.ToList();
    List<Order> allOrders = _context.Orders.ToList();
    List<OrderItem> allOrderItems = _context.OrderItems.ToList();

    if (allCustumerDetails.Any()) _context.CustumerDetails.RemoveRange(allCustumerDetails);
    if (allCustomers.Any()) _context.Custumers.RemoveRange(allCustomers);
    if (allProducts.Any()) _context.Products.RemoveRange(allProducts);
    if (allCategories.Any()) _context.Categories.RemoveRange(allCategories);
    if (allOrders.Any()) _context.Orders.RemoveRange(allOrders);
    if (allOrderItems.Any()) _context.OrderItems.RemoveRange(allOrderItems);
    _context.SaveChanges();
    Console.Clear();
    SetRedColor(() =>
    {
        ConsoleLine();
        Console.WriteLine("All Data Has Been Deleted");
        ConsoleLine();
    });
}
// funqcia shlis yvela datas listebshi da databashi

// funqcia romelic inaxavs sheqmnil da damatebul categoriebs
void InitializeCategories(DataContext _context)
{
    var categories = new List<string> { "Phones", "Laptops", "Computers", "Cameras", "Tvs", "Monitors" };
    foreach (var categoryName in categories)
    {
        var existingCategory = _context.Categories
            .FirstOrDefault(c => c.Name.ToLower() == categoryName.ToLower());
        if (existingCategory == null)
        {
            var category = new Category
            {
                Name = categoryName.ToUpper(),
                Description = $"{categoryName} Description"
            };
            _context.Categories.Add(category);
        }
        else
        {
            if (string.IsNullOrEmpty(existingCategory.Description))
            {
                existingCategory.Description = $"{categoryName} Description";
                _context.Categories.Update(existingCategory);
            }
        }
    }
    _context.SaveChanges();
}
// funqcia romelic inaxavs sheqmnil da damatebul categoriebs

// funqcia romelic amatebs productebs aseve amowmebs categorias da productebs
void InitializeProducts(DataContext _context)
{
    var products = new List<(string Name, string Description, decimal Price, string CategoryName, int StockQuantity, bool IsAvailable)>
    {
        ("iPhone 15", "Apple iPhone", 2699.99m, "Phones", 25, true),
        ("iPhone 16 Pro Max", "Apple 256GB iPhone", 4599.00m, "Phones", 10, true),
        ("Asus TUF Gaming A15", "High-performance Laptop", 3499.00m, "Laptops", 0, false),
        ("Apple MacBook Air 13 M1", "Apple Ultrabook", 2499.00m, "Laptops", 40, true),
        ("Moes Camera P04", "Indoor smart Camera", 89.00m, "Cameras", 0, false),
        ("TP-Link C100 Home Security Camera", "Home indoor IP camera", 79.00m, "Cameras", 15, true),
        ("Samsung TV 65 NEO Qled Mini LED 8K", "QLED TV", 11299.99m, "Tvs", 10, true),
        ("UDTV 43F4", "SMART TV", 649.00m, "Tvs", 0, false),
        ("UDTV", "SMART TV", 499.00m, "Tvs", 20, true),
        ("HP Pavilion 15", "Office Notebook", 1899.00m, "Laptops", 60, true),
        ("iPhone 13", "Apple iPhone 13", 1799.99m, "Phones", 0, false),
        ("Lenovo IdeaPad Slim 3", "Office/multimedia notebook", 999.00m, "Laptops", 35, true),
        ("iPhone 14", "Apple iPhone", 1999.99m, "Phones", 45, true),
        ("LG 45", "Curved Gaming Monitor", 4999.00m, "Monitors", 2, true),
        ("Samsung S24 Ultra", "Samsung Phone", 4599.99m, "Phones", 5, true),
        ("ALTA Ryzen 9 7900X", "Desktop PC", 11499.0m, "Computers", 10, true),
    };
    foreach (var productData in products)
    {
        var existingProduct = _context.Products
            .FirstOrDefault(p => p.Name.ToLower() == productData.Name.ToLower());
        if (existingProduct == null)
        {
            var category = _context.Categories
                .FirstOrDefault(c => c.Name.ToLower() == productData.CategoryName.ToLower());
            if (category != null)
            {
                var newProduct = new Product(
                    productData.Name,
                    productData.Description,
                    productData.Price,
                    category.Id,
                    productData.StockQuantity,
                    productData.IsAvailable,
                    category,
                    null
                );
                _context.Products.Add(newProduct);
            }
        }
        else
        {
            continue;
        }
    }
    _context.SaveChanges();
}
// funqcia romelic amatebs productebs aseve amowmebs categorias da productebs

/// funqcia qmnis failebs titoeuli orderistvis /// funqcia qmnis failebs titoeuli orderistvis
void GenerateInvoice(int orderId)
{
    var order = _context.Orders
        .Include(o => o.OrderItem)
        .Include(o => o.Custumer)
        .ThenInclude(c => c.CustumerDetails)
        .FirstOrDefault(o => o.Id == orderId);
    if (order == null)
    {
        ConsoleClear();
        ConsoleLine();
        Console.WriteLine("Order was not found.");
        ConsoleLine();
    }
    string invoiceFileName = $"order_{orderId}_invoice.txt";
    string invoiceFilePath = Path.Combine(Directory.GetCurrentDirectory(), invoiceFileName);
    using (StreamWriter writer = new StreamWriter(invoiceFileName))
    {
        writer.WriteLine("==================== Invoice ====================");
        writer.WriteLine();
        writer.WriteLine($"Order ID: {order.Id}");
        writer.WriteLine();
        writer.WriteLine($"User FirstName: {order.Custumer.FirstName}, LastName: {order.Custumer.LastName}, Email: {order.Custumer.Email}");
        writer.WriteLine($"Shipping Address: {order.ShippingAddress}");
        writer.WriteLine($"Payment Status: {order.Status}");
        writer.WriteLine("");
        writer.WriteLine("Product:");
        var orderItem = order.OrderItem;
        var product = _context.Products.FirstOrDefault(p => p.Id == orderItem.ProductId);
        if (product != null)
        {
            writer.WriteLine($"ProductName: {product.Name}, TotalPrice: {orderItem.TotalPrice}, Quantity: {orderItem.Quantity}");
        }
        writer.WriteLine($"Total Amount: {order.TotalAmount}");
        writer.WriteLine();
        writer.WriteLine($"Status: {order.Status}");
    }
    /// agzavnis orderis txt fails meilze
    SMTPService smtpService = new SMTPService();
    smtpService.SendInvoiceWithAttachment(
        order.Custumer.Email,
        "Order",
        $" #{orderId}",
        invoiceFilePath
    );
}
/// funqcia qmnis failebs titoeuli orderistvis /// funqcia qmnis failebs titoeuli orderistvis

/// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID
/// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID
/// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID /// VOID

/// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP
/// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP
/// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP
bool running = true;
while (running)
{
    ColorfulWriteLine("1. User Management", ConsoleColor.Green);
    ColorfulWriteLine("2. Product Management", ConsoleColor.Blue);
    ColorfulWriteLine("3. Order Management", ConsoleColor.Cyan);
    ColorfulWriteLine("4. Analytics", ConsoleColor.Yellow);
    ColorfulWriteLine("5. File Management", ConsoleColor.Red);
    ColorfulWriteLine("6. Other Options", ConsoleColor.DarkCyan);
    ColorfulWriteLine("7. Exit", ConsoleColor.DarkRed);
    string choice = Console.ReadLine();
    /// USER /// USER /// USER /// USER /// USER /// USER /// USER /// USER /// USER /// USER
    /// USER /// USER /// USER /// USER /// USER /// USER /// USER /// USER /// USER /// USER
    if (choice == "1")
    {
        ConsoleClear();
        ColorfulWriteLine("1. New User Registration", ConsoleColor.Green);
        ColorfulWriteLine("2. Update User Data", ConsoleColor.Yellow);
        ColorfulWriteLine("3. Delete User", ConsoleColor.Red);
        ColorfulWriteLine("4. List of Users", ConsoleColor.Blue);
        ColorfulWriteLine("5. List of VIP Users", ConsoleColor.Cyan);
        ColorfulWriteLine("6. List of Crypted Data", ConsoleColor.Magenta);
        string UserManagementChoise = Console.ReadLine();
        /// ADD USER /// ADD USER /// ADD USER /// ADD USER /// ADD USER /// ADD USER
        if (UserManagementChoise == "1")
        {
            bool isRegisteredSuccessfully = false;
            while (!isRegisteredSuccessfully)
            {
                ConsoleClear();
                ColorfulWriteLine("Enter User FirstName: ", ConsoleColor.Green);
                string UserFirstName = Console.ReadLine();
                if (UserFirstName == "")
                {
                    ConsoleClear();
                    SetRedColor(() =>
                    {
                        ConsoleLine();
                        Console.WriteLine("First Name Can not be Empty!");
                        ConsoleLine();
                    });
                    ColorfulWriteLine("1. Main Menu", ConsoleColor.Blue);
                    ColorfulWriteLine("2. Restart Registration", ConsoleColor.Yellow);
                    string retryChoice = Console.ReadLine();
                    if (retryChoice == "1")
                    {
                        ConsoleClear();
                        break;
                    }
                    if (retryChoice == "2")
                    {
                        continue;
                    }
                    else
                    {
                        InvalidChoice();
                        break;
                    }
                }
                ColorfulWriteLine("Enter User LastName: ", ConsoleColor.Green);
                string UserLastName = Console.ReadLine();
                if (UserFirstName == UserLastName)
                {
                    ConsoleClear();
                    SetRedColor(() =>
                    {
                        ConsoleLine();
                        Console.WriteLine("First Name and Last Name cannot be the same!");
                        ConsoleLine();
                    });
                    ColorfulWriteLine("1. Main Menu", ConsoleColor.Blue);
                    ColorfulWriteLine("2. Restart Registration", ConsoleColor.Yellow);
                    string retryChoice = Console.ReadLine();
                    if (retryChoice == "1")
                    {
                        ConsoleClear();
                        break;
                    }
                    if (retryChoice == "2")
                    {
                        continue;
                    }
                    else
                    {
                        InvalidChoice();
                        break;
                    }
                }
                else if (UserLastName == "")
                {
                    ConsoleClear();
                    SetRedColor(() =>
                    {
                        ConsoleLine();
                        Console.WriteLine("Last Name Can not be Empty!");
                        ConsoleLine();
                    });
                    ColorfulWriteLine("1. Main Menu", ConsoleColor.Blue);
                    ColorfulWriteLine("2. Restart Registration", ConsoleColor.Yellow);
                    string retryChoice = Console.ReadLine();
                    if (retryChoice == "1")
                    {
                        ConsoleClear();
                        break;
                    }
                    if (retryChoice == "2")
                    {
                        continue;
                    }
                    else
                    {
                        InvalidChoice();
                        break;
                    }
                }
                ColorfulWriteLine("Enter User Email: ", ConsoleColor.Green);
                string UserEmail = Console.ReadLine();
                bool isValidEmail = UserEmail
                    .ToLower()
                    .Contains("@")
                    && UserEmail
                    .ToLower()
                    .Contains(".com")
                     && UserEmail
                    .ToLower()
                    .Contains("gmail");
                if (!isValidEmail)
                {
                    ConsoleClear();
                    SetRedColor(() =>
                    {
                        ConsoleLineLong();
                        Console.WriteLine($"Incorrect email Format!");
                        ConsoleLineLong();
                    });
                    ColorfulWriteLine("1. Main Menu", ConsoleColor.Blue);
                    ColorfulWriteLine("2. Restart Registration", ConsoleColor.Yellow);
                    string retryEmailChoice = Console.ReadLine();
                    if (retryEmailChoice == "1")
                    {
                        ConsoleClear();
                        break;
                    }
                    if (retryEmailChoice == "2")
                    {
                        continue;
                    }
                    else
                    {
                        InvalidChoice();
                        break;
                    }
                }
                var existingUserNameEmail = _context.Custumers
                           .FirstOrDefault(c => c.FirstName == UserFirstName &&
                           c.Email == UserEmail);
                if (existingUserNameEmail != null)
                {
                    ConsoleClear();
                    SetRedColor(() =>
                    {
                        ConsoleLineLong();
                        Console.WriteLine("A User With The Same Name and Email Already Exists. Registration Failed.");
                        ConsoleLineLong();
                    });
                    isRegisteredSuccessfully = true; /// NOT TRUE
                }
                else
                {
                    ColorfulWriteLine("Enter User Address: ", ConsoleColor.Green);
                    string UserAddress = Console.ReadLine();
                    ColorfulWriteLine("Enter User PhoneNumber: ", ConsoleColor.Green);
                    string UserPhoneNumber = Console.ReadLine();
                    if (!long.TryParse(UserPhoneNumber, out _) || UserPhoneNumber.Length > 9 || UserPhoneNumber.Length < 9)
                    {
                        if (UserPhoneNumber.Length > 9)
                        {
                            ConsoleClear();
                            SetRedColor(() =>
                            {
                                ConsoleLineLong();
                                Console.WriteLine($"Invalid phone number! {UserPhoneNumber} has more than 9 digits.");
                                ConsoleLineLong();
                            });
                        }
                        else if (UserPhoneNumber.Length < 9)
                        {
                            ConsoleClear();
                            SetRedColor(() =>
                            {
                                ConsoleLineLong();
                                Console.WriteLine($"Invalid phone number! {UserPhoneNumber} has less than 9 digits.");
                                ConsoleLineLong();
                            });
                        }
                        else
                        {
                            ConsoleClear();
                            SetRedColor(() =>
                            {
                                ConsoleLineLong();
                                Console.WriteLine($"Incorrect phone number format! {UserPhoneNumber} Is Not Valid");
                                ConsoleLineLong();
                            });
                        }
                        ColorfulWriteLine("1. Main Menu", ConsoleColor.Blue);
                        ColorfulWriteLine("2. Restart Registration", ConsoleColor.Yellow);
                        string retryPhoneNumberChoice = Console.ReadLine();
                        if (retryPhoneNumberChoice == "1")
                        {
                            ConsoleClear();
                            break;
                        }
                        if (retryPhoneNumberChoice == "2")
                        {
                            continue;
                        }
                        else
                        {
                            InvalidChoice();
                            break;
                        }
                    }
                    var existingUserEmailAndNumber = _context.Custumers
                           .FirstOrDefault(c => c.Email == UserEmail &&
                            c.CustumerDetails.PhoneNumber == UserPhoneNumber);
                    var existingUserByNameAndNumber = _context.Custumers
                        .FirstOrDefault(c => c.FirstName == UserFirstName &&
                                             c.CustumerDetails.PhoneNumber == UserPhoneNumber);
                    if (existingUserEmailAndNumber != null)
                    {
                        ConsoleClear();
                        SetRedColor(() =>
                        {
                            ConsoleLineLong();
                            Console.WriteLine("A User With The Same Email And Number Already Exists. Registration Failed.");
                            ConsoleLineLong();
                        });
                        isRegisteredSuccessfully = true; // Not true
                    }
                    else if (existingUserByNameAndNumber != null)
                    {
                        ConsoleClear();
                        SetRedColor(() =>
                        {
                            ConsoleLineLong();
                            Console.WriteLine("A User With The Same First Name And Number Already Exists. Registration Failed.");
                            ConsoleLineLong();
                        });
                        isRegisteredSuccessfully = true; // Not true
                    }

                    else
                    {
                        ColorfulWriteLine("Enter User BirthDate (MM/dd/yyyy): ", ConsoleColor.Green);
                        string birthDateInput = Console.ReadLine();
                        if (!DateTime.TryParse(birthDateInput, out DateTime UserBirthDate))
                        {
                            ConsoleClear();
                            SetRedColor(() =>
                            {
                                ConsoleLine();
                                Console.WriteLine("Incorrect birth date format!");
                                ConsoleLine();
                            });
                            ColorfulWriteLine("1. Main Menu", ConsoleColor.Blue);
                            ColorfulWriteLine("2. Restart Registration", ConsoleColor.Yellow);
                            string retrybirthDateChoice = Console.ReadLine();
                            if (retrybirthDateChoice == "1")
                            {
                                ConsoleClear();
                                break;
                            }
                            if (retrybirthDateChoice == "2")
                            {
                                continue;
                            }
                            else
                            {
                                InvalidChoice();
                                break;
                            }
                        }
                        bool isCodeValid = false;
                        while (!isCodeValid)
                        {
                            /// SMTP SERVICE /// SMTP SERVICE /// SMTP SERVICE /// SMTP SERVICE /// SMTP SERVICE
                            string verificationCode = SMTPService.GenerateVerificationCode();
                            SMTPService.EmailSender(UserEmail, verificationCode);
                            /// SMTP SERVICE /// SMTP SERVICE /// SMTP SERVICE /// SMTP SERVICE /// SMTP SERVICE
                            SetGreenColor(() =>
                            {
                                ConsoleLineLong();
                                ColorfulWriteLine($"Verification Code Sent To '{UserEmail.ToLower()}' Email", ConsoleColor.DarkCyan);
                                ConsoleLineLong();
                            });

                            ColorfulWriteLine("Enter Code:", ConsoleColor.Green);
                            string userEnteredCode = Console.ReadLine();
                            if (userEnteredCode == verificationCode)
                            {
                                DateTime RegistrationDate = DateTime.Now;
                                List<Order> orders = new List<Order>();
                                Custumer custumer = new Custumer(
                                    UserFirstName,
                                    UserLastName,
                                    UserEmail,
                                    RegistrationDate,
                                    null,
                                    orders
                                );
                                CustumerDetails custumerDetails = new CustumerDetails(
                                    UserAddress,
                                    UserPhoneNumber,
                                    UserBirthDate,
                                    10,
                                    false,
                                    custumer
                                );
                                custumer.CustumerDetails = custumerDetails;
                                Custumers.Add(custumer);
                                CustumerDetails.Add(custumerDetails);
                                _context.Custumers.Add(custumer);
                                _context.CustumerDetails.Add(custumerDetails);
                                _context.SaveChanges();
                                ConsoleClear();
                                SetGreenColor(() =>
                                {
                                    ConsoleLineLong();
                                    Console.WriteLine($"User {UserFirstName} {UserLastName} Registered Successfully.");
                                    ConsoleLineLong();
                                });
                                isCodeValid = true;
                                isRegisteredSuccessfully = true;
                            }
                            else
                            {
                                ConsoleClear();
                                SetRedColor(() =>
                                {
                                    ConsoleLine();
                                    Console.WriteLine("Verification Code Is Incorrect.");
                                    ConsoleLine();
                                });
                                ColorfulWriteLine("1. Main Menu", ConsoleColor.Blue);
                                ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                                string Codechoice = Console.ReadLine();
                                if (Codechoice == "1")
                                {
                                    ConsoleClear();
                                    isRegisteredSuccessfully = true; // NOT TRUE
                                    break;
                                }
                                else if (Codechoice == "2")
                                {
                                    ConsoleClear();
                                    continue;
                                }
                                else
                                {
                                    InvalidChoice();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        /// ADD USER /// ADD USER /// ADD USER /// ADD USER /// ADD USER /// ADD USER
        /// USER UPDATE /// USER UPDATE /// USER UPDATE /// USER UPDATE /// USER UPDATE
        else if (UserManagementChoise == "2")
        {
            ConsoleClear();
            ColorfulWriteLine("Enter User Name: ", ConsoleColor.Green);
            string UserName = Console.ReadLine();
            ColorfulWriteLine("Enter User Phone Number: ", ConsoleColor.Green);
            string UserPhoneNumber = Console.ReadLine();
            Custumers = _context.Custumers.ToList();
            Custumer foundCustomer = null;
            Custumer foundDataBaseCustomer = null;
            // Anaxlebs Listshi
            foreach (var customer in Custumers)
            {
                if (customer.CustumerDetails != null &&
                    customer.FirstName.ToLower() == UserName.ToLower() &&
                    customer.CustumerDetails.PhoneNumber == UserPhoneNumber)
                {
                    foundCustomer = customer;
                    break;
                }
            }
            // Anaxlebs Databashi da ezebs
            foreach (var DataBaseCustomer in _context.Custumers)
            {
                if (DataBaseCustomer.FirstName != null &&
                    DataBaseCustomer.FirstName.ToLower() == UserName.ToLower() &&
                    DataBaseCustomer.CustumerDetails != null &&
                    DataBaseCustomer.CustumerDetails.PhoneNumber == UserPhoneNumber)
                {
                    foundDataBaseCustomer = DataBaseCustomer;
                    break;
                }
            }
            if (foundDataBaseCustomer != null)
            {
                bool updatingUser = true;
                while (updatingUser)
                {
                    ConsoleClear();
                    ColorfulWriteLine("What would you like to update?", ConsoleColor.Blue);
                    Console.WriteLine();
                    ColorfulWriteLine("1. First Name", ConsoleColor.Red);
                    ColorfulWriteLine("2. Last Name", ConsoleColor.Magenta);
                    ColorfulWriteLine("3. Phone Number", ConsoleColor.Yellow);
                    ColorfulWriteLine("4. Email", ConsoleColor.Cyan);
                    ColorfulWriteLine("5. Address", ConsoleColor.DarkYellow);
                    string updateChoice = Console.ReadLine();
                    bool updateSuccess = false;
                    switch (updateChoice)
                    {
                        case "1":
                            while (true)
                            {
                                ConsoleClear();
                                ColorfulWriteLine("Enter New First Name: ", ConsoleColor.Yellow);
                                string UpdatedUserName = Console.ReadLine();
                                if (UpdatedUserName == foundCustomer.FirstName)
                                {
                                    ConsoleClear();
                                    SetRedColor(() =>
                                    {
                                        ConsoleLine();
                                        Console.WriteLine("The new first name cannot be the same as the old one.");
                                        ConsoleLine();
                                    });
                                    ColorfulWriteLine("1. Try Again", ConsoleColor.Yellow);
                                    ColorfulWriteLine("2. Return to Menu", ConsoleColor.Green);
                                    string retryUpdateUserNameChoice = Console.ReadLine();
                                    if (retryUpdateUserNameChoice == "1")
                                    {
                                        ConsoleClear();
                                        continue;
                                    }
                                    else if (retryUpdateUserNameChoice == "2")
                                    {
                                        ConsoleClear();
                                        updatingUser = false;
                                        break;
                                    }
                                }
                                else if (UpdatedUserName == foundCustomer.LastName)
                                {
                                    ConsoleClear();
                                    SetRedColor(() =>
                                    {
                                        ConsoleLine();
                                        Console.WriteLine("The new first name cannot be the same as the old Last Name.");
                                        ConsoleLine();
                                    });
                                    ColorfulWriteLine("1. Try Again", ConsoleColor.Yellow);
                                    ColorfulWriteLine("2. Return to Menu", ConsoleColor.Green);
                                    string retryUpdateUserNameChoice = Console.ReadLine();
                                    if (retryUpdateUserNameChoice == "1")
                                    {
                                        ConsoleClear();
                                        continue;
                                    }
                                    else if (retryUpdateUserNameChoice == "2")
                                    {
                                        ConsoleClear();
                                        updatingUser = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    foundCustomer.FirstName = UpdatedUserName;
                                    foundDataBaseCustomer.FirstName = UpdatedUserName;
                                    SMTPService.SendUpdateNotification(foundCustomer.Email, "First Name", UpdatedUserName);
                                    updateSuccess = true;
                                    break;
                                }
                            }
                            break;
                        case "2":
                            while (true)
                            {
                                ConsoleClear();
                                ColorfulWriteLine("Enter New Last Name: ", ConsoleColor.Yellow);
                                string UpdatedLastName = Console.ReadLine();
                                if (UpdatedLastName == foundCustomer.LastName)
                                {
                                    ConsoleClear();
                                    SetRedColor(() =>
                                    {
                                        ConsoleLine();
                                        Console.WriteLine("The new last name cannot be the same as the old one.");
                                        ConsoleLine();
                                    });
                                }
                                else if (UpdatedLastName == foundCustomer.FirstName)
                                {
                                    ConsoleClear();
                                    SetRedColor(() =>
                                    {
                                        ConsoleLine();
                                        Console.WriteLine("The new last name cannot be the same as the old first name.");
                                        ConsoleLine();
                                    });
                                }
                                else
                                {
                                    foundCustomer.LastName = UpdatedLastName;
                                    foundDataBaseCustomer.LastName = UpdatedLastName;
                                    SMTPService.SendUpdateNotification(foundCustomer.Email, "Last Name", UpdatedLastName);
                                    updateSuccess = true;
                                    break;
                                }
                                ColorfulWriteLine("1. Try Again", ConsoleColor.Yellow);
                                ColorfulWriteLine("2. Return to Menu", ConsoleColor.Green);
                                string retryUpdateLastNameChoice = Console.ReadLine();
                                if (retryUpdateLastNameChoice == "1")
                                {
                                    ConsoleClear();
                                    continue;
                                }
                                else if (retryUpdateLastNameChoice == "2")
                                {
                                    ConsoleClear();
                                    updatingUser = false;
                                    break;
                                }
                            }
                            break;
                        case "3":
                            while (true)
                            {
                                ConsoleClear();
                                ColorfulWriteLine("Enter New Phone Number: ", ConsoleColor.Yellow);
                                string UpdatedPhoneNumber = Console.ReadLine();
                                if (!long.TryParse(UpdatedPhoneNumber, out long phoneNumber))
                                {
                                    ConsoleClear();
                                    SetRedColor(() =>
                                    {
                                        ConsoleLine();
                                        Console.WriteLine("Invalid phone number!");
                                        ConsoleLine();
                                    });
                                }
                                else if (UpdatedPhoneNumber == foundCustomer.CustumerDetails.PhoneNumber)
                                {
                                    ConsoleClear();
                                    SetRedColor(() =>
                                    {
                                        ConsoleLine();
                                        Console.WriteLine("The new phone number cannot be the same as the old one!");
                                        ConsoleLine();
                                    });
                                }
                                else if (UpdatedPhoneNumber.Length > 9)
                                {
                                    ConsoleClear();
                                    SetRedColor(() =>
                                    {
                                        ConsoleLine();
                                        Console.WriteLine("The new phone number Must have 9 Digit");
                                        ConsoleLine();
                                    });
                                }
                                else if (UpdatedPhoneNumber.Length < 9)
                                {
                                    ConsoleClear();
                                    SetRedColor(() =>
                                    {
                                        ConsoleLine();
                                        Console.WriteLine("The new phone number Must have 9 Digit");
                                        ConsoleLine();
                                    });
                                }
                                else
                                {
                                    foundCustomer.CustumerDetails.PhoneNumber = UpdatedPhoneNumber;
                                    foundDataBaseCustomer.CustumerDetails.PhoneNumber = UpdatedPhoneNumber;
                                    ConsoleClear();
                                    updateSuccess = true;
                                    SMTPService.SendUpdateNotification(foundCustomer.Email, "Phone Number", UpdatedPhoneNumber);
                                    break;
                                }
                                ColorfulWriteLine("1. Try Again", ConsoleColor.Yellow);
                                ColorfulWriteLine("2. Return to Menu", ConsoleColor.Green);
                                string retryUpdatePhoneChoice = Console.ReadLine();
                                if (retryUpdatePhoneChoice == "1")
                                {
                                    ConsoleClear();
                                    continue;
                                }
                                else if (retryUpdatePhoneChoice == "2")
                                {
                                    ConsoleClear();
                                    updatingUser = false;
                                    break;
                                }
                            }
                            break;
                        case "4":
                            while (true)
                            {
                                ConsoleClear();
                                ColorfulWriteLine("Enter New Email: ", ConsoleColor.Yellow);
                                string UpdatedEmail = Console.ReadLine();
                                if (!UpdatedEmail.Contains("@") || !UpdatedEmail.EndsWith(".com"))   // EMAIL
                                {
                                    ConsoleClear();
                                    SetRedColor(() =>
                                    {
                                        ConsoleLineLong();
                                        Console.WriteLine("Invalid email Format! It should contain '@' and ends with '.com'.");
                                        ConsoleLineLong();
                                    });
                                }
                                else
                                {
                                    foundCustomer.Email = UpdatedEmail;
                                    foundDataBaseCustomer.Email = UpdatedEmail;
                                    updateSuccess = true;
                                    SMTPService.SendUpdateNotification(foundCustomer.Email, "Email", UpdatedEmail);
                                    break;
                                }
                                ColorfulWriteLine("1. Try Again", ConsoleColor.Yellow);
                                ColorfulWriteLine("2. Return to Menu", ConsoleColor.Green);
                                string retryUpdateEmailChoice = Console.ReadLine();
                                if (retryUpdateEmailChoice == "1")
                                {
                                    ConsoleClear();
                                    continue;
                                }
                                else if (retryUpdateEmailChoice == "2")
                                {
                                    ConsoleClear();
                                    updatingUser = false;
                                    break;
                                }
                            }
                            break;
                        case "5":
                            ConsoleClear();
                            ColorfulWriteLine("Enter New Address: ", ConsoleColor.Yellow);
                            string UpdatedAddress = Console.ReadLine();
                            foundCustomer.CustumerDetails.Address = UpdatedAddress;
                            foundDataBaseCustomer.CustumerDetails.Address = UpdatedAddress;
                            updateSuccess = true;
                            SMTPService.SendUpdateNotification(foundCustomer.Email, "Address", UpdatedAddress);

                            break;
                        default:
                            InvalidChoice();
                            break;
                    }
                    if (updateSuccess)
                    {
                        ConsoleClear();
                        SetGreenColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine("User data updated successfully.");
                            ConsoleLine();
                        });
                        _context.SaveChanges();
                        updatingUser = false;
                    }
                }
            }
            else
            {
                ConsoleClear();
                SetRedColor(() =>
                {
                    ConsoleLineLong();
                    Console.WriteLine($"User '{UserName}' With Number '{UserPhoneNumber}' was not found.");
                    ConsoleLineLong();
                });
            }
        }
        /// USER UPDATE /// USER UPDATE /// USER UPDATE /// USER UPDATE /// USER UPDATE
        /// USER DELETE /// USER DELETE /// USER DELETE /// USER DELETE /// USER DELETE
        else if (UserManagementChoise == "3")
        {
            bool continueManagingUser = true;
            while (continueManagingUser)
            {
                ConsoleClear();
                ColorfulWriteLine("Enter User Name: ", ConsoleColor.DarkRed);
                string UserName = Console.ReadLine();
                string UserPhoneNumber = "";
                while (true)
                {
                    ColorfulWriteLine("Enter User Phone Number: ", ConsoleColor.DarkRed);
                    UserPhoneNumber = Console.ReadLine();
                    if (!long.TryParse(UserPhoneNumber, out long phoneNumber))
                    {
                        ConsoleClear();
                        SetRedColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine("Invalid phone number. Please enter only numbers!");
                            ConsoleLine();
                        });
                        ColorfulWriteLine("1. Try Again", ConsoleColor.Red);
                        ColorfulWriteLine("2. Return to Menu", ConsoleColor.Green);
                        string retryPhoneChoice = Console.ReadLine();
                        if (retryPhoneChoice == "1")
                        {
                            ConsoleClear();
                            continue;
                        }
                        else if (retryPhoneChoice == "2")
                        {
                            ConsoleClear();
                            continueManagingUser = false;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (!continueManagingUser) break;
                string UserEmail = "";
                while (true)
                {
                    ColorfulWriteLine("Enter User Email: ", ConsoleColor.DarkRed);
                    UserEmail = Console.ReadLine();
                    if (!UserEmail.Contains("@") || !UserEmail.EndsWith(".com"))
                    {
                        ConsoleClear();
                        SetRedColor(() =>
                        {
                            ConsoleLineLong();
                            Console.WriteLine("Invalid email. Make Sure email contains '@' and ends with '.com'.");
                            ConsoleLineLong();
                        });
                        ColorfulWriteLine("1. Try Again", ConsoleColor.Red);
                        ColorfulWriteLine("2. Return to Menu", ConsoleColor.Green);
                        string retryUpdateEmailChoice = Console.ReadLine();
                        if (retryUpdateEmailChoice == "1")
                        {
                            ConsoleClear();
                            continue;
                        }
                        else if (retryUpdateEmailChoice == "2")
                        {
                            ConsoleClear();

                            continueManagingUser = false;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (!continueManagingUser) break;
                Custumer foundCustumer = null;
                CustumerDetails foundCustumerDetails = null;
                Custumer foundDataBaseCustumer = null;
                // Ezebs Listshi
                foreach (var custumer in Custumers)
                {
                    if (custumer.FirstName.ToLower() == UserName.ToLower() && custumer.Email.ToLower() == UserEmail.ToLower())
                    {
                        if (custumer.CustumerDetails != null && custumer.CustumerDetails.PhoneNumber == UserPhoneNumber)
                        {
                            foundCustumer = custumer;
                            foundCustumerDetails = custumer.CustumerDetails;
                            break;
                        }
                    }
                }
                // ezebs databaseshi 
                foreach (var DataBaseCustumer in _context.Custumers)
                {
                    if (DataBaseCustumer.FirstName.ToLower() == UserName.ToLower() &&
                        DataBaseCustumer.Email.ToLower() == UserEmail.ToLower() &&
                        DataBaseCustumer.CustumerDetails != null &&
                        DataBaseCustumer.CustumerDetails.PhoneNumber == UserPhoneNumber)
                    {
                        foundDataBaseCustumer = DataBaseCustumer;
                        break;
                    }
                }
                if (foundDataBaseCustumer != null)
                {
                    Custumers.Remove(foundCustumer);
                    _context.Custumers.Remove(foundDataBaseCustumer);
                    _context.SaveChanges();
                    Custumers = _context.Custumers.ToList();
                    ConsoleClear();
                    SetGreenColor(() =>
                    {
                        ConsoleLine();
                        Console.WriteLine("User deleted successfully.");
                        ConsoleLine();
                    });
                    continueManagingUser = false;
                }
                else
                {
                    ConsoleClear();
                    SetYellowColor(() =>
                    {
                        ConsoleLine();
                        Console.WriteLine($"User {UserName} with those details was not found.");
                        ConsoleLine();
                    });
                    continueManagingUser = false;
                }
            }
        }
        /// USER DELETE /// USER DELETE /// USER DELETE /// USER DELETE /// USER DELETE
        /// USER LIST /// USER LIST /// USER LIST /// USER LIST /// USER LIST /// USER LIST
        else if (UserManagementChoise == "4")
        {
            ConsoleClear();
            SetBlueColor(() =>
            {
                ConsoleLine();
                Console.WriteLine("User Data: ");
            });

            Custumers.Clear();
            var customersFromDataBase = _context.Custumers
                .Include(c => c.CustumerDetails).ToList();
            if (!customersFromDataBase.Any())
            {
                ConsoleClear();
                SetYellowColor(() =>
                {
                    ConsoleLine();
                    Console.WriteLine("No User was found!");
                    ConsoleLine();
                });
            }
            else
            {
                foreach (var custumer in customersFromDataBase)
                {
                    SetBlueColor(() =>
                    {
                        ConsoleLineLong();
                    });
                    if (custumer.CustumerDetails == null)
                    {
                        Console.WriteLine("No CustumerDetails found for this customer!");
                    }
                    else
                    {
                        SetDarkCyanColor(() =>
                        {
                            custumer.PrintUserData();
                            custumer.CustumerDetails.PrintUserDetailsData();
                        });
                        SetBlueColor(() =>
                        {
                            ConsoleLineLong();
                        });
                    }
                }
            }
        }
        /// USER LIST /// USER LIST /// USER LIST /// USER LIST /// USER LIST /// USER LIST
        /// VIP USERS /// VIP USERS /// VIP USERS /// VIP USERS /// VIP USERS /// VIP USERS
        else if (UserManagementChoise == "5")
        {
            ConsoleClear();
            var customersFromDataBase = _context.Custumers
                .Include(c => c.CustumerDetails)
                .ToList();
            if (!customersFromDataBase.Any())
            {
                ConsoleClear();
                SetYellowColor(() =>
                {
                    ConsoleLine();
                    Console.WriteLine("No User was Found");
                    ConsoleLine();
                });
            }
            else
            {
                bool anyQualifiedUsers = false;
                foreach (var custumer in customersFromDataBase)
                {
                    if (custumer.CustumerDetails != null && custumer.CustumerDetails.IsVipCostumer == true)
                    {
                        SetCyanColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine("VIP User Data:");
                            ConsoleLine();
                        });
                        SetCyanColor(() =>
                        {
                            ConsoleLineLong();
                        });
                        SetCyanColor(() =>
                        {
                            custumer.PrintUserData(); // 
                            custumer.CustumerDetails.PrintUserDetailsData();
                        });
                        SetCyanColor(() =>
                        {
                            ConsoleLineLong();
                        });
                        anyQualifiedUsers = true;
                    }
                }
                if (!anyQualifiedUsers)
                {
                    SetYellowColor(() =>
                    {
                        ConsoleLine();
                        Console.WriteLine("No VIP Users were Found");
                        ConsoleLine();
                    });
                }
            }
        }
        /// VIP USERS /// VIP USERS /// VIP USERS /// VIP USERS /// VIP USERS /// VIP USERS
        /// BCrypted Data /// BCrypted Data /// BCrypted Data /// BCrypted Data
        else if (UserManagementChoise == "6")
        {
            var customers = _context.Custumers.Include(c => c.CustumerDetails).ToList();
            if (!customers.Any())
            {
                ConsoleClear();
                SetRedColor(() =>
                {
                    ConsoleLineLong();
                    Console.WriteLine("No User was found.");
                    ConsoleLineLong();
                });
            }
            else
            {
                ConsoleClear();
                ConsoleLineLong();
                foreach (var custumer in customers)
                {
                    ColorfulWriteLine($"Name: {custumer.FirstName}", ConsoleColor.Cyan);
                    ColorfulWriteLine($"Email: {custumer.Email}", ConsoleColor.Cyan);
                    ColorfulWriteLine($"Phone Number: {custumer.CustumerDetails.PhoneNumber}", ConsoleColor.Cyan);
                    ConsoleLine();
                    string hashedEmail = BCrypt.Net.BCrypt.HashPassword(custumer.Email);
                    string hashedName = BCrypt.Net.BCrypt.HashPassword(custumer.FirstName);
                    string hashedPhoneNumber = BCrypt.Net.BCrypt.HashPassword(custumer.CustumerDetails.PhoneNumber);
                    ColorfulWriteLine($"Encrypted Data for User Name: {custumer.FirstName}", ConsoleColor.Magenta);
                    Console.WriteLine();
                    ColorfulWriteLine($"Encrypted Name: {hashedName}", ConsoleColor.Green);
                    ColorfulWriteLine($"Encrypted Email: {hashedEmail}", ConsoleColor.Green);
                    ColorfulWriteLine($"Encrypted Phone Number: {hashedPhoneNumber}", ConsoleColor.Green);
                    ConsoleLineLong();
                }
            }
        }
        /// BCrypted Data /// BCrypted Data /// BCrypted Data /// BCrypted Data
        else
        {
            InvalidChoice();
        }
    }
    /// USER /// USER /// USER /// USER /// USER /// USER /// USER /// USER /// USER /// USER
    /// USER /// USER /// USER /// USER /// USER /// USER /// USER /// USER /// USER /// USER
    /// PRODUCT /// PRODUCT /// PRODUCT /// PRODUCT /// PRODUCT /// PRODUCT /// PRODUCT
    /// PRODUCT /// PRODUCT /// PRODUCT /// PRODUCT /// PRODUCT /// PRODUCT /// PRODUCT
    else if (choice == "2")
    {
        ConsoleClear();
        ColorfulWriteLine("1. Add a New Product", ConsoleColor.Green);
        ColorfulWriteLine("2. Edit Product", ConsoleColor.Yellow);
        ColorfulWriteLine("3. Remove Product", ConsoleColor.Red);
        ColorfulWriteLine("4. List of Products", ConsoleColor.Blue);
        ColorfulWriteLine("5. Manage Categories", ConsoleColor.Cyan);
        string ProductManagementChoice = Console.ReadLine();
        /// ADD PRODUCT /// ADD PRODUCT /// ADD PRODUCT /// ADD PRODUCT /// ADD PRODUCT
        if (ProductManagementChoice == "1")
        {
            bool continueToProductMenu = true;
            bool isProductAdded = false;
            while (continueToProductMenu)
            {
                ConsoleClear();
                string ProductName = "";
                decimal ProductPrice = 0;
                int ProductQuantity = 0;
                string ProductDescription = "";
                string ProductCategoryName = "";
                while (true)
                {
                    ColorfulWriteLine("Enter Product Name: ", ConsoleColor.Green);
                    ProductName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(ProductName))
                    {
                        ConsoleClear();
                        SetRedColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine("Product Name cannot be empty. Please enter product name: ");
                            ConsoleLine();
                        });
                    }
                    else
                    {
                        var product = _context.Products
                            .FirstOrDefault(p => p.Name.ToLower() == ProductName.ToLower());
                        if (product != null)
                        {
                            ConsoleClear();
                            SetYellowColor(() =>
                            {
                                ConsoleLine();
                                Console.WriteLine($"Product '{ProductName}' already exists.");
                                ConsoleLine();
                                Console.WriteLine("Do you want to Add this Product?");
                                ConsoleLine();
                            });
                            ColorfulWriteLine("1. Yes", ConsoleColor.Green);
                            ColorfulWriteLine("2. No", ConsoleColor.Red);
                            string SameProductNameChoice = Console.ReadLine();
                            if (SameProductNameChoice == "1")
                            {
                                ConsoleClear();
                                int quantity;
                                bool isValid = false;
                                while (!isValid)
                                {
                                    ColorfulWriteLine("Enter Product Quantity: ", ConsoleColor.Green);
                                    string input = Console.ReadLine();
                                    if (int.TryParse(input, out quantity) && quantity > 0)
                                    {
                                        isValid = true;
                                        var Sameproduct = _context.Products.FirstOrDefault(p => p.Name == $"{ProductName}");
                                        if (Sameproduct != null)
                                        {
                                            ConsoleClear();
                                            int oldQuantity = product.StockQuantity;
                                            product.StockQuantity += quantity;
                                            if (oldQuantity == 0 && Sameproduct.StockQuantity > 0)
                                            {
                                                Sameproduct.IsAvailable = true;
                                                ConsoleLine();
                                                Console.WriteLine($"The product '{ProductName}' is now available.");
                                            }
                                            _context.SaveChanges();
                                            SetGreenColor(() =>
                                            {
                                                ConsoleLine();
                                                Console.WriteLine($"Product '{ProductName}'");
                                                ConsoleLine();
                                            });
                                            SetYellowColor(() =>
                                            {
                                                ConsoleLine();
                                                Console.WriteLine($"Old Quantity: {oldQuantity}");
                                                ConsoleLine();
                                            });
                                            SetYellowColor(() =>
                                            {
                                                ConsoleLine();
                                                Console.WriteLine($"Added Quantity: {quantity}");
                                                ConsoleLine();
                                            });
                                            SetGreenColor(() =>
                                            {
                                                ConsoleLine();
                                                Console.WriteLine($"New Quantity: {product.StockQuantity}");
                                                ConsoleLine();
                                            });
                                        }
                                    }
                                    else
                                    {
                                        InvalidChoice();
                                    }
                                }
                                continueToProductMenu = false;
                                break;
                            }
                            else if (SameProductNameChoice == "2")
                            {
                                ConsoleClear();
                                continueToProductMenu = false;
                                break;
                            }
                            else
                            {
                                InvalidChoice();
                                continueToProductMenu = false;
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (!continueToProductMenu) break;
                while (true)
                {
                    ColorfulWriteLine("Enter Product Price: ", ConsoleColor.Green);
                    string priceInput = Console.ReadLine();
                    if (decimal.TryParse(priceInput, out decimal parsedPrice) && parsedPrice > 0)
                    {
                        ProductPrice = parsedPrice;
                        break;
                    }
                    else
                    {
                        ConsoleClear();
                        SetRedColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine("Invalid price!");
                            ConsoleLine();
                        });
                        ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                        ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                        string retryPriceChoice = Console.ReadLine();
                        if (retryPriceChoice == "2")
                        {
                            ConsoleClear();
                            continue;
                        }
                        else if (retryPriceChoice == "1")
                        {
                            ConsoleClear();
                            isProductAdded = false;
                            continueToProductMenu = false;
                            break;
                        }
                    }
                }
                if (!continueToProductMenu) break;
                while (true)
                {
                    ColorfulWriteLine("Enter Product Quantity: ", ConsoleColor.Green);
                    string quantityInput = Console.ReadLine();
                    if (int.TryParse(quantityInput, out ProductQuantity) && ProductQuantity > 0)
                    {
                        break;
                    }
                    else
                    {
                        ConsoleClear();
                        SetRedColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine("Invalid quantity!");
                            ConsoleLine();
                        });
                        ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                        ColorfulWriteLine("2. Try Again", ConsoleColor.Green);
                        string retryQuantityChoice = Console.ReadLine();
                        if (retryQuantityChoice == "2")
                        {
                            ConsoleClear();
                            continue;
                        }
                        else if (retryQuantityChoice == "1")
                        {
                            ConsoleClear();
                            continueToProductMenu = false;
                            isProductAdded = false;
                            break;
                        }
                        else
                        {
                            InvalidChoice();
                        }
                    }
                }
                if (!continueToProductMenu) break;
                ColorfulWriteLine("Enter Product Description: ", ConsoleColor.Green);
                ProductDescription = Console.ReadLine()?.Trim();
                while (string.IsNullOrEmpty(ProductDescription))
                {
                    ConsoleClear();
                    SetRedColor(() =>
                    {
                        ConsoleLine();
                        Console.WriteLine("Product description cannot be empty");
                        ConsoleLine();
                    });
                    ColorfulWriteLine("Please enter a description: ", ConsoleColor.Green);
                    ProductDescription = Console.ReadLine()?.Trim();
                }
                int categoryId = 0;
                while (true)
                {
                    ColorfulWriteLine("Enter Product Category: ", ConsoleColor.Green);
                    ProductCategoryName = Console.ReadLine()?.Trim();
                    var category = _context.Categories
                        .FirstOrDefault(c => c.Name.ToLower() == ProductCategoryName.ToLower());
                    if (category == null && ProductCategoryName.EndsWith("s", StringComparison.OrdinalIgnoreCase))
                    {
                        string singularCategoryName = ProductCategoryName.Substring(0, ProductCategoryName.Length - 1);
                        category = _context.Categories
                            .FirstOrDefault(c => c.Name.ToLower() == singularCategoryName.ToLower());
                    }
                    if (category != null)
                    {
                        categoryId = category.Id;
                        break;
                    }
                    else
                    {
                        ConsoleClear();
                        SetYellowColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine($"Category '{ProductCategoryName}' was not found. All categories are:");
                            ConsoleLine();
                        });
                        foreach (var categories in _context.Categories)
                        {
                            ColorfulWriteLine($"- {categories.Name.ToUpper()}", ConsoleColor.Green);
                        }
                        ConsoleLine();
                        ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                        ColorfulWriteLine("2. Try again", ConsoleColor.Yellow);
                        ConsoleLine();
                        string userChoice = Console.ReadLine()?.Trim();
                        if (userChoice == "1")
                        {
                            isProductAdded = false;
                            continueToProductMenu = false;
                            break;
                        }
                        else if (userChoice == "2")
                        {
                            ConsoleClear();
                            ConsoleLine();
                            foreach (var categories in _context.Categories)
                            {
                                ColorfulWriteLine($"- {categories.Name.ToUpper()}", ConsoleColor.Green);
                            }
                            ConsoleLine();
                            continue;
                        }
                        else
                        {
                            InvalidChoice();
                        }
                    }
                }
                if (categoryId > 0)
                {
                    var newProduct = new Product
                    {
                        Name = ProductName,
                        Description = ProductDescription,
                        Price = ProductPrice,
                        StockQuantity = ProductQuantity,
                        CategoryId = categoryId,
                        IsAvailable = ProductQuantity > 0
                    };
                    _context.Products.Add(newProduct);
                    _context.SaveChanges();
                    isProductAdded = true;
                    ConsoleClear();
                    SetGreenColor(() =>
                    {
                        ConsoleLineLong();
                        Console.WriteLine($"Product '{ProductName}' added to '{ProductCategoryName.ToUpper()}' Category successfully!");
                        ConsoleLineLong();
                    });
                }
                else
                {
                    ConsoleClear();
                    SetRedColor(() =>
                    {
                        ConsoleLine();
                        Console.WriteLine("No product was added.");
                        ConsoleLine();
                    });
                }
                continueToProductMenu = false;
            }
        }
        /// ADD PRODUCT /// ADD PRODUCT /// ADD PRODUCT /// ADD PRODUCT /// ADD PRODUCT
        /// EDIT PRODUCT /// EDIT PRODUCT /// EDIT PRODUCT /// EDIT PRODUCT 
        else if (ProductManagementChoice == "2")
        {
            bool continueUpdating = true;
            while (continueUpdating)
            {
                ConsoleClear();
                ColorfulWriteLine("Enter the Product Name to Edit:", ConsoleColor.Green);
                string productNameToUpdate = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(productNameToUpdate))
                {
                    ConsoleClear();
                    SetRedColor(() =>
                    {
                        ConsoleLine();
                        Console.WriteLine("Invalid input!");
                        ConsoleLine();
                    });
                    ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                    ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                    ConsoleLine();
                    string userChoice = Console.ReadLine()?.Trim();
                    if (userChoice == "1")
                    {
                        ConsoleClear();
                        continueUpdating = false;
                    }
                    else if (userChoice == "2")
                    {
                        ConsoleClear();
                        continue;
                    }
                    else
                    {
                        ConsoleClear();
                        SetRedColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine("Invalid input!");
                            ConsoleLine();
                        });
                        continueUpdating = false;
                    }
                }
                else
                {
                    var productToUpdate = _context.Products
                        .FirstOrDefault(p => p.Name.ToLower() == productNameToUpdate.ToLower());
                    if (productToUpdate != null)
                    {
                        bool continueUpdatingProduct = true;
                        while (continueUpdatingProduct)
                        {
                            ConsoleClear();
                            ColorfulWriteLine($"Editing Product: '{productToUpdate.Name}' : ", ConsoleColor.Green);
                            Console.WriteLine();
                            ColorfulWriteLine($"1. Edit Name", ConsoleColor.DarkRed);
                            ColorfulWriteLine($"2. Edit Description", ConsoleColor.Magenta);
                            ColorfulWriteLine($"3. Edit Price", ConsoleColor.Blue);
                            ColorfulWriteLine($"4. Edit Stock Quantity", ConsoleColor.Yellow);
                            ColorfulWriteLine($"5. Edit Availability", ConsoleColor.Cyan);
                            ColorfulWriteLine($"6. Main Menu", ConsoleColor.Green);
                            string ProductUpdateChoice = Console.ReadLine()?.Trim();
                            bool isUpdated = false;
                            switch (ProductUpdateChoice)
                            {
                                case "1":
                                    /// Edit PRODUCT NAME /// Edit PRODUCT NAME
                                    while (true)
                                    {
                                        ConsoleClear();
                                        ColorfulWriteLine("Enter the new Product Name:", ConsoleColor.Green);
                                        string newProductName = Console.ReadLine()?.Trim();
                                        if (string.IsNullOrEmpty(newProductName))
                                        {
                                            isUpdated = false;
                                            ConsoleClear();
                                            SetRedColor(() =>
                                            {
                                                ConsoleLine();
                                                Console.WriteLine("New Product Name Can not be empty!");
                                                ConsoleLine();
                                            });
                                            ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                                            ColorfulWriteLine("2. Editing Menu", ConsoleColor.Blue);
                                            ColorfulWriteLine("3. Try Again", ConsoleColor.Yellow);
                                            ConsoleLine();
                                            string choise = Console.ReadLine();
                                            if (choise == "1")
                                            {
                                                ConsoleClear();
                                                continueUpdating = false;
                                                continueUpdatingProduct = false;
                                                break;
                                            }
                                            if (choise == "2")
                                            {
                                                break;
                                            }
                                            else if (choise == "3")
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        var existingProduct = _context.Products
                                            .FirstOrDefault(product => product.Name != null && product.Name.ToLower() == newProductName.ToLower());
                                        if (existingProduct != null)
                                        {
                                            isUpdated = false;
                                            ConsoleClear();
                                            SetRedColor(() =>
                                            {
                                                ConsoleLine();
                                                Console.WriteLine("New Product Name Can not be same Name!");
                                                ConsoleLine();
                                            });
                                            ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                                            ColorfulWriteLine("2. Editing Menu", ConsoleColor.Blue);
                                            ColorfulWriteLine("3. Try Again", ConsoleColor.Yellow);
                                            ConsoleLine();
                                            string choise = Console.ReadLine();
                                            if (choise == "1")
                                            {
                                                ConsoleClear();
                                                continueUpdating = false;
                                                continueUpdatingProduct = false;
                                                break;
                                            }
                                            if (choise == "2")
                                            {
                                                break;
                                            }
                                            else if (choise == "3")
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        productToUpdate.Name = newProductName;
                                        isUpdated = true;
                                        break;
                                    }
                                    break;
                                /// Edit PRODUCT NAME /// Edit PRODUCT NAME
                                case "2":
                                    /// Edit PRODUCT DESCRIPTION /// Edit PRODUCT DESCRIPTION
                                    while (true)
                                    {
                                        ConsoleClear();
                                        Console.WriteLine("Enter the new Product Description:");
                                        string newProductDescription = Console.ReadLine()?.Trim();
                                        if (string.IsNullOrEmpty(newProductDescription))
                                        {
                                            isUpdated = false;
                                            ConsoleClear();
                                            SetRedColor(() =>
                                            {
                                                ConsoleLine();
                                                Console.WriteLine("New Product Description Can not be empty!");
                                                ConsoleLine();
                                            });
                                            ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                                            ColorfulWriteLine("2. Editing Menu", ConsoleColor.Blue);
                                            ColorfulWriteLine("3. Try Again", ConsoleColor.Yellow);
                                            ConsoleLine();
                                            string choise = Console.ReadLine();
                                            if (choise == "1")
                                            {
                                                ConsoleClear();
                                                continueUpdating = false;
                                                continueUpdatingProduct = false;
                                                break;
                                            }
                                            if (choise == "2")
                                            {
                                                break;
                                            }
                                            else if (choise == "3")
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        var existingProduct = _context.Products
                                          .FirstOrDefault(product => product.Description != null && product.Description.ToLower() == newProductDescription.ToLower());
                                        if (existingProduct != null)
                                        {
                                            isUpdated = false;
                                            ConsoleClear();
                                            SetRedColor(() =>
                                            {
                                                ConsoleLine();
                                                Console.WriteLine("New Product Description Can not be same Description!");
                                                ConsoleLine();
                                            });
                                            ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                                            ColorfulWriteLine("2. Editing Menu", ConsoleColor.Blue);
                                            ColorfulWriteLine("3. Try Again", ConsoleColor.Yellow);
                                            ConsoleLine();
                                            string choise = Console.ReadLine();
                                            if (choise == "1")
                                            {
                                                ConsoleClear();
                                                continueUpdating = false;
                                                continueUpdatingProduct = false;
                                                break;
                                            }
                                            if (choise == "2")
                                            {
                                                break;
                                            }
                                            else if (choise == "3")
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        productToUpdate.Description = newProductDescription;
                                        isUpdated = true;
                                        break;
                                    }
                                    break;
                                /// Edit PRODUCT DESCRIPTION /// Edit PRODUCT DESCRIPTION
                                case "3":
                                    /// Edit PRODUCT PRICE /// Edit PRODUCT PRICE /// Edit PRODUCT PRICE
                                    while (true)
                                    {
                                        ConsoleClear();
                                        ColorfulWriteLine("Enter the new Product Price:", ConsoleColor.Green);
                                        string input = Console.ReadLine()?.Trim();
                                        if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int newProductPrice) || newProductPrice <= 0)
                                        {
                                            isUpdated = false;
                                            ConsoleClear();
                                            SetRedColor(() =>
                                            {
                                                ConsoleLineLong();
                                                Console.WriteLine("New Product Price must be a positive number and greater than 0!");
                                                ConsoleLineLong();
                                            });
                                            ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                                            ColorfulWriteLine("2. Editing Menu", ConsoleColor.Blue);
                                            ColorfulWriteLine("3. Try Again", ConsoleColor.Yellow);
                                            ConsoleLine();
                                            string PriceChoice = Console.ReadLine();
                                            if (PriceChoice == "1")
                                            {
                                                Console.Clear();
                                                continueUpdating = false;
                                                continueUpdatingProduct = false;
                                                break;
                                            }
                                            else if (PriceChoice == "2")
                                            {
                                                break;
                                            }
                                            else if (PriceChoice == "3")
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        productToUpdate.Price = newProductPrice;
                                        isUpdated = true;
                                        break;
                                    }
                                    break;
                                /// Edit PRODUCT PRICE /// Edit PRODUCT PRICE /// Edit PRODUCT PRICE
                                case "4":
                                    /// Edit PRODUCT QUANTITY /// Edit PRODUCT QUANTITY /// Edit PRODUCT QUANTITY
                                    while (true)
                                    {
                                        ConsoleClear();
                                        ColorfulWriteLine("Enter the new Product Quantity:", ConsoleColor.Green);
                                        string input = Console.ReadLine()?.Trim();
                                        if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int newProductQuantity))
                                        {
                                            isUpdated = false;
                                            ConsoleClear();
                                            SetRedColor(() =>
                                            {
                                                ConsoleLine();
                                                Console.WriteLine("Invalid Option!");
                                                ConsoleLine();
                                            });
                                            ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                                            ColorfulWriteLine("2. Editing Menu", ConsoleColor.Blue);
                                            ColorfulWriteLine("3. Try Again", ConsoleColor.Yellow);
                                            ConsoleLine();
                                            string QuantityChoice = Console.ReadLine();
                                            if (QuantityChoice == "1")
                                            {
                                                ConsoleClear();
                                                continueUpdating = false;
                                                continueUpdatingProduct = false;
                                                break;
                                            }
                                            else if (QuantityChoice == "2")
                                            {
                                                break;
                                            }
                                            else if (QuantityChoice == "3")
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        int oldQuantity = productToUpdate.StockQuantity;
                                        int newQuantity = newProductQuantity;
                                        productToUpdate.StockQuantity = newProductQuantity;
                                        DateTime updateTime = DateTime.Now;
                                        using (StreamWriter writer = new StreamWriter(logFileName, append: true))
                                        {
                                            writer.WriteLine($"QunatityChangeDate: {updateTime}, Product {productToUpdate.Name} Old Quantity: {oldQuantity} New Quantity: {newQuantity}");
                                        }
                                        if (productToUpdate.StockQuantity == 0)
                                        {
                                            productToUpdate.IsAvailable = false;
                                        }
                                        if (productToUpdate.StockQuantity > 0)
                                        {
                                            productToUpdate.IsAvailable = true;
                                        }
                                        isUpdated = true;
                                        _context.SaveChanges();
                                        break;
                                    }
                                    break;
                                /// Edit PRODUCT QUANTITY /// Edit PRODUCT QUANTITY /// Edit PRODUCT QUANTITY
                                case "5":
                                    /// Edit PRODUCT AVAILABILITY /// Edit PRODUCT AVAILABILITY /// Edit PRODUCT AVAILABILITY
                                    while (true)
                                    {
                                        ConsoleClear();
                                        ColorfulWriteLine("Enter the new Product Availability (True/False):", ConsoleColor.Green);
                                        string input = Console.ReadLine()?.Trim();
                                        if (string.IsNullOrEmpty(input) || !bool.TryParse(input, out bool newProductAvailability))
                                        {
                                            isUpdated = false;
                                            ConsoleClear();
                                            SetRedColor(() =>
                                            {
                                                ConsoleLine();
                                                Console.WriteLine("Invalid Input!");
                                                ConsoleLine();
                                            });
                                            ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                                            ColorfulWriteLine("2. Editing Menu", ConsoleColor.Blue);
                                            ColorfulWriteLine("3. Try Again", ConsoleColor.Yellow);
                                            ConsoleLine();
                                            string AvailabilityChoice = Console.ReadLine();
                                            if (AvailabilityChoice == "1")
                                            {
                                                ConsoleClear();
                                                continueUpdating = false;
                                                continueUpdatingProduct = false;
                                                break;
                                            }
                                            else if (AvailabilityChoice == "2")
                                            {
                                                break;
                                            }
                                            else if (AvailabilityChoice == "3")
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        productToUpdate.IsAvailable = newProductAvailability;
                                        isUpdated = true;
                                        break;
                                    }
                                    break;
                                /// Edit PRODUCT AVAILABILITY /// Edit PRODUCT AVAILABILITY /// Edit PRODUCT AVAILABILITY
                                case "6":
                                    ConsoleClear();
                                    continueUpdatingProduct = false;
                                    continueUpdating = false;
                                    break;
                                default:
                                    ConsoleClear();
                                    SetRedColor(() =>
                                    {
                                        ConsoleLine();
                                        Console.WriteLine("Invalid Option!");
                                        ConsoleLine();
                                    });
                                    break;
                            }
                            if (isUpdated)
                            {
                                _context.SaveChanges();
                                ConsoleClear();
                                SetGreenColor(() =>
                                {
                                    ConsoleLine();
                                    Console.WriteLine($"Product '{productToUpdate.Name}' updated successfully!");
                                    ConsoleLine();
                                });
                                continueUpdatingProduct = false;
                                continueUpdating = false;
                            }
                        }
                    }
                    else
                    {
                        ConsoleClear();
                        SetRedColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine($"Product {productNameToUpdate} was not found.");
                            ConsoleLine();
                        });
                        var allProducts = _context.Products.ToList();
                        if (allProducts.Any())
                        {
                            SetGreenColor(() =>
                            {
                                ConsoleLine();
                                Console.WriteLine("All Products:");
                                ConsoleLine();
                            });
                            foreach (var product in allProducts)
                            {
                                ColorfulWriteLine($"--- {product.Name}", ConsoleColor.Green);
                            }
                            ConsoleLine();
                        }
                        else
                        {
                            ConsoleClear();
                            SetRedColor(() =>
                            {
                                ConsoleLine();
                                Console.WriteLine("No products are available.");
                                ConsoleLine();
                            });
                        }
                        ColorfulWriteLine("1. Main menu", ConsoleColor.Green);
                        ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                        ConsoleLine();
                        string userChoice = Console.ReadLine()?.Trim();
                        if (userChoice == "1")
                        {
                            ConsoleClear();
                            continueUpdating = false;
                        }
                        else if (userChoice == "2")
                        {
                            ConsoleClear();
                            continue;
                        }
                        else
                        {
                            InvalidChoice();
                            break;
                        }
                    }
                }
            }
        }


        /// UPDATE PRODUCT /// UPDATE PRODUCT /// UPDATE PRODUCT /// UPDATE PRODUCT 
        /// REMOVE PRODUCT /// REMOVE PRODUCT /// REMOVE PRODUCT /// REMOVE PRODUCT 
        else if (ProductManagementChoice == "3")
        {
            bool continueRemoving = true;
            while (continueRemoving)
            {
                ConsoleClear();
                ColorfulWriteLine("Enter the Product Name to Remove:", ConsoleColor.Green);
                string productNameToRemove = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(productNameToRemove))
                {
                    bool invalidChoice = true;
                    while (invalidChoice)
                    {
                        ConsoleClear();
                        SetRedColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine("Invalid input!");
                            ConsoleLine();
                        });
                        ColorfulWriteLine("1. Main menu", ConsoleColor.Green);
                        ColorfulWriteLine("2. Try Again", ConsoleColor.Green);
                        ConsoleLine();
                        string userChoice = Console.ReadLine()?.Trim();
                        if (userChoice == "1")
                        {
                            ConsoleClear();
                            continueRemoving = false;
                            invalidChoice = false;
                        }
                        else if (userChoice == "2")
                        {
                            invalidChoice = false;
                        }
                        else
                        {
                            InvalidChoice();
                        }
                    }
                    continue;
                }
                var productToRemove = _context.Products.FirstOrDefault(p => p.Name.ToLower() == productNameToRemove.ToLower());
                if (productToRemove != null)
                {
                    _context.Products.Remove(productToRemove);
                    _context.SaveChanges();
                    ConsoleClear();
                    SetGreenColor(() =>
                    {
                        ConsoleLine();
                        Console.WriteLine($"Product {productNameToRemove.ToUpper()} has been removed successfully!");
                        ConsoleLine();
                    });
                    continueRemoving = false;
                }
                else
                {
                    bool invalidChoice = true;
                    while (invalidChoice)
                    {
                        ConsoleClear();
                        SetRedColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine($"Product {productNameToRemove} was not found.");
                            ConsoleLine();
                        });
                        ColorfulWriteLine("Current Products:", ConsoleColor.Green);
                        SetGreenColor(() =>
                        {
                            ConsoleLineLong();
                        });
                        var allProducts = _context.Products.ToList();
                        if (allProducts.Count > 0)
                        {
                            foreach (var product in allProducts)
                            {
                                Console.WriteLine($"--- {product.Name} (Stock: {product.StockQuantity}, Price: {product.Price:C}, Available: {product.IsAvailable})");
                            }
                            ColorfulWriteLine("Current Products:", ConsoleColor.Green);
                            SetGreenColor(() =>
                            {
                                ConsoleLineLong();
                            });
                        }
                        else
                        {
                            ConsoleClear();
                            SetYellowColor(() =>
                            {
                                ConsoleLine();
                                Console.WriteLine("No products was found in the database.");
                                ConsoleLine();
                            });
                        }
                        ColorfulWriteLine("1. Main menu", ConsoleColor.Green);
                        ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                        ConsoleLine();
                        string userChoice = Console.ReadLine()?.Trim();
                        if (userChoice == "1")
                        {
                            ConsoleClear();
                            continueRemoving = false;
                            invalidChoice = false;
                        }
                        else if (userChoice == "2")
                        {
                            ConsoleClear();
                            invalidChoice = false;
                        }
                        else
                        {
                            // restarts loop
                            ConsoleClear();
                            InvalidChoice();
                        }
                    }
                }
            }
        }
        /// REMOVE PRODUCT /// REMOVE PRODUCT /// REMOVE PRODUCT /// REMOVE PRODUCT 
        /// LIST PRODUCT /// LIST PRODUCT /// LIST PRODUCT /// LIST PRODUCT 
        else if (ProductManagementChoice == "4")
        {
            ConsoleClear();
            SetGreenColor(() =>
            {
                Console.WriteLine("All Products:");
                ConsoleLineLong();
            });
            var allProducts = _context.Products
                                      .Include(p => p.Category)
                                      .OrderBy(p => p.Name)
                                      .ToList();
            if (!allProducts.Any())
            {
                ConsoleClear();
                SetYellowColor(() =>
                {
                    ConsoleLine();
                    Console.WriteLine("No products were found!");
                    ConsoleLine();
                });
            }
            else
            {
                foreach (var product in allProducts)
                {
                    SetGreenColor(() =>
                    {
                        product.PrintProducts();
                    });
                }
                SetGreenColor(() =>
                {
                    ConsoleLineLong();
                });
            }
        }
        /// LIST PRODUCT /// LIST PRODUCT /// LIST PRODUCT /// LIST PRODUCT 
        /// MANAGE CATEGORIES /// MANAGE CATEGORIES /// MANAGE CATEGORIES
        else if (ProductManagementChoice == "5")
        {
            ConsoleClear();
            ConsoleLine();
            bool exitCategoryManagement = false;
            while (!exitCategoryManagement)
            {
                ColorfulWriteLine("1. List of Categories", ConsoleColor.Cyan);
                ColorfulWriteLine("2. Add Category", ConsoleColor.Yellow);
                ColorfulWriteLine("3. Main Menu", ConsoleColor.Green);
                ConsoleLine();
                string categoryChoice = Console.ReadLine()?.Trim();
                if (categoryChoice == "1")
                {
                    ConsoleClear();
                    var categories = _context.Categories.ToList();
                    if (categories.Count == 0)
                    {
                        SetYellowColor(() =>
                        {
                            ColorfulWriteLine("No Categories found in the database.", ConsoleColor.Yellow);
                        });
                    }
                    else
                    {
                        SetGreenColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine("Categories:");
                            ConsoleLine();
                        });
                        foreach (var category in categories)
                        {
                            ColorfulWriteLine($"--- {category.Name}", ConsoleColor.Green);
                        }
                    }
                    SetGreenColor(() =>
                    {
                        ConsoleLine();
                    });
                }
                else if (categoryChoice == "2")
                {
                    ConsoleClear();
                    ColorfulWriteLine("Enter category name:", ConsoleColor.Green);
                    string newCategoryName = Console.ReadLine()?.Trim();
                    if (string.IsNullOrEmpty(newCategoryName))
                    {
                        ConsoleClear();
                        SetRedColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine("Category name cannot be empty.");
                            ConsoleLine();
                        });
                        continue;
                    }
                    var existingCategory = _context.Categories
                        .FirstOrDefault(c => c.Name.ToLower() == newCategoryName.ToLower());
                    if (existingCategory == null)
                    {
                        ColorfulWriteLine("Enter category description:", ConsoleColor.Green);
                        string newCategoryDescription = Console.ReadLine()?.Trim();
                        if (string.IsNullOrEmpty(newCategoryDescription))
                        {
                            ConsoleClear();
                            SetRedColor(() =>
                            {
                                ConsoleLine();
                                Console.WriteLine("Category description cannot be empty.");
                                ConsoleLine();
                            });
                            continue;
                        }
                        var newCategory = new Category
                        {
                            Name = newCategoryName.ToUpper(),
                            Description = newCategoryDescription
                        };
                        _context.Categories.Add(newCategory);
                        _context.SaveChanges();
                        exitCategoryManagement = true;
                        ConsoleClear();
                        SetGreenColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine($"Category {newCategoryName.ToUpper()} added successfully");
                            ConsoleLine();
                        });
                    }
                    else
                    {
                        ConsoleClear();
                        SetYellowColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine("Category already exists!");
                            ConsoleLine();
                        });
                    }
                }
                else if (categoryChoice == "3")
                {
                    ConsoleClear();
                    exitCategoryManagement = true;
                }
                else
                {
                    InvalidChoice();
                }
            }
        }
        /// MANAGE CATEGORIES /// MANAGE CATEGORIES /// MANAGE CATEGORIES
        else
        {
            InvalidChoice();
        }
    }
    /// PRODUCT /// PRODUCT /// PRODUCT /// PRODUCT /// PRODUCT /// PRODUCT /// PRODUCT
    /// PRODUCT /// PRODUCT /// PRODUCT /// PRODUCT /// PRODUCT /// PRODUCT /// PRODUCT
    /// ORDER /// ORDER /// ORDER /// ORDER /// ORDER/// ORDER /// ORDER /// ORDER
    /// ORDER /// ORDER /// ORDER /// ORDER /// ORDER/// ORDER /// ORDER /// ORDER
    else if (choice == "3")
    {
        ConsoleClear();
        ColorfulWriteLine("1. Place a New Order", ConsoleColor.Green);
        ColorfulWriteLine("2. Update Order Status", ConsoleColor.Yellow);
        ColorfulWriteLine("3. Order History", ConsoleColor.Blue);
        ColorfulWriteLine("4. View Order Details", ConsoleColor.Magenta);
        string OrderManagementChoice = Console.ReadLine();
        /// PLACE ORDER /// PLACE ORDER /// PLACE ORDER /// PLACE ORDER
        /// PLACE ORDER /// PLACE ORDER /// PLACE ORDER /// PLACE ORDER       
        if (OrderManagementChoice == "1")
        {
            ConsoleClear();
            ColorfulWriteLine("1. Order With List of Products", ConsoleColor.Green);
            ColorfulWriteLine("2. Order With Product Name", ConsoleColor.Blue);
            string OrderPlaceChoice = Console.ReadLine();
            /// PLACE ORDER WITH LIST /// PLACE ORDER WITH LIST 
            /// PLACE ORDER WITH LIST /// PLACE ORDER WITH LIST 
            if (OrderPlaceChoice == "1")
            {
                bool ContinueOrdering = true;
                while (ContinueOrdering)
                {
                    ConsoleClear();
                    var products = _context.Products;
                    int counter = 1;
                    foreach (var product in products)
                    {
                        ColorfulWriteLine($"Product {counter}: Name: {product.Name}, Price: {product.Price}, Stock: {product.StockQuantity}, Category: {product.Category.Name}", ConsoleColor.Green);
                        counter++;
                    }
                    ConsoleLine();
                    string productNumberInput;
                    bool validInput = false;
                    while (!validInput)
                    {
                        ColorfulWriteLine("Enter Product Number: ", ConsoleColor.Green);
                        productNumberInput = Console.ReadLine();
                        if (int.TryParse(productNumberInput, out int productNumber) && productNumber > 0 && productNumber <= products.Count())
                        {
                            var selectedProduct = products.ElementAt(productNumber - 1);
                            if (selectedProduct.StockQuantity <= 0)
                            {
                                ConsoleClear();
                                SetRedColor(() =>
                                {
                                    ConsoleLine();
                                    Console.WriteLine($"Product {selectedProduct.Name} is out of stock.");
                                    ConsoleLine();
                                });
                                ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                                ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                                string stockChoice = Console.ReadLine()?.Trim();
                                if (stockChoice == "1")
                                {
                                    ConsoleClear();
                                    ContinueOrdering = false;
                                    break;
                                }
                                else if (stockChoice == "2")
                                {
                                    ConsoleClear();
                                    continue;
                                }
                                else
                                {
                                    InvalidChoice();
                                    ContinueOrdering = false;
                                }
                            }
                            if (selectedProduct.IsAvailable == false)
                            {
                                ConsoleClear();
                                SetRedColor(() =>
                                {
                                    ConsoleLine();
                                    Console.WriteLine($"Product {selectedProduct.Name} is Not Available");
                                    ConsoleLine();
                                });
                                ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                                ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                                string stockChoice = Console.ReadLine()?.Trim();
                                if (stockChoice == "1")
                                {
                                    ConsoleClear();
                                    ContinueOrdering = false;
                                    break;
                                }
                                else if (stockChoice == "2")
                                {
                                    ConsoleClear();
                                    continue;
                                }
                                else
                                {
                                    InvalidChoice();
                                    ContinueOrdering = false;
                                }
                            }
                            ConsoleClear();
                            SetGreenColor(() =>
                            {
                                ConsoleLineLong();
                                Console.WriteLine($"You selected: {selectedProduct.Name}, Price: {selectedProduct.Price}, Stock: {selectedProduct.StockQuantity}");
                                ConsoleLineLong();
                            });
                            string quantityInput;
                            bool validQuantityInput = false;
                            while (!validQuantityInput)
                            {
                                ColorfulWriteLine("Enter Quantity: ", ConsoleColor.Green);
                                quantityInput = Console.ReadLine();
                                if (string.IsNullOrEmpty(quantityInput) || !int.TryParse(quantityInput, out int quantity) || quantity <= 0)
                                {
                                    ConsoleClear();
                                    SetRedColor(() =>
                                    {
                                        ConsoleLine();
                                        Console.WriteLine("Invalid quantity, Enter a positive number.");
                                        ConsoleLine();
                                    });
                                }
                                else if (quantity > selectedProduct.StockQuantity)
                                {
                                    ConsoleClear();
                                    SetYellowColor(() =>
                                    {
                                        ConsoleLine();
                                        Console.WriteLine($"Insufficient Quantity. Available stock: {selectedProduct.StockQuantity}");
                                        ConsoleLine();
                                    });
                                    ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                                    ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                                    string retryChoice = Console.ReadLine()?.Trim();
                                    if (retryChoice == "1")
                                    {
                                        ConsoleClear();
                                        ContinueOrdering = false;
                                        validInput = true;
                                        break;
                                    }
                                    else if (retryChoice == "2")
                                    {
                                        ConsoleClear();
                                        continue;
                                    }
                                    else
                                    {
                                        InvalidChoice();
                                        ContinueOrdering = false;
                                        validInput = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    bool UserNameWhile = true;
                                    while (UserNameWhile)
                                    {
                                        validQuantityInput = true;
                                        int enteredQuantity = int.Parse(quantityInput);
                                        string customerName;
                                        ConsoleClear();
                                        ColorfulWriteLine("Enter User Name: ", ConsoleColor.Green);
                                        customerName = Console.ReadLine()?.Trim();
                                        var customer = _context.Custumers
                                            .FirstOrDefault(c => c.FirstName.ToLower() == customerName.ToLower());
                                        if (customer == null)
                                        {
                                            ConsoleClear();
                                            SetRedColor(() =>
                                            {
                                                ConsoleLine();
                                                Console.WriteLine("User was not found.");
                                                ConsoleLine();
                                            });
                                            ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                                            ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                                            ConsoleLine();
                                            string OrderProductNameChoiceUserName = Console.ReadLine()?.Trim();
                                            if (OrderProductNameChoiceUserName == "1")
                                            {
                                                ConsoleClear();
                                                ContinueOrdering = false;
                                                validInput = true;
                                                break;
                                            }
                                            if (OrderProductNameChoiceUserName == "2")
                                            {
                                            }
                                            else
                                            {
                                                ConsoleClear();
                                                InvalidChoice();
                                                ContinueOrdering = false;
                                                UserNameWhile = false;
                                                validInput = true;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            bool AmountWhile = true;
                                            while (AmountWhile)
                                            {
                                                ConsoleClear();
                                                ColorfulWriteLine("Enter Amount:", ConsoleColor.Green);
                                                string inputAmount = Console.ReadLine()?.Trim();
                                                if (decimal.TryParse(inputAmount, out decimal amount) && amount >= selectedProduct.Price * enteredQuantity)
                                                {
                                                    var order = new Order
                                                    {
                                                        CustumerId = customer.Id,
                                                        OrderDate = DateTime.Now,
                                                        TotalAmount = amount,
                                                        Status = ORDER_STATUS.Confirmed.ToString(),
                                                        ShippingAddress = customer.CustumerDetails.Address,
                                                        PaymentStatus = ORDER_PAYMENT_STATUS.Completed.ToString(),
                                                        OrderItem = new OrderItem
                                                        {
                                                            ProductId = selectedProduct.Id,
                                                            Quantity = enteredQuantity,
                                                            UnitPrice = selectedProduct.Price,
                                                            TotalPrice = selectedProduct.Price * enteredQuantity,
                                                        }
                                                    };
                                                    selectedProduct.StockQuantity -= enteredQuantity;
                                                    if (selectedProduct.StockQuantity == 0)
                                                    {
                                                        selectedProduct.IsAvailable = false;
                                                    }
                                                    var customerPoints = _context.Custumers
                                                        .Include(c => c.CustumerDetails)
                                                        .FirstOrDefault(c => c.Id == order.CustumerId);
                                                    if (customer != null && customer.CustumerDetails != null)
                                                    {
                                                        customer.CustumerDetails.LoyaltyPoits += 20;
                                                        if (customer.CustumerDetails.LoyaltyPoits >= 50)
                                                        {
                                                            customer.CustumerDetails.IsVipCostumer = true;
                                                        }
                                                    }
                                                    _context.Orders.Add(order);
                                                    _context.SaveChanges();
                                                    ConsoleClear();
                                                    SetGreenColor(() =>
                                                    {
                                                        ConsoleLine();
                                                        Console.WriteLine("Order Placed successfully");
                                                        ConsoleLine();
                                                    });
                                                    var orderDetails = _context.Orders
                                                        .Where(o => o.Id == order.Id)
                                                        .Include(o => o.Custumer)
                                                        .Include(o => o.OrderItem)
                                                        .FirstOrDefault();
                                                    if (orderDetails != null)
                                                    {
                                                        string UserName = orderDetails.Custumer?.FirstName + " " + orderDetails.Custumer?.LastName;
                                                        var productName = _context.Products
                                                            .FirstOrDefault(p => p.Id == orderDetails.OrderItem.ProductId)?.Name ?? "Unknown Product";
                                                        DateTime orderPlacedTime = DateTime.Now;

                                                        using (StreamWriter writer = new StreamWriter(logFileName, append: true))
                                                        {
                                                            writer.WriteLine($"OrderDate: {orderPlacedTime}, Order ID {orderDetails.Id} placed by {UserName}. Product: {productName}");
                                                        }
                                                    }
                                                    GenerateInvoice(order.Id);
                                                    ContinueOrdering = false;
                                                    UserNameWhile = false;
                                                    AmountWhile = false;
                                                    validInput = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    ConsoleClear();
                                                    SetRedColor(() =>
                                                    {
                                                        ConsoleLine();
                                                        Console.WriteLine("Insufficient amount.");
                                                        ConsoleLine();
                                                    });
                                                    ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                                                    ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                                                    ConsoleLine();
                                                    string AmountChoice = Console.ReadLine();
                                                    if (AmountChoice == "1")
                                                    {
                                                        ConsoleClear();
                                                        ContinueOrdering = false;
                                                        validInput = true;
                                                        AmountWhile = false;
                                                        UserNameWhile = false;
                                                    }
                                                    else if (AmountChoice == "2")
                                                    {
                                                    }
                                                    else
                                                    {
                                                        InvalidChoice();
                                                        ContinueOrdering = false;
                                                        validInput = true;
                                                        AmountWhile = false;
                                                        UserNameWhile = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            ConsoleClear();
                            SetRedColor(() =>
                            {
                                ConsoleLine();
                                Console.WriteLine("Invalid Product Number!");
                                ConsoleLine();
                            });
                            ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                            ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                            string ProductChoice = Console.ReadLine()?.Trim();
                            if (ProductChoice == "1")
                            {
                                ConsoleClear();
                                ContinueOrdering = false;
                                break;
                            }
                            if (ProductChoice == "2")
                            {
                                ConsoleClear();
                            }
                            else
                            {
                                ConsoleClear();
                                InvalidChoice();
                                ContinueOrdering = false;
                                break;
                            }
                        }
                    }
                }
            }
            /// PLACE ORDER WITH LIST /// PLACE ORDER WITH LIST 
            /// PLACE ORDER WITH LIST /// PLACE ORDER WITH LIST 
            /// PLACE ORDER WITH NAME /// PLACE ORDER WITH NAME 
            else if (OrderPlaceChoice == "2")
            {
                bool ContinueOrdering = true;
                while (ContinueOrdering)
                {
                    ConsoleClear();
                    ColorfulWriteLine("Enter Product Name:", ConsoleColor.Green);
                    string OrderProductName = Console.ReadLine();
                    if (string.IsNullOrEmpty(OrderProductName))
                    {
                        ConsoleClear();
                        SetRedColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine("Product name cannot be empty.");
                            ConsoleLine();
                        });
                        ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                        ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                        ConsoleLine();
                        string OrderProductNameChoice = Console.ReadLine()?.Trim();
                        if (OrderProductNameChoice == "1")
                        {
                            ConsoleClear();
                            ContinueOrdering = false;
                            continue;
                        }
                        else if (OrderProductNameChoice == "2")
                        {
                            continue;
                        }
                        else
                        {
                            InvalidChoice();
                            continue;
                        }
                    }
                    var product = _context.Products.FirstOrDefault(p => p.Name.ToLower() == OrderProductName.ToLower());
                    if (product == null)
                    {
                        ConsoleClear();
                        SetRedColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine($"Product {OrderProductName} was Not Found");
                            ConsoleLine();
                        });
                        ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                        ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                        ConsoleLine();
                        string OrderProductNameChoiceNull = Console.ReadLine()?.Trim();
                        if (OrderProductNameChoiceNull == "1")
                        {
                            ConsoleClear();
                            ContinueOrdering = false;
                            continue;
                        }
                        else if (OrderProductNameChoiceNull == "2")
                        {
                            continue;
                        }
                        else
                        {
                            InvalidChoice();
                            continue;
                        }
                    }
                    else if (!product.IsAvailable)
                    {
                        ConsoleClear();
                        SetRedColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine($"Product Is Not Available");
                            ConsoleLine();
                        });
                        ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                        ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                        ConsoleLine();
                        string OrderProductNameChoiceAvailable = Console.ReadLine()?.Trim();
                        if (OrderProductNameChoiceAvailable == "1")
                        {
                            ConsoleClear();
                            ContinueOrdering = false;
                            continue;
                        }
                        else if (OrderProductNameChoiceAvailable == "2")
                        {
                            continue;
                        }
                        else
                        {
                            InvalidChoice();
                            continue;
                        }
                    }
                    bool continueOrderingQuantity = true;
                    while (continueOrderingQuantity)
                    {
                        ConsoleClear();
                        ColorfulWriteLine("Enter Product Quantity:", ConsoleColor.Green);
                        string OrderProductQuantity = Console.ReadLine();
                        if (
                            string.IsNullOrEmpty(OrderProductQuantity)
                            || !int.TryParse(OrderProductQuantity, out int quantity)
                            || quantity <= 0
                            )
                        {
                            ConsoleClear();
                            SetRedColor(() =>
                            {
                                ConsoleLine();
                                Console.WriteLine($"Quantity must be Positive Number");
                                ConsoleLine();
                            });
                            ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                            ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                            ConsoleLine();
                            string OrderProductQuantityChoice = Console.ReadLine()?.Trim();
                            ConsoleLine();
                            if (OrderProductQuantityChoice == "1")
                            {
                                ConsoleClear();
                                ContinueOrdering = false;
                                continueOrderingQuantity = false;
                                continue;
                            }
                            else if (OrderProductQuantityChoice == "2")
                            {
                                continue;
                            }
                            else
                            {
                                InvalidChoice();
                            }
                        }
                        else if (quantity > product.StockQuantity)
                        {
                            ConsoleClear();
                            SetRedColor(() =>
                            {
                                ConsoleLine();
                                Console.WriteLine($"Insufficient Quantity. Available stockQuantity: {product.StockQuantity}");
                                ConsoleLine();
                            });
                            ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                            ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                            ConsoleLine();
                            string stockChoice = Console.ReadLine()?.Trim();
                            if (stockChoice == "1")
                            {
                                ConsoleClear();
                                ContinueOrdering = false;
                                continueOrderingQuantity = false;
                                break;
                            }
                            else if (stockChoice == "2")
                            {
                                continue;
                            }
                            else
                            {
                                InvalidChoice();
                                continue;
                            }
                        }

                        else
                        {
                            bool backToMainMenu = true;
                            while (backToMainMenu)
                            {
                                ConsoleClear();
                                ColorfulWriteLine("Enter User Name:", ConsoleColor.Green);
                                string inputUserName = Console.ReadLine()?.Trim();
                                var custumer = _context.Custumers.FirstOrDefault(p => p.FirstName.ToLower() == inputUserName.ToLower());
                                if (custumer == null)
                                {
                                    ConsoleClear();
                                    SetRedColor(() =>
                                    {
                                        ConsoleLine();
                                        Console.WriteLine("User was not found.");
                                        ConsoleLine();
                                    });
                                    ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                                    ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                                    ConsoleLine();
                                    string OrderProductNameChoiceUserName = Console.ReadLine()?.Trim();
                                    if (OrderProductNameChoiceUserName == "1")
                                    {
                                        ConsoleClear();
                                        ContinueOrdering = false;
                                        continueOrderingQuantity = false;
                                        backToMainMenu = false;
                                        break;
                                    }
                                    else if (OrderProductNameChoiceUserName == "2")
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        InvalidChoice();
                                        continue;
                                    }
                                }
                                bool orderPlaced = false;
                                while (!orderPlaced)
                                {
                                    ConsoleClear();
                                    ColorfulWriteLine("Enter Amount:", ConsoleColor.Green);
                                    string inputAmount = Console.ReadLine()?.Trim();
                                    if (decimal.TryParse(inputAmount, out decimal amount) && amount >= product.Price)
                                    {
                                        decimal totalPrice = product.Price * quantity;
                                        if (amount < totalPrice)
                                        {
                                            ConsoleClear();
                                            SetRedColor(() =>
                                            {
                                                ConsoleLine();
                                                Console.WriteLine("Insufficient Amount.");
                                                ConsoleLine();
                                            });
                                            ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                                            ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                                            ConsoleLine();
                                            string OrderAmountChoice = Console.ReadLine()?.Trim();
                                            if (OrderAmountChoice == "1")
                                            {
                                                ConsoleClear();
                                                ContinueOrdering = false;
                                                continueOrderingQuantity = false;
                                                backToMainMenu = false;
                                                break;
                                            }
                                            else if (OrderAmountChoice == "2")
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                InvalidChoice();
                                                continue;
                                            }
                                        }
                                        var existingOrderItem = _context.OrderItems
                                                              .Include(oi => oi.Order)
                                                              .FirstOrDefault(oi => oi.ProductId == product.Id && oi.Order.CustumerId == custumer.Id);
                                        var newOrder = new Order
                                        {
                                            CustumerId = custumer.Id,
                                            OrderDate = DateTime.Now,
                                            TotalAmount = amount,
                                            Status = ORDER_STATUS.Confirmed.ToString(),
                                            ShippingAddress = custumer.CustumerDetails.Address,
                                            PaymentStatus = ORDER_PAYMENT_STATUS.Completed.ToString(),
                                            OrderItem = new OrderItem
                                            {
                                                ProductId = product.Id,
                                                Quantity = quantity,
                                                UnitPrice = product.Price,
                                                TotalPrice = product.Price * quantity,
                                            }
                                        };
                                        product.StockQuantity -= quantity;
                                        if (product.StockQuantity == 0)
                                        {
                                            product.IsAvailable = false;
                                        }
                                        var customer = _context.Custumers
                                            .Include(c => c.CustumerDetails)
                                            .FirstOrDefault(c => c.Id == custumer.Id);
                                        if (customer?.CustumerDetails != null)
                                        {
                                            customer.CustumerDetails.LoyaltyPoits += 20;
                                            if (customer.CustumerDetails.LoyaltyPoits >= 50)
                                            {
                                                customer.CustumerDetails.IsVipCostumer = true;
                                            }
                                        }
                                        _context.Orders.Add(newOrder);
                                        _context.SaveChanges();
                                        ConsoleClear();
                                        SetGreenColor(() =>
                                        {
                                            ConsoleLine();
                                            Console.WriteLine("Order placed successfully");
                                            ConsoleLine();
                                        });
                                        var orderDetails = _context.Orders
                                           .Where(o => o.Id == newOrder.Id)
                                           .Include(o => o.Custumer)
                                           .Include(o => o.OrderItem)
                                           .FirstOrDefault();
                                        if (orderDetails != null)
                                        {
                                            string UserName = orderDetails.Custumer?.FirstName + " " + orderDetails.Custumer?.LastName;
                                            var productName = _context.Products
                                                .FirstOrDefault(p => p.Id == orderDetails.OrderItem.ProductId)?.Name ?? "Unknown Product";
                                            DateTime orderPlacedTime = DateTime.Now;
                                            using (StreamWriter writer = new StreamWriter(logFileName, append: true))
                                            {
                                                writer.WriteLine($"OrderDate: {orderPlacedTime}, Order ID {orderDetails.Id} placed by {UserName}. Product: {productName}");
                                            }
                                        }
                                        GenerateInvoice(newOrder.Id);
                                        ContinueOrdering = false;
                                        backToMainMenu = false;
                                        continueOrderingQuantity = false;
                                        break;
                                    }
                                    else
                                    {
                                        ConsoleClear();
                                        SetRedColor(() =>
                                        {
                                            ConsoleLine();
                                            Console.WriteLine("Insufficient Amount.");
                                            ConsoleLine();
                                        });
                                        ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                                        ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                                        ConsoleLine();
                                        string OrderAmountChoice = Console.ReadLine()?.Trim();
                                        if (OrderAmountChoice == "1")
                                        {
                                            ConsoleClear();
                                            ContinueOrdering = false;
                                            continueOrderingQuantity = false;
                                            backToMainMenu = false;
                                            break;
                                        }
                                        else if (OrderAmountChoice == "2")
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            InvalidChoice();
                                            continue;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            /// PLACE ORDER WITH NAME /// PLACE ORDER WITH NAME 
            else
            {
                InvalidChoice();
            }
        }
        /// PLACE ORDER /// PLACE ORDER /// PLACE ORDER /// PLACE ORDER
        /// UPDATE ORDER STATUS /// UPDATE ORDER STATUS /// UPDATE ORDER STATUS
        else if (OrderManagementChoice == "2")
        {
            ConsoleClear();
            var customerIds = _context.Custumers
                .Select(c => c.Id)
                .ToList();
            bool hasOrders = false;
            int globalOrderCounter = 1;
            var allOrders = new List<Order>();
            foreach (var customerId in customerIds)
            {
                var customer = _context.Custumers
                    .FirstOrDefault(c => c.Id == customerId);
                var orders = _context.Orders
                    .Include(o => o.Custumer)
                    .Where(o => o.CustumerId == customerId)
                    .ToList();
                if (orders.Any())
                {
                    SetGreenColor(() =>
                    {
                        ConsoleLineLong();
                    });
                    SetBlueColor(() =>
                    {
                        Console.WriteLine($"Orders for User {customer?.FirstName}:");
                    });
                    foreach (var order in orders)
                    {
                        SetGreenColor(() =>
                        {
                            ConsoleLine();
                            Console.WriteLine($"Order {globalOrderCounter}:");
                        });
                        var orderItems = _context.OrderItems
                            .Where(oi => oi.OrderId == order.Id)
                            .ToList();

                        foreach (var orderItem in orderItems)
                        {
                            var product = _context.Products
                                .FirstOrDefault(p => p.Id == orderItem.ProductId);
                            if (product != null)
                                ColorfulWriteLine($"Product: {product.Name}", ConsoleColor.Cyan);
                        }
                        SetGreenColor(() =>
                        {
                            ConsoleLine();
                        });
                        SetYellowColor(() =>
                        {
                            order.PrintOrderFirstLine();
                        });
                        allOrders.Add(order);
                        globalOrderCounter++;
                    }
                    hasOrders = true;
                }
            }
            if (!hasOrders)
            {
                ConsoleClear();
                SetYellowColor(() =>
                {
                    ConsoleLine();
                    Console.WriteLine("No orders found for any Users.");
                    ConsoleLine();
                });
            }
            else
            {
                bool validOrderNumber = false;
                while (!validOrderNumber)
                {
                    SetGreenColor(() =>
                    {
                        ConsoleLine();
                    });
                    ColorfulWriteLine("Enter the Order Number: ", ConsoleColor.Green);
                    if (int.TryParse(Console.ReadLine(), out int orderNumber))
                    {
                        var orderToUpdate = allOrders.ElementAtOrDefault(orderNumber - 1);
                        if (orderToUpdate != null)
                        {
                            bool validStatus = false;
                            while (!validStatus)
                            {
                                ConsoleClear();
                                ColorfulWriteLine("Enter new Order status (Pending, Confirmed, Shipped, Delivered):", ConsoleColor.Green);
                                string orderStatus = Console.ReadLine()?.ToLower();
                                DateTime statusUpdateTime = DateTime.Now;
                                if (orderStatus == "pending" || orderStatus == "confirmed" || orderStatus == "shipped" || orderStatus == "delivered")
                                {
                                    orderToUpdate.Status = orderStatus.ToUpper();
                                    validStatus = true;
                                    using (StreamWriter writer = new StreamWriter(logFileName, append: true))
                                    {
                                        writer.WriteLine($"StatusUpdateDate: {statusUpdateTime},  - Order ID {orderToUpdate.Id} status updated to '{orderToUpdate.Status}'.");
                                    }
                                }
                                else
                                {
                                    ConsoleClear();
                                    SetRedColor(() =>
                                    {
                                        ConsoleLine();
                                        Console.WriteLine("Invalid status entered!");
                                        ConsoleLine();
                                    });
                                    ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                                    ColorfulWriteLine("2. Try Again", ConsoleColor.Yellow);
                                    string StatusBackChoice = Console.ReadLine();
                                    if (StatusBackChoice == "1")
                                    {
                                        ConsoleClear();
                                        validOrderNumber = true;
                                        validStatus = true;
                                    }
                                    else if (StatusBackChoice == "2")
                                    {
                                        validStatus = false;
                                    }
                                    else
                                    {
                                        validStatus = false;
                                    }
                                }
                            }
                            if (validStatus && !validOrderNumber)
                            {
                                validOrderNumber = true;
                                ConsoleClear();
                                _context.SaveChanges();
                                SetGreenColor(() =>
                                {
                                    ConsoleLine();
                                    Console.WriteLine($"Order {orderToUpdate.Id} new Status is {orderToUpdate.Status}.");
                                    ConsoleLine();
                                });
                            }
                        }
                        else
                        {
                            ConsoleClear();
                            SetRedColor(() =>
                            {
                                ConsoleLineLong();
                                Console.WriteLine("Invalid order number");
                            });
                            foreach (var customerId in customerIds)
                            {
                                var customer = _context.Custumers
                                    .FirstOrDefault(c => c.Id == customerId);
                                var orders = _context.Orders
                                    .Include(o => o.Custumer)
                                    .Where(o => o.CustumerId == customerId)
                                    .ToList();
                                if (orders.Any())
                                {
                                    SetRedColor(() =>
                                    {
                                        ConsoleLineLong();
                                    });
                                    ColorfulWriteLine($"Orders for User {customer?.FirstName}:", ConsoleColor.Blue);
                                    foreach (var order in orders)
                                    {
                                        SetGreenColor(() =>
                                        {
                                            ConsoleLine();
                                            Console.WriteLine($"Order {globalOrderCounter}:");
                                        });
                                        var orderItems = _context.OrderItems
                                            .Where(oi => oi.OrderId == order.Id)
                                            .ToList();
                                        foreach (var orderItem in orderItems)
                                        {
                                            var product = _context.Products
                                                .FirstOrDefault(p => p.Id == orderItem.ProductId);
                                            if (product != null)
                                                ColorfulWriteLine($"Product: {product.Name}", ConsoleColor.Cyan);
                                        }
                                        SetGreenColor(() =>
                                        {
                                            ConsoleLine();
                                        });
                                        SetYellowColor(() =>
                                        {
                                            order.PrintOrderFirstLine();
                                        });
                                        allOrders.Add(order);
                                        globalOrderCounter++;
                                    }
                                    hasOrders = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        ConsoleClear();
                        SetRedColor(() =>
                        {
                            ConsoleLineLong();
                            Console.WriteLine("Invalid Input");
                        });
                        foreach (var customerId in customerIds)
                        {
                            var customer = _context.Custumers
                                .FirstOrDefault(c => c.Id == customerId);
                            var orders = _context.Orders
                                .Include(o => o.Custumer)
                                .Where(o => o.CustumerId == customerId)
                                .ToList();
                            if (orders.Any())
                            {
                                SetRedColor(() =>
                                {
                                    ConsoleLineLong();
                                });
                                ColorfulWriteLine($"Orders for User {customer?.FirstName}:", ConsoleColor.Blue);
                                foreach (var order in orders)
                                {
                                    SetGreenColor(() =>
                                    {
                                        ConsoleLine();
                                        Console.WriteLine($"Order {globalOrderCounter}:");
                                    });
                                    var orderItems = _context.OrderItems
                                        .Where(oi => oi.OrderId == order.Id)
                                        .ToList();
                                    foreach (var orderItem in orderItems)
                                    {
                                        var product = _context.Products
                                            .FirstOrDefault(p => p.Id == orderItem.ProductId);
                                        if (product != null)
                                            ColorfulWriteLine($"Product: {product.Name}", ConsoleColor.Cyan);
                                    }
                                    SetGreenColor(() =>
                                    {
                                        ConsoleLine();
                                    });
                                    SetYellowColor(() =>
                                    {
                                        order.PrintOrderFirstLine();
                                    });
                                    allOrders.Add(order);
                                    globalOrderCounter++;
                                }
                                hasOrders = true;
                            }
                        }
                    }
                }
            }
        }
        /// UPDATE ORDER STATUS /// UPDATE ORDER STATUS /// UPDATE ORDER STATUS
        /// ORDER HISTORY /// ORDER HISTORY /// ORDER HISTORY /// ORDER HISTORY
        else if (OrderManagementChoice == "3")
        {
            ConsoleClear();
            var customerIds = _context.Custumers
                .Select(c => c.Id)
                .ToList();
            if (customerIds.Count == 0)
            {
                SetYellowColor(() =>
                {
                    ConsoleLine();
                    Console.WriteLine("No User was found in database.");
                    ConsoleLine();
                });
            }
            foreach (var customerId in customerIds)
            {
                var customer = _context.Custumers
                    .FirstOrDefault(c => c.Id == customerId);
                var orders = _context.Orders
                    .Include(o => o.Custumer)
                    .Where(o => o.CustumerId == customerId)
                    .ToList();
                if (orders.Any())
                {
                    SetGreenColor(() =>
                    {
                        ConsoleLineLong();
                        Console.WriteLine($"Orders for User {customer?.FirstName}:");
                    });
                    int orderCounter = 1;
                    foreach (var order in orders)
                    {
                        SetGreenColor(() =>
                        {
                            ConsoleLineLong();
                        });
                        ColorfulWriteLine($"Order {orderCounter}:", ConsoleColor.Blue);
                        var orderItems = _context.OrderItems
                            .Where(oi => oi.OrderId == order.Id)
                            .ToList();
                        foreach (var orderItem in orderItems)
                        {
                            var product = _context.Products
                                .FirstOrDefault(p => p.Id == orderItem.ProductId);
                            if (product != null)
                                ColorfulWriteLine($"Product: {product.Name}", ConsoleColor.Cyan);
                        }
                        SetYellowColor(() =>
                        {
                            Line();
                        });
                        SetGreenColor(() =>
                        {
                            order.PrintOrder();
                        });
                        orderCounter++;
                    }
                    SetGreenColor(() =>
                    {
                        ConsoleLine();
                    });
                }
                else
                {
                    SetYellowColor(() =>
                    {
                        ConsoleLine();
                        Console.WriteLine($"User {customer?.FirstName} has not placed any orders yet.");
                        ConsoleLine();
                    });
                }
            }
        }
        /// ORDER HISTORY /// ORDER HISTORY /// ORDER HISTORY /// ORDER HISTORY
        /// ORDER DETAILS /// ORDER DETAILS /// ORDER DETAILS /// ORDER DETAILS
        else if (OrderManagementChoice == "4")
        {
            ConsoleClear();
            var customerIds = _context.Custumers
                .Select(c => c.Id)
                .ToList();
            if (customerIds.Count == 0)
            {
                SetYellowColor(() =>
                {
                    ConsoleLine();
                    Console.WriteLine("No User was found in database.");
                    ConsoleLine();
                });
            }
            foreach (var customerId in customerIds)
            {
                var customer = _context.Custumers
                    .FirstOrDefault(c => c.Id == customerId);
                var orders = _context.Orders
                    .Where(o => o.CustumerId == customerId)
                    .ToList();
                if (orders.Any())
                {
                    SetGreenColor(() =>
                    {
                        ConsoleLineLong();
                        Console.WriteLine($"Orders for User {customer?.FirstName}:");
                    });
                    int orderCounter = 1;
                    foreach (var order in orders)
                    {
                        SetGreenColor(() =>
                        {
                            ConsoleLineLong();
                        });
                        ColorfulWriteLine($"Order {orderCounter}:", ConsoleColor.Magenta);
                        var orderItems = _context.OrderItems
                            .Where(oi => oi.OrderId == order.Id)
                            .ToList();
                        foreach (var orderItem in orderItems)
                        {
                            var product = _context.Products
                                .FirstOrDefault(p => p.Id == orderItem.ProductId);
                            if (product != null)
                            {
                                ColorfulWriteLine($"Product: {product.Name}", ConsoleColor.Cyan);
                                ColorfulWriteLine($"Quantity: {orderItem.Quantity}, Unit Price: {orderItem.UnitPrice}, Total Price: {orderItem.TotalPrice}", ConsoleColor.Blue);
                            }
                        }
                        orderCounter++;
                    }
                    SetGreenColor(() =>
                    {
                        ConsoleLine();
                    });
                }
                else
                {
                    SetYellowColor(() =>
                    {
                        ConsoleLine();
                        Console.WriteLine($"User {customer?.FirstName} has not placed any orders yet.");
                        ConsoleLine();
                    });
                }
            }
        }
        /// ORDER DETAILS /// ORDER DETAILS /// ORDER DETAILS /// ORDER DETAILS
        else
        {
            InvalidChoice();
        }
    }
    /// ORDER /// ORDER /// ORDER /// ORDER /// ORDER/// ORDER /// ORDER /// ORDER
    /// ORDER /// ORDER /// ORDER /// ORDER /// ORDER/// ORDER /// ORDER /// ORDER
    /// ANALYTICS /// ANALYTICS /// ANALYTICS/// ANALYTICS /// ANALYTICS /// ANALYTICS
    /// ANALYTICS /// ANALYTICS /// ANALYTICS/// ANALYTICS /// ANALYTICS /// ANALYTICS   
    else if (choice == "4")
    {
        Analytics.InitializeAnalytics();
    }
    /// ANALYTICS /// ANALYTICS /// ANALYTICS/// ANALYTICS /// ANALYTICS /// ANALYTICS   
    /// ANALYTICS /// ANALYTICS /// ANALYTICS/// ANALYTICS /// ANALYTICS /// ANALYTICS   
    /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE
    /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE
    else if (choice == "5")
    {
        ConsoleClear();
        ColorfulWriteLine("1. Generation Of Invoice", ConsoleColor.Green);
        ColorfulWriteLine("2. View System Log", ConsoleColor.Cyan);
        string FileManagementChoice = Console.ReadLine();
        /// VIEW INVOICE /// VIEW INVOICE /// VIEW INVOICE /// VIEW INVOICE
        /// VIEW INVOICE /// VIEW INVOICE /// VIEW INVOICE /// VIEW INVOICE
        if (FileManagementChoice == "1")
        {
            ConsoleClear();
            SetGreenColor(() =>
            {
                ConsoleLine();
                Console.WriteLine("Existing Orders:");
                ConsoleLine();
            });
            var orders = _context.Orders
                .Include(o => o.OrderItem)
                .Include(o => o.Custumer)
                .ThenInclude(c => c.CustumerDetails)
                .ToList();
            if (orders.Count == 0)
            {
                ConsoleClear();
                SetRedColor(() =>
                {
                    ConsoleLine();
                    Console.WriteLine("No orders found.");
                    ConsoleLine();
                });
                continue;
            }
            foreach (var order in orders)
            {
                var customerName = $"{order.Custumer.FirstName}";
                var product = _context.Products.FirstOrDefault(p => p.Id == order.OrderItem.ProductId);
                if (product != null)
                {
                    ColorfulWriteLine($"Order ID: {order.Id}, User: {customerName}, Product: {product.Name}", ConsoleColor.Yellow);
                    SetGreenColor(() =>
                    {
                        ConsoleLine();
                    });
                }
            }
            ColorfulWriteLine("Choose Order By Its ID: ", ConsoleColor.Yellow);
            string input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                ConsoleClear();
                SetGreenColor(() =>
                {
                    ConsoleLine();
                });
                foreach (var order in orders)
                {
                    var customerName = $"{order.Custumer.FirstName}";
                    var product = _context.Products.FirstOrDefault(p => p.Id == order.OrderItem.ProductId);
                    if (product != null)
                    {
                        ColorfulWriteLine($"Order ID: {order.Id}, User: {customerName}, Product: {product.Name}", ConsoleColor.Yellow);
                        SetGreenColor(() =>
                        {
                            ConsoleLine();
                        });
                    }
                }
                ColorfulWriteLine("Invalid input! Order ID cannot be empty. Enter Order ID: ", ConsoleColor.Red);
                input = Console.ReadLine();
            }
            int selectedOrderId;
            while (!int.TryParse(input, out selectedOrderId))
            {
                ConsoleClear();
                SetGreenColor(() =>
                {
                    ConsoleLine();
                });
                foreach (var order in orders)
                {
                    var customerName = $"{order.Custumer.FirstName}";
                    var product = _context.Products.FirstOrDefault(p => p.Id == order.OrderItem.ProductId);
                    if (product != null)
                    {
                        ColorfulWriteLine($"Order ID: {order.Id}, User: {customerName}, Product: {product.Name}", ConsoleColor.Yellow);
                        SetGreenColor(() =>
                        {
                            ConsoleLine();
                        });
                    }
                }
                ColorfulWriteLine("Invalid input! Enter numeric Order ID:", ConsoleColor.Red);
                input = Console.ReadLine();
            }
            string invoiceFileName = $"order_{selectedOrderId}_invoice.txt";
            if (File.Exists(invoiceFileName))
            {
                using (StreamReader reader = new StreamReader(invoiceFileName))
                {
                    ConsoleClear();
                    ColorfulWriteLine($"Showing Invoice for Order ID: {selectedOrderId}\n", ConsoleColor.Green);
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
            else
            {
                ConsoleClear();
                SetYellowColor(() =>
                {
                    ConsoleLine();
                    Console.WriteLine($"Invoice for Order ID {selectedOrderId} was not found.");
                    ConsoleLine();
                });
            }
        }
        /// VIEW INVOICE /// VIEW INVOICE /// VIEW INVOICE /// VIEW INVOICE
        /// VIEW INVOICE /// VIEW INVOICE /// VIEW INVOICE /// VIEW INVOICE
        /// VIEW SYSTEM LOG  /// VIEW SYSTEM LOG  /// VIEW SYSTEM LOG
        /// VIEW SYSTEM LOG  /// VIEW SYSTEM LOG  /// VIEW SYSTEM LOG
        else if (FileManagementChoice == "2")
        {
            ConsoleClear();
            using (StreamReader reader = new StreamReader(logFileName))
            {
                string fileContent = reader.ReadToEnd();
                Console.WriteLine(fileContent);
            }
        }
        /// VIEW SYSTEM LOG  /// VIEW SYSTEM LOG  /// VIEW SYSTEM LOG
        /// VIEW SYSTEM LOG  /// VIEW SYSTEM LOG  /// VIEW SYSTEM LOG
        else
        {
            InvalidChoice();
        }
    }
    /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE
    /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE
    /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE /// FILE
    else if (choice == "6")
    {
        ConsoleClear();
        ColorfulWriteLine("1. Send SMS", ConsoleColor.Blue);
        ColorfulWriteLine("2. View System Information", ConsoleColor.Green);
        ColorfulWriteLine("3. Dotnet", ConsoleColor.Yellow);
        ColorfulWriteLine("4. Play Music", ConsoleColor.Magenta);
        ColorfulWriteLine("5. Delete All Data", ConsoleColor.DarkRed);
        string OptionsChoice = Console.ReadLine();
        if (OptionsChoice == "1")
        {
            TwilioService.InitializeTwilio();
        }
        else if (OptionsChoice.ToLower() == "2")
        {
            SystemInfo.DisplaySystemDetails();
        }
        else if (OptionsChoice.ToLower() == "3")
        {
            Dotnet.InitializeDotnet();
        }
        else if (OptionsChoice.ToLower() == "4")
        {
            Music.PlayMusic();
        }
        else if (OptionsChoice.ToLower() == "5")
        {
            DeleteData(_context);
        }
        else
        {
            InvalidChoice();
        }
    }
    else if (choice == "7")
    {
        Environment.Exit(0);
    }
    else
    {
        InvalidChoice();
    }
}
/// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP
/// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP
/// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP /// APP