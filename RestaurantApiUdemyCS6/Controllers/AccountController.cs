using Microsoft.AspNetCore.Mvc;
using RestaurantApiUdemyCS6.Models;
using RestaurantApiUdemyCS6.Services;

namespace RestaurantApiUdemyCS6.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private IAccountService _service;
        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _service.RegisterUser(dto);
            return Ok();
        }
    }
}
