using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApiUdemyCS6.Entities;
using RestaurantApiUdemyCS6.Models;
using RestaurantApiUdemyCS6.Services;
using System.Security.Claims;

namespace RestaurantApiUdemyCS6.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize] // jeśli takie zapytanie nie będzie zawierać nagłówka autoryzacji to dostaniemy  kod 401 automatycznie
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute]int id, [FromBody]UpdateRestaurantDto dto)
        {
           _restaurantService.Update(id, dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
           _restaurantService.Delete(id);

            return NoContent();
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
         
           var id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpGet]
        [Authorize(Policy = "HasMinimum2Restaurant")] // musi się pokrywać z nazwą zadeklarowaną w klasie program
        public ActionResult<IEnumerable<RestaurantDto>> GetAll([FromQuery]string? searchPhrase)
        {
            var restaurantDtos = _restaurantService.GetAll(searchPhrase);

            return Ok(restaurantDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // gdy mamy [Authorize] nadany wyżej to tu można go dezaktywować dla wybranej metody
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);

            return Ok(restaurant);
        }
    }
}
