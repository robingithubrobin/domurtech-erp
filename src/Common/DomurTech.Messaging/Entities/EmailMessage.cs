using System.Collections.Generic;
using DomurTech.Messaging.Options;

namespace DomurTech.Messaging.Entities
{
    public class EmailMessage
    {
        public List<EmailAttachment> Attachments { get; set; }
        public EmailAddress From { get; set; }
        public List<EmailAddress> To { get; set; }
        public List<EmailAddress> Cc { get; set; }
        public List<EmailAddress> Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailPriorityOptions Priority { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}
