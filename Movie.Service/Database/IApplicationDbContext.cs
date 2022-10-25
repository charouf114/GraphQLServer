using Microsoft.EntityFrameworkCore;
using Movies.service.Common.Models;
using Movies.Service.Common.Models;
using System.Threading.Tasks;

namespace MovieReviews.Database
{
    public interface IApplicationDbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Drink> Drinks { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<User> Users { get; set; }

        Task<int> SaveChanges();
    }
}