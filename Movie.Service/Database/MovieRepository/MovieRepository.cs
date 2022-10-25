using Microsoft.EntityFrameworkCore;
using Movies.service.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviews.Database
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public Task<Movie> GetMovieByIdAsync(Guid id)
        {
            return _context.Movies.Where(m => m.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Movie> AddMovie(Movie movie)
        {
            using (var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.Snapshot))
            {
                try
                {
                    movie.Id = movie.Id == default(Guid) ? Guid.NewGuid() : movie.Id;
                    _context.Movies.Add(movie);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                    return movie;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }

        }

        public Task<List<Movie>> GetAllMovies()
        {

            return _context.Movies.ToListAsync();
        }
    }
}