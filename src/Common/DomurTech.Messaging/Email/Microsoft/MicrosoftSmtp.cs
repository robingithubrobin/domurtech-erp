using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using DomurTech.Helpers;
using DomurTech.Messaging.Email.Abstract;
using DomurTech.Messaging.Entities;
using DomurTech.Messaging.Options;
using DomurTech.Providers;

namespace DomurTech.Messaging.Email.Microsoft
{
    public class MicrosoftSmtp : ICustomSmtp
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
            var mailMessage = new MailMessage
            {
                From = new MailAddress(eMailMessage.From.Address, eMailMessage.From.DisplayName),
                HeadersEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8,
                Subject = eMailMessage.Subject,
                Body = eMailMessage.Body,
                IsBodyHtml = eMailMessage.IsBodyHtml,
                Priority = ConvertHelper.ConvertToEnum<MailPriority>(eMailMessage.Priority.ToString())
            };
            foreach (var mailAddress in eMailMessage.To)
            {
                mailMessage.To.Add(mailAddress.Address);
            }
            if (eMailMessage.Cc != null)
            {
                foreach (var mailAddress in eMailMessage.Cc)
                {
                    mailMessage.CC.Add(mailAddress.Address);
                }
            }

            if (eMailMessage.Bcc != null)
            {
                foreach (var mailAddress in eMailMessage.Bcc)
                {
                    mailMessage.Bcc.Add(mailAddress.Address);
                }
            }

            if (eMailMessage.Attachments != null)
            {
                foreach (var eMailAttachment in eMailMessage.Attachments)
                {
                    mailMessage.Attachments.Add(new Attachment(eMailAttachment.ContentStream, eMailAttachment.Name, eMailAttachment.MediaType));
                }
            }

            var smtpClient = new SmtpClient
            {
                Host = _smtpClient.Host,
                Port = _smtpClient.Port,
                EnableSsl = _smtpClient.EnableSsl
            };

            if (!_smtpClient.UseDefaultCredentials)
            {
                if (!_smtpClient.UseDefaultNetworkCredentials)
                {
                    smtpClient.Credentials = new NetworkCredential
                    {
                        UserName = _smtpClient.Username,
                        Password = _smtpClient.Password
                    };
                }
                else
                {
                    smtpClient.UseDefaultCredentials = true;
                }
            }
            else
            {
                smtpClient.UseDefaultCredentials = true;
            }
            smtpClient.Send(mailMessage);
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
