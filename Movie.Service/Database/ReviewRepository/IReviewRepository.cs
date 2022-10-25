using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.service.Common.Models;

namespace MovieReviews.Database
{
    public interface IReviewRepository
    {
        Task<Review> GetMovieByIdAsync(Guid id);
        Task<Review> AddReviewToMovieAsync(Guid id, Review review);
        Task<List<Review>> GetReviewsByMovieId(Guid movieId);
    }
}