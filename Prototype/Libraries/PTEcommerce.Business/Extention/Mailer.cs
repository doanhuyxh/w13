using Framework.Configuration;
using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PTEcommerce.Business
{
    public class Mailer
    {
        public void SendEmail(string subject, string body, string receiveEmail, string bccEmail = "", string attachment = "")
        {
            string mailHost = Config.GetConfigByKey("Host");
            int mailPort = Convert.ToInt32(Config.GetConfigByKey("Port"));
            string mailSender = Config.GetConfigByKey("Sender");
            string mailAddress = Config.GetConfigByKey("MailAddress");
            string mailPassword = Config.GetConfigByKey("MailPassword");
            try
            {
                //The system send email with validation link
                SmtpClient smtp = new SmtpClient
                {
                    Host = mailHost,
                    Port = mailPort,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(mailAddress, mailPassword),
                    Timeout = 60000
                };
                MailMessage message = new MailMessage()
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body,
                    From = new MailAddress(mailAddress, mailSender)
                };
                string[] toMails = receiveEmail.Split(',');
                foreach (string item in toMails)
                {
                    message.To.Add(item);
                }
                if (!string.IsNullOrEmpty(bccEmail))
                {
                    string[] bccMails = bccEmail.Split(',');
                    foreach (string addr in bccMails)
                    {
                        message.Bcc.Add(addr);
                    }
                }
                if (!string.IsNullOrEmpty(attachment))
                {
                    Attachment attachmentFile = new Attachment(HttpContext.Current.Server.MapPath(attachment));
                    message.Attachments.Add(attachmentFile);
                }
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message,ex);
            }
        }
    }
}
