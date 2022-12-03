using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserIdentityStore.API.DataLayer.Models;
using UserIdentityStore.API.Repositories;

namespace UserIdentityStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStoreController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public UserStoreController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> UserRegistration([FromBody] RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepo.UserRegister(registerModel);
                if (result.Status == "Error")
                {
                    return StatusCode(result.StatusCode, result);
                }
                else
                {
                    return Ok(result);
                }
            }
            return BadRequest();
            
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> AdminRegistration([FromBody] RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepo.AdminRegister(registerModel);
                if (result.Status == "Error")
                {
                    return StatusCode(result.StatusCode, result);
                }
                else
                {
                    return Ok(result);
                }
            }
            return BadRequest();

        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> UserLogin([FromBody] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepo.UserLogin(loginModel);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
