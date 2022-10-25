namespace Movies.Service.Common.Models
{
    public class SignUpResponse : AuthenticateResponse
    {
        public SignUpResponse(User user, string accessToken, string refreshToken) : base(user, accessToken, refreshToken)
        {
        }

        public SignUpResponse()
        {

        }
    }
}
