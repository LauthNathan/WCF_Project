using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using WCF_Service;

namespace WCF_Middleware {
    public class MailSender {

        public static void sendMail(MSG msg) {
            try {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("devcesi@outlook.fr");
                message.To.Add(new MailAddress("clement.acker@viacesi.fr"));
                message.Subject = "T bo";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "abraesch s'est connecté";
                smtp.Port = 587;
                smtp.Host = "SMTP.office365.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("devcesi@outlook.fr", "Password420");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            } catch (Exception e){
                Console.WriteLine(e.ToString());
            }
            
            
        }
    }
}
