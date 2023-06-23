using System;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Exam_Prep_Assignment___API.View_Models;

namespace Exam_Prep_Assignment___API.EmailServices
{
    public class EmailService : IEmailService
    {
        EmailSettings _emailSettings = null;
        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }
        public bool SendEmail(Email email)
        {
            try
            {
                SmtpClient gmailClient = new SmtpClient
                {
                    Host = _emailSettings.Host,
                    Port = _emailSettings.Port,
                    EnableSsl = _emailSettings.UseSSL,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(_emailSettings.EmailId, _emailSettings.Password)
                };

                gmailClient.Send(_emailSettings.EmailId, email.EmailToId, email.EmailSubject, email.EmailBody);
                gmailClient.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                //Log Exception Details
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}
