using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieReviews.Database;
using Movies.Service.Common.Helpers;
using Movies.Service.Common.Models;
using Movies.Service.Common.Models.Login.SignUp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieReviews.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;

        // Will Be Deleted Soon
        private User _user = new User { Id = Guid.NewGuid(), FirstName = "Test2", LastName = "User2", PasswordHash = "test" };


        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }

        public async Task<SignUpResponse> SignUp(SignUpInput model)
        {
            var user = await _userRepository.GetUserByMail(model.Mail);

            if (user != null)
            {
                return null;
            }

            if (model.Password != model.ReTypedPassword)
            {
                return null;
            }

            // TODO : Hash The Password And Check Mail + PhoneNumber With Regex

            User userToAdd = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Mail = model.Mail,
                PhoneNumber = model.PhoneNumber,
                Function = model.Function,
                Roles = model.Roles,
                PasswordHash = model.Password
            };

            _userRepository.AddUser(userToAdd);

            // authentication successful so generate jwt token
            var accessToken = generateJwtToken(userToAdd, TokenType.Access);
            var refreshToken = generateJwtToken(userToAdd, TokenType.Refresh);

            return new SignUpResponse(userToAdd, accessToken, refreshToken);

        }


        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await _userRepository.GetUserByMail(model.Mail);

            // return null if user not found
            if (user == null)
            {
                user = _user;
            }
            // authentication successful so generate jwt token
            var accessToken = generateJwtToken(user, TokenType.Access);
            var refreshToken = generateJwtToken(user, TokenType.Refresh);

            return new AuthenticateResponse(user, accessToken, refreshToken);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetUsers(Guid.Empty);
        }

        public async Task<User> GetById(Guid id)
        {
            return await _userRepository.GetUserById(id);
        }

        // helper methods

        private string generateJwtToken(User user, TokenType tokenType)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = tokenType == TokenType.Access ? DateTime.UtcNow.AddMinutes(30) : DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public enum TokenType
    {
        Access = 0,
        Refresh = 1
    }
}
