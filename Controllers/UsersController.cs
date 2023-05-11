using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_CommerceAPI.Context;
using E_CommerceAPI.Models;
using Microsoft.AspNetCore.Authentication;

namespace E_CommerceAPI.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersContext _context;

        public UsersController(UsersContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> Login(Login login)
        {

            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FromSqlInterpolated($"sp_get_validate_login {login.Email},{login.Password}").ToListAsync();

            if (user.Count > 0)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<User>> Register(UserRegister user)
        {

            if (_context.Users == null)
            {
                return NotFound();
            }

            var userRegister = await _context.Users.FromSqlInterpolated($"sp_insert_user {user.FullName},{user.Email},{user.Password}").ToListAsync();

            if (userRegister.Count > 0)
            {
                return Ok(userRegister);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.IdUser == id)).GetValueOrDefault();
        }
    }
}
