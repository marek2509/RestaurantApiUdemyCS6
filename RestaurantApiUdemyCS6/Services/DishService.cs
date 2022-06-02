using AutoMapper;
using RestaurantApiUdemyCS6.Entities;
using RestaurantApiUdemyCS6.Exceptions;
using RestaurantApiUdemyCS6.Models;

namespace RestaurantApiUdemyCS6.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto dto);
    }

    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = _dbContext.Restaurants
                .FirstOrDefault(r => r.Id == restaurantId);

            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            var dish = _mapper.Map<Dish>(dto);

            dish.RestaurantId = restaurantId;

            _dbContext.Dishes.Add(dish);
            _dbContext.SaveChanges();

            return dish.Id;
        }
    }
}
