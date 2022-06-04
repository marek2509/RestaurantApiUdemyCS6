using Microsoft.AspNetCore.Mvc;
using RestaurantApiUdemyCS6.Models;
using RestaurantApiUdemyCS6.Services;

namespace RestaurantApiUdemyCS6.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private IAccountService _accountService;
        public AccountController(IAccountService service)
        {
            _accountService = service;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
