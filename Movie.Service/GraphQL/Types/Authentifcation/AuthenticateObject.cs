using GraphQL.Types;
using Movies.Service.Common.Models;

namespace MovieReviews.GraphQL.Types
{
    public sealed class AuthenticateObject : ObjectGraphType<AuthenticateResponse>
    {
        public AuthenticateObject()
        {
            Name = nameof(AuthenticateResponse);
            Description = "AuthenticateResponse";

            Field(d => d.Id).Description("Id");
            Field(d => d.FirstName).Description("FirstName");
            Field(d => d.LastName).Description("LastName");
            Field(d => d.Username).Description("Username");
            Field(d => d.AccessToken).Description("AcessToken");
            Field(d => d.RefreshToken).Description("RefreshToken");
        }
    }
}