using System;
using DomurTech.Core.Abstract;

namespace DomurTech.Messaging.Entities
{
    public class EmailUser : IEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
