using System;
using System.Collections.Generic;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    public class Person : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TcKimlikNo { get; set; }
        public DateTime BirthDate { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreateDate { get; set; }      
        public virtual IList<User> Users { get; set; }

    }
}
