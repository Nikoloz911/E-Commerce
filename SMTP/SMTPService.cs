using System.Net;
using System.Net.Mail; 

namespace E_Commerce.SMTP;
internal class SMTPService
{
    public static string GenerateVerificationCode()
    {
        Random random = new Random();
        return random.Next(1000, 9999).ToString(); 
    }
    public static void EmailSender(string ToAddress, string verificationCode)
    {
        string senderEmail = "nikalobjanidze014@gmail.com";
        string appPassword = "MY APP PASSWORD HERE"; /// APP PASSWORD
        string htmlContent = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Verification Email</title>
</head>
<body style='margin: 0; padding: 0; font-family: sans-serif; background-color: #000000; color: #ffffff;'>
    <table width='100%' cellpadding='0' cellspacing='0' style='background-color: #000000; text-align: center;'>
        <tr>
            <td>
              <h1 style='color: #ffffff; font-size: 35px; margin: 40px 0 10px;'>E_Commerce</h1>
            </td>
        </tr>
        <tr>
            <td>
                <p style='font-size: 19px; margin: 20px 0;'>Hello,</p>
                <p style='font-size: 19px; margin: 20px 0;'>Thank you for registering with E_Commerce!</p>
                <p style='font-size: 19px; margin: 20px 0;'>Please use the verification code below to complete your registration:</p>
                <p style='font-size: 50px; margin: 40px 0; color: #0076be; font-weight: bold;'>{verificationCode}</p>
                <p style='font-size: 19px; margin: 20px 0;'>If you did not request this, please ignore this email.</p>
                <p style='font-size: 19px; margin: 20px 0;'>Best regards,</p>
                <p style='font-size: 19px; margin: 20px 0;'>E_Commerce ConsoleApp</p>
            </td>
        </tr>
    </table>
</body>
</html>";

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(senderEmail);
        mail.To.Add(ToAddress);
        mail.Subject = "Verification Code";
        mail.Body = htmlContent;
        mail.IsBodyHtml = true;
        var smtpClient = new SmtpClient("smtp.gmail.com") // smtp.google.com
        {
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential(senderEmail, appPassword),
        };
        smtpClient.Send(mail);
    }
    public static void SendUpdateNotification(string toAddress, string updateType, string updatedValue)
    {
        string senderEmail = "nikalobjanidze014@gmail.com";
        string appPassword = "MY APP PASSWORD HERE"; /// APP PASSWORD
        string ipAddress = Dns.GetHostAddresses(Dns.GetHostName())
                                 .FirstOrDefault()?.ToString() ?? "Unknown IP";
        string htmlContent = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>{updateType} Update Notification</title>
</head>
<body style='margin: 0; padding: 0; font-family: sans-serif; background-color: #000000; color: #ffffff;'>
    <table width='100%' cellpadding='0' cellspacing='0' style='background-color: #000000; text-align: center;'>
        <tr>
            <td>
                <h1 style='color: #ffffff; font-size: 35px; margin: 40px 0 10px;'>E_Commerce</h1>
            </td>
        </tr>
        <tr>
            <td>
                <p style='font-size: 19px; margin: 20px 0;'>Your account {updateType.ToLower()} has been successfully updated!</p>
                <p style='font-size: 19px; margin: 20px 0;'>New {updateType} is:</p>
                <p style='font-size: 40px; margin: 40px 0; color: #0076be; font-weight: bold;'>{updatedValue}</p>
                <p style='font-size: 19px; margin: 20px 0;'>This Change was made from IP address: {ipAddress}</p>
                <p style='font-size: 19px; margin: 20px 0;'>If you have not done this, please contact us</p>
            </td>
        </tr>
    </table>
</body>
</html>";
        
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(senderEmail);
        mail.To.Add(toAddress);
        mail.Subject = $"{updateType} Update Notification";
        mail.Body = htmlContent;
        mail.IsBodyHtml = true;
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential(senderEmail, appPassword),
        };

        smtpClient.Send(mail);
    }

    public void SendInvoiceWithAttachment(string toAddress, string updateType, string updatedValue, string invoiceFilePath)
    {
        string senderEmail = "nikalobjanidze014@gmail.com";
        string appPassword = "MY APP PASSWORD HERE"; /// APP PASSWORD
        string htmlContent = $@"
            <html>
                <head>
                    <title>{updateType} Place Notification</title>
                </head>
                <body>
                    <h1>{updateType} Placed</h1>
                    <p>{updateType}: {updatedValue}</p>
                </body>
            </html>";
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(senderEmail);
        mail.To.Add(toAddress);
        mail.Subject = $"{updateType} Notification";
        mail.Body = htmlContent;
        mail.IsBodyHtml = true;
        mail.Attachments.Add(new Attachment(invoiceFilePath));
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential(senderEmail, appPassword),
        };
        smtpClient.Send(mail);
    }
}

