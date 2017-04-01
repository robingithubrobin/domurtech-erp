using System;
using System.Collections.Generic;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreateDate { get; set; }
        public Person Person { get; set; }
        public virtual Language Language { get; set; }
        public string FullName => Person.FirstName + " " + Person.LastName;
        public string DisplayName { get; set; }
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }
        public ICollection<RoleUserLine> RoleUserLines { get; set; }
        public ICollection<Session> SessionsCreatedBy { get; set; }
        public ICollection<SessionHistory> SessionHistoriesCreatedBy { get; set; }
    }
}
