using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantApiUdemyCS6.Entities;
using RestaurantApiUdemyCS6.Models;

namespace RestaurantApiUdemyCS6.Services
{
    public interface IRestaurantService
    {
        RestaurantDto GetById(int id);
        IEnumerable<RestaurantDto> GetAll();
        int Create(CreateRestaurantDto dto);
        bool Delete(int id);
        bool Update(int id, UpdateRestaurantDto dto);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public bool Update(int id, UpdateRestaurantDto dto)
        {
            var restaurantToUpdate = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurantToUpdate is null) return false;

            restaurantToUpdate.Name = dto.Name;
            restaurantToUpdate.Description= dto.Description;
            restaurantToUpdate.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            _logger.LogError($"Restaurant with id: {id} DELETE action invoked");

            var restaurant = _dbContext
                  .Restaurants
                  .FirstOrDefault(r => r.Id == id);
            
            if(restaurant == null)
            {
                return false;
            }

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();

            return true;
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
                   .Restaurants
                   .Include(r => r.Address)
                   .Include(r => r.Dishes)
                   .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
            {
                return null;
            }

            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext
                     .Restaurants
                     .Include(r => r.Address)
                     .Include(r => r.Dishes)
                     .ToList();

            var restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            return restaurantDtos;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }
    }
}
