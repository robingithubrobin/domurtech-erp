using System.Collections.Generic;
using DomurTech.Helpers;
using DomurTech.Messaging.Email.Abstract;
using DomurTech.Messaging.Entities;
using DomurTech.Messaging.Options;
using DomurTech.Providers;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace DomurTech.Messaging.Email.MailKit
{
    public class MailKitSmtp : ICustomSmtp
    {
        private readonly CustomSmtpClient _smtpClient = new CustomSmtpClient
        {
            EnableSsl = SystemSettings.SmtpSsl,
            Host = SystemSettings.SmtpServer,
            Port = SystemSettings.SmtpPort,
            Username = SystemSettings.SmtpUser,
            Password = SystemSettings.SmtpPassword
        };

        public void Send(EmailMessage eMailMessage)
        {
            var message = new MimeMessage {Subject = eMailMessage.Subject};
            message.From.Add(new MailboxAddress(eMailMessage.From.DisplayName, eMailMessage.From.Address));
            foreach (var mailAddress in eMailMessage.To)
            {
                message.To.Add(new MailboxAddress("", mailAddress.Address));
            }
            if (eMailMessage.Cc != null)
            {
                foreach (var mailAddress in eMailMessage.Cc)
                {
                    message.Cc.Add(new MailboxAddress("", mailAddress.Address));

                }
            }

            if (eMailMessage.Bcc != null)
            {
                foreach (var mailAddress in eMailMessage.Bcc)
                {
                    message.Bcc.Add(new MailboxAddress("", mailAddress.Address));
                }
            }


            var bodyBuilder = new BodyBuilder();
            if (eMailMessage.IsBodyHtml)
            {
                bodyBuilder.HtmlBody = eMailMessage.Body;
                message.Body = bodyBuilder.ToMessageBody();
            }
            else
            {
                message.Body = new TextPart("plain")
                {
                    Text = eMailMessage.Body
                };
            }

            using (var smtpClient = new SmtpClient())
            {

                smtpClient.Connect(_smtpClient.Host, _smtpClient.Port, _smtpClient.EnableSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.None);
                if (!_smtpClient.UseDefaultCredentials)
                {
                    if (!_smtpClient.UseDefaultNetworkCredentials)
                    {
                        smtpClient.Authenticate(_smtpClient.Username, _smtpClient.Password);
                    }
                }
                smtpClient.Send(message);
                smtpClient.Disconnect(true);
            }
        }

        public void SendWithTemplate(EmailMessage emailMessage, string emailTemplate, List<EmailRow> emailRows)
        {
            foreach (var emailRow in emailRows)
            {
                emailMessage.Body = emailTemplate;
                var toEmail = new EmailAddress();
                foreach (var emailKey in emailRow.EmailKeys)
                {
                    if (emailKey.Key != EmailKeyOptions.Name)
                    {
                        if (emailKey.Key == EmailKeyOptions.ToEmail)
                        {
                            toEmail.Address = emailKey.Value;
                            continue;
                        }
                        emailMessage.Body = StringHelper.TemplateParser(emailMessage.Body, string.Format(EmailConstants.EmailKeyRegEx, emailKey.Key), emailKey.Value);
                    }
                    else
                    {
                        toEmail.DisplayName = emailKey.Value;
                    }
                }
                Send(emailMessage);
            }
        }
    }
}
