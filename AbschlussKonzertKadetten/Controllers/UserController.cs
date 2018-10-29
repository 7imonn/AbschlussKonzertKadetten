using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Entity;
using AbschlussKonzertKadetten.Models;
using AbschlussKonzertKadetten.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbschlussKonzertKadetten.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/authenticate")]
    public class UsersController : ControllerBase
    {
        private IUserRepo _user;

        public UsersController(IUserRepo user)
        {
            _user = user;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate(ViewModelUser userModel)
        {
            var user = await _user.Authenticate(userModel.username, userModel.pw);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _user.GetAll();
            return Ok(users);
        }
    }
}
