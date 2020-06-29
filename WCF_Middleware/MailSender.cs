using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace WCF_Middleware {
    public class MailSender {

        public static void sendMail() {
            try {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("antoine.braesch@viacesi.fr");
                message.To.Add(new MailAddress("antoine.braesch@viacesi.fr"));
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "ceci est un test";
                smtp.Port = 587;
                smtp.Host = "SMTP.office365.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("antoine.braesch@viacesi.fr", "Cavag466");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            } catch (Exception e){
                Console.WriteLine(e.ToString());
            }
            
            
        }
    }
}
