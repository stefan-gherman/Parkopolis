using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Parkopolis.API.Models;
using Parkopolis.WEB.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Parkopolis.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }



        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<TempUser> Get()
        {
            
            List<TempUser> allUsersTemp = new List<TempUser>();
            var allUsersFromUserManager = userManager.Users;
            foreach (var user in allUsersFromUserManager)
            {
                allUsersTemp.Add(new TempUser()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Rank = (int)user.Type
                });
            }
            return allUsersTemp;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public TempUser Get(string id)
        {
            TempUser resultUser = new TempUser();
            var allUsersFromUserManager = userManager.Users;
            foreach (var user in allUsersFromUserManager)
            {
                if (user.Id == id)
                {
                    resultUser.Id = user.Id;
                    resultUser.FirstName = user.FirstName;
                    resultUser.LastName = user.LastName;
                    resultUser.Rank = (int)user.Type;
                }
            }
            return resultUser;
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // POST api/<UsersController>
        [HttpPost("{id}")]
        [EnableCors("AllowAnyOrigin")]
        public async Task<IActionResult> Post(string id, [FromBody] TempUser tempUserFromRequest)
        {
            var user = userManager.FindByIdAsync(id).Result;

            user.Type = (UserType)tempUserFromRequest.Rank;

            await userManager.UpdateAsync(user);

            return NoContent();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        [EnableCors("AllowAnyOrigin")]
        public async Task<IActionResult> Put(string id, [FromBody] TempUser tempUserFromRequest)
        {
            
            var user = userManager.FindByIdAsync(id).Result;
            
            user.Type = (UserType)tempUserFromRequest.Rank;

            await userManager.UpdateAsync(user);

            return NoContent();
        }

            // DELETE api/<UsersController>/5
            [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
