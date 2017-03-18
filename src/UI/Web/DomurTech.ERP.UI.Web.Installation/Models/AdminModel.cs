using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.UI.Web.Installation.Models
{
    public class AdminModel : IBaseModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }
        public bool HasError { get; set; }
    }
}