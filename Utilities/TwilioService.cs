using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
namespace E_Commerce.Utilities;
internal class TwilioService
{
    public static void ConsoleLine()
    {
        int length = 47;
        Console.WriteLine(new string('-', length));
    }
    public static void ColorfulWriteLine(string text, ConsoleColor color)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = originalColor;
    }
    public static void SetRedColor(Action action)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        action();
        Console.ResetColor();
    }
    public static void ColorfulWrite(string text, ConsoleColor color)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = originalColor;
    }
    public static void SetGreenColor(Action action)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        action();
        Console.ResetColor();
    }
    public static void InitializeTwilio()
    {
        while (true)
        {
            Console.Clear();
            ColorfulWriteLine("Enter the SMS text:", ConsoleColor.Green);
            string smsBody = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(smsBody))
            {
                var accountSid = "AC6050d2b967b8562ed0db05e3c377433f"; /// ACCOUNT SID
                var authToken = "MY AUTH TOKEN HERE"; /// AUTH TOKEN
                TwilioClient.Init(accountSid, authToken);
                var messageOptions = new CreateMessageOptions(new PhoneNumber("995599885001")); /// MY NUMBER
                messageOptions.From = new PhoneNumber("+19783076324"); /// TWILIO PHONE NUMBER
                messageOptions.Body = smsBody;
                var message = MessageResource.Create(messageOptions);
                Console.Clear();         
                SetGreenColor(() =>
                {
                    ConsoleLine();
                    ColorfulWriteLine($"Sent From your Twilio trial account ", ConsoleColor.Green);
                    Console.WriteLine();
                    ColorfulWrite($"'{smsBody}'", ConsoleColor.Cyan);
                    ConsoleLine();
                });
                return;
            }
            else
            {
                Console.Clear();
                SetRedColor(() =>
                {
                    ConsoleLine();
                    ColorfulWriteLine("SMS text cannot be empty!", ConsoleColor.Red);
                    ConsoleLine();
                });         
                ConsoleLine();
                ColorfulWriteLine("1. Main Menu", ConsoleColor.Green);
                ColorfulWriteLine("2. Retry", ConsoleColor.Yellow);
                ConsoleLine();
                string choice = Console.ReadLine()?.Trim().ToUpper();
                if (choice == "1")
                {
                    Console.Clear();
                    return; 
                }
                else if (choice == "2")
                {
                    continue;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    ConsoleLine();
                    Console.WriteLine("Invalid Choice");
                    ConsoleLine();
                    Console.ResetColor();
                    return;
                }
            }
        }
    }
}
