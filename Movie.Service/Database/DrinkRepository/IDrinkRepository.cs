using System.Threading.Tasks;
using Movies.service.Common.Models;

namespace MovieReviews.Database
{
    public interface IDrinkRepository
    {
        Task<Drink> GetLatestCoffe(Drink drink);
        Task<Drink> AddCoffe(Drink drink);
    }
}