using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SurveProzone.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(string name, string email, string phone, string message)
        {
            Console.WriteLine("Email from config: " + _config["EmailSettings:Email"]);
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("SurveProzone", "pawarsanil108@gmail.com"));
                emailMessage.To.Add(new MailboxAddress("Admin", "pawarsanil108@gmail.com"));
                if (!string.IsNullOrWhiteSpace(email) && email.Contains("@"))
                {
                    emailMessage.ReplyTo.Add(new MailboxAddress(name, email));
                }

                emailMessage.Subject = "New Contact Form Submission";

                emailMessage.Subject = "New Contact Form Submission";

                emailMessage.Body = new TextPart("plain")
                {
                    Text = $"Name: {name}\nEmail: {email}\nPhone: {phone}\nMessage: {message}"
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Timeout = 10000;

                    client.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);

                    client.Authenticate("pawarsanil108@gmail.com", "xjmwzignlbsklxad");
                    Console.WriteLine("Sending email...");
                    client.Send(emailMessage);
                    Console.WriteLine("Email sent!");
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Email Failed: " + ex.Message);
            }
        }
    }
}