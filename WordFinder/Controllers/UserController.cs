using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WordFinder_Business;
using WordFinder_Domain.Models;
using WordFinder_Domain.ServiceIO;
using WordFinder_Repository;

namespace WordFinder.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public UserController(UserService service, IConfiguration configuration, IMapper mapper)
        {
            _service = service;
            _config = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if(!_service.EmailIsUnique(user))
                return BadRequest("User with the same email address already exists");
            if(!_service.LoginIsUnique(user))
                return BadRequest("User with the same login already exists");
            
            var addedUser = _service.Register(user);
            var token = _service.CreateTokenForUser(addedUser);

            UserDTO mappedUser = _mapper.Map<User, UserDTO>(addedUser);
            HttpContext.Response.Headers["x-token"] = token;
            
            return Ok(mappedUser);
        }
        
        [HttpPost("login")]
        public ActionResult Login(SignInCredentials credentials)
        {
            User user = _service.GetUserByCredentials(credentials);
            if (user != null)
            {
                var token = _service.CreateTokenForUser(user);
                HttpContext.Response.Headers["x-token"] = token;
                UserDTO mappedUser = _mapper.Map<User, UserDTO>(user);
                return Ok(mappedUser);
            }

            return BadRequest("Invalid login or password");
        }

        [Authorize]
        [HttpGet]
        public ActionResult LoginWithToken()
        {
            var user = _service.GetUserByToken(getToken());
            if (user != null)
            {
                var mappedUser = _mapper.Map<User, UserDTO>(user);
                return Ok(mappedUser);
            }
            return BadRequest("Invalid token");
        }

        private string getToken()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return token;
        }
    }
}