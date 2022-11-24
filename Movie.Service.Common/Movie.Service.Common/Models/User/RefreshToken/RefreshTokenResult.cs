namespace Movies.Service.Common.Models
{
    class RefreshTokenResult : AuthenticateResponse
    {

        public RefreshTokenResult(User user, string accessToken, string refreshToken) : base(user, accessToken, refreshToken)
        {
        }

        public RefreshTokenResult()
        {

        }
    }
}
