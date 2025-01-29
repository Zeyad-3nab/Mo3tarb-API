using Mo3tarb.Core.Entites;
using System.Net.Mail;
using System.Net;

namespace Mo3tarb.APIs.PL.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Emails email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("zeyadenab220@gmail.com", "kqcmidghkjybpdph");
            client.Send("zeyadenab220@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
