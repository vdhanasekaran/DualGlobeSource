using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace DualGlobe.ERP.Utility
{
    public class CustomPrincipal: IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            if (roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string Username)
        {
            Identity = new GenericIdentity(Username);
        }

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> roles { get; set; }
    }
}