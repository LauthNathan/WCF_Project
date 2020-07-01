using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using WCF_Service;

namespace WCF_Middleware {
    public class MailSender {

        public static void sendMail(MSG msg) {
            DataAccess dA = new DataAccess();
            string to = dA.getMail(msg.tokenUser);
            if(to != null) {
                try {

                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress("developpeurcesi@outlook.fr");
                    message.To.Add(new MailAddress(to));
                    message.Subject = "Update : fichiers chiffrés";
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = "L'information secrète a été trouvée";
                    smtp.Port = 587;
                    smtp.Host = "SMTP.office365.com"; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("developpeurcesi@outlook.fr", "Password420");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                } catch (Exception e) {
                    Console.WriteLine("Mail sent to user");
                    Console.WriteLine(e.ToString());
                }
            } else {
                try {

                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress("developpeurcesi");
                    message.To.Add(new MailAddress("developpeurcesi"));
                    message.Subject = "Erreur mail";
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = "L'utilisateur avec le user token " + msg.tokenUser  + " n'a pas de de mail";
                    smtp.Port = 587;
                    smtp.Host = "SMTP.office365.com"; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("developpeurcesir", "Password420");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                } catch (Exception e) {
                    Console.WriteLine("Mail sent to admin");
                    Console.WriteLine(e.ToString());
                }
            }
            
            
            
        }

       

        
    }
}
