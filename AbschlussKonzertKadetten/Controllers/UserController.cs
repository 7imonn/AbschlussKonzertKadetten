//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AbschlussKonzertKadetten.Entity;
//using AbschlussKonzertKadetten.Repository;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace AbschlussKonzertKadetten.Controllers
//{
//    [Authorize]
//    [ApiController]
//    [Route("[controller]")]
//    public class UsersController : ControllerBase
//    {
//        private IUserService _userService;

//        public UsersController(IUserService userService)
//        {
//            _userService = userService;
//        }

//        [AllowAnonymous]
//        [HttpPost("authenticate")]
//        public async Task<IActionResult> Authenticate([FromBody]User userParam)
//        {
//            var user = await _userService.Authenticate(userParam.Username, userParam.Password);

//            if (user == null)
//                return BadRequest(new { message = "Username or password is incorrect" });

//            return Ok(user);
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var users = await _userService.GetAll();
//            return Ok(users);
//        }
//    }
//}
