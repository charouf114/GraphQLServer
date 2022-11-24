using Microsoft.EntityFrameworkCore;
using Movies.service.Common.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviews.Database
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly ApplicationDbContext _context;

        public DrinkRepository(ApplicationDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<Drink> GetLatestCoffe(Drink drink)
        {
            var drinkResult = await _context.Drinks.Where(d => d.BadgeId == drink.BadgeId).Include(x => x.User)
                           .OrderByDescending(d => d.CreationDate)
                           .FirstOrDefaultAsync();
            if (drinkResult != null)
                drinkResult.character = new Droid()
                {
                    Name = "Achref",
                    Id = Guid.NewGuid()
                };
            return drinkResult;
        }

        public async Task<Drink> AddCoffe(Drink drink)
        {
            var latestDrink = await GetLatestCoffe(drink);

            if (latestDrink == null)
            {
                drink.CreationDate = DateTime.UtcNow;
                _context.Drinks.Add(drink);
            }
            else
            {
                // For Now We Can Update the value Only 
                // We Don't need All the history :)
                latestDrink.CreationDate = DateTime.UtcNow;
                latestDrink.DrinkType = drink.DrinkType;
                latestDrink.SucreCount = drink.SucreCount;
                latestDrink.HasMug = drink.HasMug;
                latestDrink.UserId = drink.UserId;
            }
            await _context.SaveChanges();
            return drink;
        }
    }
}