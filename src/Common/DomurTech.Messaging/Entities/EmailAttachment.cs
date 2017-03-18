using System.IO;

namespace DomurTech.Messaging.Entities
{
    public class EmailAttachment
    {
        public Stream ContentStream { get; set; }
        public string Name { get; set; }
        public string MediaType { get; set; }
    }
}
