using GraphQL.Types;
using Movies.Service.Common.Models.Login.SignUp;

namespace MovieReviews.GraphQL.Types
{
    public sealed class SignUpInputObject : InputObjectGraphType<SignUpInput>
    {
        public SignUpInputObject()
        {
            Name = "SignUpInput";
            Description = "SignUpInput";

            Field(a => a.FirstName).Description("FirstName");
            Field(a => a.LastName).Description("LastName");
            Field(a => a.Mail).Description("Mail");
            Field(a => a.PhoneNumber).Description("PhoneNumber");
            Field(a => a.Password).Description("Password");
            Field(a => a.ReTypedPassword).Description("ReTypedPassword");
            Field(a => a.Function).Description("Function");
            Field(a => a.Roles).Description("Roles");
        }
    }
}