using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.service.Common.Models;

namespace MovieReviews.Database
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public Task<Review> GetMovieByIdAsync(Guid id)
        {
            return _context.Reviews.Where(r => r.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Review> AddReviewToMovieAsync(Guid movieId, Review review)
        {
            review.Id = Guid.NewGuid();
            review.MovieId = movieId;
            _context.Reviews.Add(review);

            var result = _context.Reviews.Join(_context.Movies,
                    entryPoint => entryPoint.MovieId,
                    entry => entry.Id,
                    (entryPoint, entry) => new { entryPoint, entry }).ToList();

            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<List<Review>> GetReviewsByMovieId(Guid movieId)
        {
            return await _context.Reviews.Where(r => r.MovieId == movieId).ToListAsync();
        }
    }
}