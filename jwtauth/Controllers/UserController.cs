using jwtauth.Models;
using jwtauth.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace jwtauth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IJWTManagerRepository jWTManagerRepository;

        public UserController(IJWTManagerRepository JWTManagerRepository)
        {
            this.jWTManagerRepository = JWTManagerRepository;
        }

        [HttpGet]
        [Route("userlist")]
        public List<string> Get()
        {
            var users = new List<string>()
            {
                "Nazmul",
                "Alom",
                "Nafis"
            };
            return users;

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("authinticate")]
        public IActionResult Authenticate(Users userdata)
        {
            var token = jWTManagerRepository.Authenticate(userdata);

            if(token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
