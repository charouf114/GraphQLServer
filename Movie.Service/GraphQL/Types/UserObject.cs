using GraphQL.Types;
using Movies.Service.Common.Models;

namespace Movies.Service.GraphQL
{
    public class UserObject : ObjectGraphType<User>
    {
        public UserObject()
        {
            Name = nameof(User);
            Description = "A User";

            Field(d => d.FirstName).Description("FirstName");
            Field(d => d.LastName).Description("LastName");
            Field(d => d.Username).Description("Username");
            Field(d => d.Mail).Description("Mail");
            Field(d => d.PhoneNumber).Description("PhoneNumber");
            Field(d => d.Function).Description("Function");
        }
    }
}
