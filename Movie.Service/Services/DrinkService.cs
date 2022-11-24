using MovieReviews.Database;
using Movies.service.Common.Models;
using Movies.Service.Common.Models;
using System;
using System.Threading.Tasks;

namespace MovieReviews.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinkRepository _drinkRepository;
        public readonly IUserRepository _userRepository;

        public DrinkService(IDrinkRepository drinkRepository, IUserRepository userRepository)
        {
            _drinkRepository = drinkRepository;
            _userRepository = userRepository;
        }

        public Task<Drink> AddCoffe(Drink drink)
        {
            User userToAdd = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "model.FirstName",
                LastName = "model.LastName",
                Mail = "model.Mail",
                PhoneNumber = "model.PhoneNumber",
                Function = "model.Function",
                Roles = UserRoles.User,
                PasswordHash = "model.Password"
            };

            _userRepository.AddUser(userToAdd);

            drink.UserId = userToAdd.Id;
            return _drinkRepository.AddCoffe(drink);
        }

        public Task<Drink> GetLatestCoffe(Drink drink)
        {
            return _drinkRepository.GetLatestCoffe(drink);
        }
    }


}
