using Movies.Service.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieReviews.Database
{
    public interface IUserRepository
    {
        Task<User> GetUserByMail(string mail);

        Task<User> GetUserById(Guid UserId);

        Task<List<User>> GetUsers(Guid AdminId);

        Task<bool> AddUser(User user);

        Task<bool> UpdateUser(User user);

        Task<bool> DeleteUser(User user);
    }
}