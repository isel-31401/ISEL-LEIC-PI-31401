using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;


namespace CurricularUnitFormManager.Models.Mail
{
    public class MailLoadConfig
    {
        private readonly static String _filePath = "./Content/config/mail.config";
        public static MailModel Load()
        {
            Environment.CurrentDirectory = HttpContext.Current.Server.MapPath("/");
            XmlTextReader xmlFile = new XmlTextReader(_filePath);
            xmlFile.Read();
            MailModel mail = new MailModel();
            while (xmlFile.Read())
            {
                xmlFile.MoveToElement();
                if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name.Equals("mail"))
                {
                    mail.SmtpPassWord = xmlFile.GetAttribute("smtpPassword").Trim();
                    mail.SmtpPort = Int16.Parse(xmlFile.GetAttribute("smtpPort"));
                    mail.SmtpServer = xmlFile.GetAttribute("smtpServer").Trim();
                    mail.SmtpUserName = xmlFile.GetAttribute("smtpUserName").Trim();
                    mail.smtpFromEmail = xmlFile.GetAttribute("smtpFromEmail").Trim();
                }

            }
            xmlFile.Close();
            return mail;
        }
    }
}