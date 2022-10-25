using Microsoft.EntityFrameworkCore;
using Movies.Service.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieReviews.Database
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<bool> AddUser(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteUser(User user)
        {
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

            }
            catch
            {
                return false;
            }
            return true;
        }

        public Task<User> GetUserById(Guid UserId)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
        }

        public Task<User> GetUserByMail(string mail)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Mail == mail);
        }

        public Task<List<User>> GetUsers(Guid AdminId)
        {
            return _context.Users.ToListAsync();
        }
    }
}