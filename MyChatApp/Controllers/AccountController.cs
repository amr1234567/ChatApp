using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MyChatApp.Models;
using MyChatApp.Models.Dto;
using MyChatApp.Servieses;
using System.Text.RegularExpressions;

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
        
        [HttpPost]
        [Route("/Login")]
        public async Task<ActionResult<User>> LogIn([FromBody] UserLogInDto user)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var DBuserDetails =await accountServices.LogIn(user);
            if (DBuserDetails.Succesed)
            {
            
                var DbUser =await accountServices.GetUserById(DBuserDetails.UserId);
                if (DbUser != null) 
                {
                    return Ok(new FrontEndResponse()
                    {
                        Id = DbUser.Id.ToString(),
                        Name = DbUser.Name,
                        isSuccess = true
                    }); 
                }
                return BadRequest(new FrontEndResponse()
                {
                    ErrorMessages = new List<string>() { "Can't find ur Account" }
                });
            }
            return NotFound(new FrontEndResponse()
            {
                ErrorMessages = DBuserDetails.messageDetails
            });
        }

        
        [Route("/SignUp")]
        [HttpPost]
        public async Task<ActionResult<User>> SignUp([FromBody] UserLogInDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var DBuserDetails =await accountServices.SignUp(user);
            if (DBuserDetails.Succesed)
            {
                var DbUser = await accountServices.GetUserById(DBuserDetails.UserId);
                if (DbUser != null) return Ok(DbUser);
            }
            return BadRequest(DBuserDetails);
        }

    }
}
