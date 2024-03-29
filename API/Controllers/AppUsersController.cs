using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
//by using JWT
//After applying the attribute we should add services in program.cs
[Authorize]
    public class AppUsersController : BaseAPIController
    {

        private readonly AppUserDb _context;

        //whenever we inject anything(DBCONTEXT CLASS) we use constructor in controller
        public AppUsersController(AppUserDb context)
        {
            _context = context;
        }
        // [AllowAnonymous] it helps to access any user
       
        [HttpGet]
        //here we use IEnumerable because we return all the data in list from a generic class AppUser
        //we use ActionResult because some return types like (return Badrequest) will works. And for multiple return type we use IActionResult
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _context.AppUsers.ToListAsync();
            return users;

        }

        [HttpGet("{id}")]
        //here we want single value thats why we not use IEnumerable
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await _context.AppUsers.FindAsync(id);
            return user;
        }
    }
}