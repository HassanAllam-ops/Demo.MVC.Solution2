using System.Net;
using System.Net.Mail;

namespace Demo.PresentationLayer.Utilities
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("hassan309pro@gmail.com", "ydazzumcxatywvyh");
            client.Send("hassan309pro@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
