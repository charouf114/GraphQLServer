using System.Collections.Generic;
using System.Security.Claims;

namespace Movies.Service.GraphQL
{
    public class MyGraphQLUserContext : Dictionary<string, object>
    {
        public ClaimsPrincipal User { get; set; }

        public MyGraphQLUserContext(ClaimsPrincipal user)
        {
            // We Set Him In JWtMiddleware In the Catch Block
            User = user;
        }
    }
}
