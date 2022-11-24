using Movies.service.Common.Models;
using System.Threading.Tasks;

namespace MovieReviews.Services
{
    public interface IDrinkService
    {
        Task<Drink> GetLatestCoffe(Drink drink);
        Task<Drink> AddCoffe(Drink drink);
    }
}
