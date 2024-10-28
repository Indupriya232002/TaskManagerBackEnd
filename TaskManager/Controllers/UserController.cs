using BusinessLayer.Services;
using EntityLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(Login login)
        {
            var token = _userService.Login(login);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Invalid Username or Password");
            }
            return Ok(new { token });
        }

        [Route("SignUp")]
        [HttpPost]
        public IActionResult SignUp(Users users)
        {
            return Ok(new {message = _userService.SignUp(users) });
        }


        [Route("GetUsers")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var result = _userService.GetAllUsers();
            return Ok(result);
        }

    }
}
