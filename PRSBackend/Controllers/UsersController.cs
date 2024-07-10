using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRSBackend.Data;
using PRSBackend.Models;

namespace PRSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PRSBackendContext _context;

        public UsersController(PRSBackendContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
 //GET current user: api/Users/{username}/{password}
        [HttpGet("{Username}/{Password}")]
        public async Task<ActionResult<User>> Login(string userName, string password)
        {

            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == userName && x.Password == password);
            
            if(user is null)
            {
                return NotFound();
            }
            return user; //user with that username and passowrd is found
            
 //if user is found, store in service instance available to all components. use this to set the user id to the Id pk when requests are created.
            
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
            public async Task<IActionResult> PutUser(int id, User user)
            {
                if (id != user.Id)
                {
                    return BadRequest();
                }

                _context.Entry(user).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
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
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
