using Movies.service.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieReviews.Database
{
    public interface IMovieRepository
    {
        Task<Movie> AddMovie(Movie movie);

        Task<Movie> GetMovieByIdAsync(Guid id);

        Task<List<Movie>> GetAllMovies();
    }
}
