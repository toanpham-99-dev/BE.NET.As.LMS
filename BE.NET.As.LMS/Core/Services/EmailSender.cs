using BE.NET.As.LMS.Core.Interfaces;
using MimeKit;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit.Text;
using MailKit.Security;
using System;
using System.Web;
using BE.NET.As.LMS.Core.Models;
using Microsoft.Extensions.Options;

namespace BE.NET.As.LMS.Core.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly MailSetting _mailSetting;
        public EmailSender(IOptions<MailSetting> mailSetting)
        {
            _mailSetting = mailSetting.Value;
        }
        public async Task<bool> SendEmailAsync(string subject, string email, string content)
        {
            using var smtp = new SmtpClient();         
            var sendEmail = new MimeMessage();
            sendEmail.Sender = new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Mail);
            sendEmail.From.Add(new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Mail));
            sendEmail.To.Add(MailboxAddress.Parse(email));
            sendEmail.Subject = subject;
            sendEmail.Body = new TextPart(TextFormat.Html) { Text = content};      
            try
            {   
                await smtp.ConnectAsync(_mailSetting.Host, _mailSetting.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailSetting.Mail, _mailSetting.Password);
                await smtp.SendAsync(sendEmail);
                await smtp.DisconnectAsync(true);
                return true;
            }
            catch
            {
                System.IO.Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                return false;
            }

        }
    }
}