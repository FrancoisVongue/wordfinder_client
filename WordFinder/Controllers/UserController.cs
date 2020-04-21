using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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

            var addedUser = _service.Register(user);
            var token = _service.CreateTokenForUser(addedUser);

            UserInfo mappedUser = _mapper.Map<User, UserInfo>(addedUser);
            HttpContext.Response.Headers["x-token"] = token;
            
            return Ok(mappedUser);
        }
        
        // [HttpPost("[action]")]
        // public ActionResult SignIn(SignInCredentials credentials)
        // {
        //     var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        //     
        //     User user = _service.GetUserByCredntials(credentials);
        //     if (user == null)
        //     {
        //         return BadRequest();
        //     }
        //     else
        //     {
        //         HttpContext.Response.Headers["x-token"] = token;
        //         return Ok();
        //     }
        // }
        //
        // [Authorize]
        // [HttpPost("[action]")]
        // public ActionResult Verify()
        // {
        //     var user = _service.GetUser(HttpContext);
        //     if (user != null)
        //     {
        //         return Ok(user);
        //     }
        //     else
        //         return BadRequest();
        // }
    }
}