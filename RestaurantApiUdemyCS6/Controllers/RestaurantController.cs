using Microsoft.AspNetCore.Mvc;
using RestaurantApiUdemyCS6.Entities;

namespace RestaurantApiUdemyCS6.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : Controller
    {
        private readonly RestaurantDbContext _dbContext;

        public RestaurantController(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAll()
        {
            var restaurants = _dbContext.Restaurants.ToList();
            Console.WriteLine("ło kurde 1");
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public ActionResult<Restaurant> Get([FromRoute] int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);
            Console.WriteLine("ło kurde");
           
            if (restaurant is null)
            {
                return NotFound();
            }
  
            return Ok(restaurant);
        }
    }
}
