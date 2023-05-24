using System.Security.Claims;
using System.Security.Principal;

namespace AppDentistry.Services
{
    public class BaseService
    {
        public class Identity : IIdentity
        {
            public Identity(ClaimsPrincipal identity)
            {
                AuthenticationType = identity.Identity.AuthenticationType;
                IsAuthenticated = identity.Identity.IsAuthenticated;
                Name = identity.Identity.Name;
                long id;
                if (long.TryParse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out id))
                {
                    Id = id;
                }
            }
            public string AuthenticationType { get; set; }
            public bool IsAuthenticated { get; set; }
            public string Name { get; set; }
            public long? Id { get; set; }
        }
    }
}
