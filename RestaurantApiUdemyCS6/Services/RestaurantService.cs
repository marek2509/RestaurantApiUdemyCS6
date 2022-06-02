using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantApiUdemyCS6.Entities;
using RestaurantApiUdemyCS6.Exceptions;
using RestaurantApiUdemyCS6.Models;

namespace RestaurantApiUdemyCS6.Services
{
    public interface IRestaurantService
    {
        RestaurantDto GetById(int id);
        IEnumerable<RestaurantDto> GetAll();
        int Create(CreateRestaurantDto dto);
        void Delete(int id);
        void Update(int id, UpdateRestaurantDto dto);
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

        public void Update(int id, UpdateRestaurantDto dto)
        {
            var restaurantToUpdate = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurantToUpdate is null)
                throw new NotFoundException("Restaurant not found");

            restaurantToUpdate.Name = dto.Name;
            restaurantToUpdate.Description= dto.Description;
            restaurantToUpdate.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _logger.LogError($"Restaurant with id: {id} DELETE action invoked");

            var restaurant = _dbContext
                  .Restaurants
                  .FirstOrDefault(r => r.Id == id);
            
            if(restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
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
                throw new NotFoundException("Restaurant not found");
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
