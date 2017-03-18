using System.Security.Principal;

namespace DomurTech.Core.Security
{
    public class Principal : IPrincipal
    {
        private readonly Identity _identity;

        public Principal(Identity identity)
        {
            _identity = identity;
        }

        public bool IsInRole(string role)
        {
            return false;
        }

        public IIdentity Identity => _identity;
    }
}
