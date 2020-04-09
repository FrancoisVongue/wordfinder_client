using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WordFinder_Business;
using WordFinder_Domain.Models;
using WordFinder_Repository;

namespace WordFinder.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        private readonly IConfiguration _config;

        public UserController(UserService service, IConfiguration configuration)
        {
            _service = service;
            _config = configuration;
        }

        [HttpPost("[action]")]
        public ActionResult Register(User user)
        {
            if(!ModelState.IsValid) 
                return new BadRequestResult();

            var addedUser = _service.Register(user);
            var token = _service.GenerateToken(addedUser);
            HttpContext.Response.Headers["x-token"] = token;

            return Ok(addedUser);
        }
        
        [HttpPost("[action]")]
        public ActionResult SignIn(User user)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpPost("[action]")]
        public ActionResult Verify(string login)
        {
            var user = _service.GetByLogin(login);
            if (user != null)
                return Ok(user);
            else 
                return new UnauthorizedResult();
        }
    }
}