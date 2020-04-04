using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WordFinder_Domain.Models;

namespace WordFinder.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DbContext _context;
        private readonly IConfiguration _config;

        public AccountController(DbContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [HttpPost("[action]")]
        public ActionResult CreateUser(User user)
        {
            var createdUser = _context.Set<User>().Add(user);
            _context.SaveChanges();
            return Ok(createdUser.Entity);
        }
    }
}