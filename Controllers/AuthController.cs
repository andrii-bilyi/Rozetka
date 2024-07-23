using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Rozetka.Data.Entity;
using Rozetka.Services;
using Rozetka.Services.Hash;

namespace Rozetka.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHashService _hashService;

        public AuthController(IUserService userService, IHashService hashService)
        {
            _userService = userService;
            _hashService = hashService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
           // Guid.TryParse(id, out Guid userGuid);
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("login")]
        public async Task<ActionResult<User>> GetUser([FromQuery] string email, [FromQuery] string password)
        {
            var user = await _userService.GetUserByEmailAsync(email);

            if (user == null)
            {
                return Unauthorized(); //401
                //return NotFound(); //404
            }
            else
            {
                //String dk = _hashService.HexString(user.PasswordSalt + password);
                if (user.PasswordHash != password)
                {
                    //HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    return Unauthorized(); //401
                }
                //if (user.IsSuspended)
                //{
                //    // Возвращаем 410 для приостановленного аккаунта
                //    return StatusCode(StatusCodes.Status410Gone);
                //}
            }
            //зберігаємо у сесії факт успішної автентифікації
            HttpContext.Session.SetString("AuthUserId", user.Id.ToString());
            return Ok(user); //200
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var createdUser = await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (!result)
            {
                return NotFound();
            }
            HttpContext.Session.Remove("AuthUserId");
            return NoContent();
        }
    }
}
