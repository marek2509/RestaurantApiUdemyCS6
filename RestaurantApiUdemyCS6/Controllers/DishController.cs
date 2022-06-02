using Microsoft.AspNetCore.Mvc;
using RestaurantApiUdemyCS6.Models;
using RestaurantApiUdemyCS6.Services;

namespace RestaurantApiUdemyCS6.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int restaurantId, [FromBody]CreateDishDto dto)
        {
            var dishId = _dishService.Create(restaurantId, dto);

            return Created($"api/{restaurantId}/dish/{dishId}", null);
        } 
    }
}
