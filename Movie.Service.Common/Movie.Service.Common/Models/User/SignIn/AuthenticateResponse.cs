using System;

namespace Movies.Service.Common.Models
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public AuthenticateResponse()
        {

        }

        public AuthenticateResponse(User user, string accessToken, string refreshToken)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
