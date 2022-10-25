namespace Movies.Service.Common.Models.Login.SignUp
{
    public class SignUpInput
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string ReTypedPassword { get; set; }

        public string Function { get; set; }

        public string Roles { get; set; }
    }
}
