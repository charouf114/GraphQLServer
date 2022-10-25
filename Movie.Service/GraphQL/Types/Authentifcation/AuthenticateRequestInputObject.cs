using GraphQL.Types;
using Movies.Service.Common.Models;

namespace MovieReviews.GraphQL.Types
{
    public sealed class AuthenticateRequestInputObject : InputObjectGraphType<AuthenticateRequest>
    {
        public AuthenticateRequestInputObject()
        {
            Name = "AuthenticateRequestInput";
            Description = "AuthenticateRequestInput";

            Field(a => a.Mail).Description("Mail");
            Field(a => a.Password).Description("Password");
        }
    }
}