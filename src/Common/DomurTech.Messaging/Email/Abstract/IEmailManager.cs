using DomurTech.Messaging.Entities;
using DomurTech.Messaging.Options;

namespace DomurTech.Messaging.Email.Abstract
{
    public interface IEmailManager
    {
        void SendEmailToUser(EmailUser user, EmailTypeOptions emailTypes);
    }
}
