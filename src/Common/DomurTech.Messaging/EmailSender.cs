using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DomurTech.Globalization;
using DomurTech.Helpers;
using DomurTech.Messaging.Email.Abstract;
using DomurTech.Messaging.Entities;
using DomurTech.Messaging.Options;
using DomurTech.Providers;

namespace DomurTech.Messaging
{
    public class EmailManager : IEmailManager
    {

        private readonly ICustomSmtp _smtp;

        public EmailManager(ICustomSmtp smtp)
        {
            _smtp = smtp;
        }

        private static string ConvertTemplateToString(string emailTemplate, EmailRow emailRow)
        {
            return emailRow.EmailKeys.Aggregate(emailTemplate, (current, key) => StringHelper.TemplateParser(current, EmailConstants.EmailKeyRegEx.Replace(EmailConstants.EmailTokenName, key.Key.ToString()), key.Value));
        }

        public void SendEmailToUser(EmailUser user, EmailTypeOptions emailTypes)
        {
            var templateFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        "" + SystemSettings.EmailTemplatePath + "\\" + emailTypes + ".html");
            var templateText = FileHelper.ReadAllLines(templateFilePath);
            var eMailMessage = new EmailMessage();
            var from = new EmailAddress
            {
                DisplayName = SystemSettings.SmtpSenderName,
                Address = SystemSettings.SmtpSenderMail
            };
            eMailMessage.To = new List<EmailAddress>
            {
                new EmailAddress
                {
                    Address = user.Email
                }
            };
            string eMailSubject;
            var emailRow = new EmailRow();
            var emailKeys = new List<EmailKey>
            {
                new EmailKey
                {
                    Key = EmailKeyOptions.ApplicationName,
                    Value = Dictionaries.ApplicationName
                },
                new EmailKey
                {
                    Key = EmailKeyOptions.ApplicationUrl,
                    Value = SystemSettings.ApplicationUrl
                },
                new EmailKey
                {
                    Key = EmailKeyOptions.FirstName,
                    Value = user.FirstName
                }
                ,
                new EmailKey
                {
                    Key = EmailKeyOptions.LastName,
                    Value = user.LastName
                }
            };
            switch (emailTypes)
            {
                case EmailTypeOptions.Add:
                    {
                        eMailSubject = Dictionaries.UserInformation;
                        emailKeys.Add(new EmailKey
                        {
                            Key = EmailKeyOptions.Username,
                            Value = user.Username
                        });
                        emailKeys.Add(new EmailKey
                        {
                            Key = EmailKeyOptions.Password,
                            Value = user.Password
                        });
                        break;
                    }
                case EmailTypeOptions.SignUp:
                    {
                        eMailSubject = Dictionaries.UserInformation;
                        emailKeys.Add(new EmailKey
                        {
                            Key = EmailKeyOptions.Username,
                            Value = user.Username
                        });
                        emailKeys.Add(new EmailKey
                        {
                            Key = EmailKeyOptions.Password,
                            Value = user.Password
                        });
                        emailKeys.Add(new EmailKey
                        {
                            Key = EmailKeyOptions.ActivationCode,
                            Value = (user.Id + "@" + user.CreateDate).Encrypt()
                        });
                        break;
                    }
                case EmailTypeOptions.ForgotPassword:
                    {
                        eMailSubject = Dictionaries.NewPassword;

                        emailKeys.Add(new EmailKey
                        {
                            Key = EmailKeyOptions.Username,
                            Value = user.Username
                        });
                        emailKeys.Add(new EmailKey
                        {
                            Key = EmailKeyOptions.Password,
                            Value = user.Password
                        });
                        break;
                    }
                case EmailTypeOptions.Update:
                    {
                        eMailSubject = Dictionaries.UserInformation;
                        break;
                    }

                case EmailTypeOptions.UpdateMyPassword:
                    {
                        eMailSubject = Dictionaries.NewPassword;
                        emailKeys.Add(new EmailKey
                        {
                            Key = EmailKeyOptions.Username,
                            Value = user.Username
                        });
                        emailKeys.Add(new EmailKey
                        {
                            Key = EmailKeyOptions.Password,
                            Value = user.Password
                        });
                        break;
                    }
                case EmailTypeOptions.UpdateMyInformation:
                    {
                        eMailSubject = Dictionaries.UserInformation;
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(emailTypes), emailTypes, null);
            }
            emailKeys.Add(new EmailKey
            {
                Key = EmailKeyOptions.Subject,
                Value = eMailSubject
            });
            emailRow.EmailKeys = emailKeys;
            eMailMessage.Subject = eMailSubject;
            eMailMessage.From = from;
            eMailMessage.IsBodyHtml = true;
            eMailMessage.Priority = EmailPriorityOptions.Normal;
            eMailMessage.Body = ConvertTemplateToString(templateText, emailRow);
            _smtp.Send(eMailMessage);
        }
    }
}
