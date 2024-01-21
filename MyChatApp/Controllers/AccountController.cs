using Microsoft.AspNetCore.Mvc;
using MyChatApp.Models;
using MyChatApp.Models.Dto;
using MyChatApp.Servieses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyChatApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices accountServices;

        public AccountController(IAccountServices accountServices)
        {
            this.accountServices = accountServices;
        }
        // GET: api/<AccountController>
        [HttpPost]
        public ActionResult<User> LogIn([FromBody] UserLogInDto user)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var DBuserDetails = accountServices.LogIn(user);
            if (DBuserDetails.IsCompleted)
            {
                var DbUser = accountServices.GetUserById(DBuserDetails.Result.UserId);
                if (DbUser != null) return Ok(DbUser);
            }
            return NotFound(DBuserDetails);
        }

        
        [HttpPost]
        public ActionResult<User> SignUp([FromBody] UserLogInDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var DBuserDetails = accountServices.SignUp(user);
            if (DBuserDetails.IsCompleted)
            {
                var DbUser = accountServices.GetUserById(DBuserDetails.Result.UserId);
                if (DbUser != null) return Ok(DbUser);
            }
            return BadRequest(DBuserDetails);
        }

    }
}
