using RestaurantApiUdemyCS6.Entities;
using RestaurantApiUdemyCS6.Models;

namespace RestaurantApiUdemyCS6.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
    }

    public class AccountService : IAccountService
    {
        private RestaurantDbContext _dbContext;

        public AccountService(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId,
            };
            
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }
    }
}
