using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Movies.service.Common.Models;
using Movies.Service.Common.Models;
using System.Threading.Tasks;

namespace MovieReviews.Database
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Drink> Drinks { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<User> Users { get; set; }


        public async new Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUser>()
                .ToTable("AspNetUsers", t => t.ExcludeFromMigrations());

            modelBuilder.Entity<User>()
                .HasMany(x => x.Drinks)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}