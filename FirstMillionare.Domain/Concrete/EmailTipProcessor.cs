using System.Text;
using System.Net;
using System.Net.Mail;
using System;
using FirstMillionare.Domain.Abstract;

namespace FirstMillionare.Domain.Concrete
{
    public class EmailSettings
    {
        public const string EmailSender= "orcoss36@gmail.com";

        public string MailToAddress { get; private set; }
        public string MailFromAddress { get; private set; }
        public bool UseSsl { get; private set; }
        public string Password { get; private set; }
        public string ServerName { get; private set; }
        public int ServerPort { get; private set; }

        public EmailSettings(string mailToAddress, string mailFromAddress, string password)
        {
            MailToAddress = mailToAddress;
            MailFromAddress = mailFromAddress;           
            Password = password;
            ServerName = "smtp.gmail.com";
            UseSsl = true;
            ServerPort = 587;
        }
    }

    public class EmailTipProcessor : ICallTipProcessor
    {
        private EmailSettings _emailSettings;

        public EmailTipProcessor(EmailSettings settings)
        {
            _emailSettings = settings;
        }

        public void ProcessQuestion(Entities.QuestionItem question)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = _emailSettings.UseSsl;
                smtpClient.Host = _emailSettings.ServerName;
                smtpClient.Port = _emailSettings.ServerPort;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(EmailSettings.EmailSender,
                          _emailSettings.Password);
                smtpClient.Timeout = 20000;

                #region Створення тіла листа

                StringBuilder body = new StringBuilder()
                    .AppendLine("Прийшло нове запитання")
                    .AppendLine("---");
                    
                body.AppendLine("Запитання:");
                body.AppendLine(question.QuestionText)
                    .AppendLine("---");

                body.AppendLine("Варіанти відповіді:");
                foreach (var option in question.Options)
                {
                    body.AppendLine(option.OptionText);
                }
                                        
                body.AppendLine(String.Format("E-mail: {0}", _emailSettings.MailFromAddress))                    
                    .AppendLine("---");
                #endregion

                MailMessage mailMessage = new MailMessage(
                                       EmailSettings.EmailSender,   
                                       _emailSettings.MailToAddress,    
                                       "Нове питання!",              
                                       body.ToString());                
                smtpClient.Send(mailMessage);
            }
        }
    }
}