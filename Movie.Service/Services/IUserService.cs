using Movies.Service.Common.Models;
using Movies.Service.Common.Models.Login.SignUp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieReviews.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);

        Task<SignUpResponse> SignUp(SignUpInput model);

        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(Guid id);
    }
}
