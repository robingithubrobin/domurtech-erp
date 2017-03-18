using System.Collections.Generic;
using DomurTech.Messaging.Entities;

namespace DomurTech.Messaging.Email.Abstract
{
    public interface ICustomSmtp
    {
        void Send(EmailMessage eMailMessage);
        void SendWithTemplate(EmailMessage emailMessage, string emailTemplate, List<EmailRow> emailRows);
    }
}
