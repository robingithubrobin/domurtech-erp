using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace DomurTech.Core.Security
{
    [Serializable]
    public class Identity : IIdentity
    {
        public string Name { get; set; }
        public string AuthenticationType => "AuthenticationType";
        public bool IsAuthenticated { get; set; }
        public Guid UserId { get; set; }
        public string LanguageCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        private List<string> _roles;
        public List<string> Roles
        {
            get
            {
                if (_roles != null) return _roles;
                _roles = new List<string>();
                return _roles;

            }
            set { _roles = value; }
        }

    }
}
