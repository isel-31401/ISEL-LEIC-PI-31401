using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
namespace CurricularUnitFormManager.Models.Mail
{
    public sealed class Mailer
    {
        readonly static String _fromName = "Curricular Unit Form Manager";
        static volatile MailModel _mailer = null;
        static Object _lock = new Object();
        

        public static Boolean SendMail(String receiver, String message)
        {
            if (_mailer == null)
            {
                lock (_lock)
                {
                    if (_mailer == null)
                    {
                        _mailer = MailLoadConfig.Load();
                    }
                }
            }
            //Prepare Message
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_mailer.smtpFromEmail, _fromName);
            mail.To.Add(receiver);
            mail.Subject = "Registration Confirmation Email";
            mail.Body = message;

            //send the message
            SmtpClient smtp = new SmtpClient(_mailer.SmtpServer);
            smtp.Port = _mailer.SmtpPort;
            smtp.Credentials = new NetworkCredential(_mailer.SmtpUserName, _mailer.SmtpPassWord);
            smtp.EnableSsl = true;
            smtp.Send(mail);
            mail.Dispose();
            smtp.Dispose();
            return true;
        }
    }
    public class MailModel
    {
        public String SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public String SmtpUserName { get; set; }
        public String SmtpPassWord { get; set; }
        public String  smtpFromEmail{ get; set; }
    }
}