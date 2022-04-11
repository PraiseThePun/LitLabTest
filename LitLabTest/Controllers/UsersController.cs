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
            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            var users = userManagement.AddUser(user);
            return Ok(users);
        }

        // DELETE: api/Delete/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            var users = userManagement.DeleteUser(id);
            return Ok(users);
        }

        // PUT: api/Users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(string id, [FromBody] User user)
        {
            var users = userManagement.UpdateUser(id, user);
            return Ok(users);
        }
    }
}
