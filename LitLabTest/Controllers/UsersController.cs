using LitLabKata.Models;
using LitLabKata.Services;
using Microsoft.AspNetCore.Mvc;

namespace LitLabTest.Controllers
{
    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUserManagementService userManagement;

        public UsersController(IUserManagementService userManagment)
        {
            userManagement = userManagment;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = userManagement.GetAllUsers();
            return Ok(users);
        }

        // GET: api/Users/{id}
        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetUserByNick(string id)
        {
            var user = userManagement.GetUserByNick(id);

            if(user == null)
                return NotFound($"User {id} not found.");

            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            try
            {
                var users = userManagement.AddUser(user);
                return Ok(users);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Delete/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            var users = userManagement.DeleteUser(id);

            if (users == null)
                return NotFound(id);

            return Ok(users);
        }

        // PUT: api/Users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(string id, [FromBody] User user)
        {
            try
            {
                var users = userManagement.UpdateUser(id, user);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
