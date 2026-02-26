//using Auth.Application.Handlers;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Auth.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize(Roles = "Admin")]
//    public class AdminUsersController : ControllerBase
//    {
//        private readonly GetAllUsersHandler _getAll;
//        private readonly UpdateUserHandler _update;
//        private readonly DeleteUserHandler _delete;

//        public AdminUsersController(
//            GetAllUsersHandler getAll,
//            UpdateUserHandler update,
//            DeleteUserHandler delete)
//        {
//            _getAll = getAll;
//            _update = update;
//            _delete = delete;
//        }

//        // GET: api/admin/users
//        [HttpGet]
//        public async Task<IActionResult> GetUsers()
//        {
//            return Ok(await _getAll.Handle());
//        }

//        // PUT: api/admin/users/{id}
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateRole(int id, [FromBody] string role)
//        {
//            await _update.Handle(id, role);
//            return Ok("User updated");
//        }

//        // DELETE: api/admin/users/{id}
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteUser(int id)
//        {
//            await _delete.Handle(id);
//            return Ok("User deleted");
//        }
//    }
//}
