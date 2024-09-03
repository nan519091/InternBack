using InternExample.Data;
using InternExample.Entity;
using InternExample.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserRepository userRepository) : ControllerBase
    {
        /* private readonly UserRepository _userRepository;
           public UserController(UserRepository userRepository)
           {
               _userRepository = userRepository;
           }อันนี้ยาว*/ 
        //-----------------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUser()
        {
            var user = await userRepository.GetAllUsersAsync();
            return Ok(user);
        }
       
        //-----------------------------------------------------------------------------------------------------------------------------

        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> GetUserById(int Id)
        {
            var user = await userRepository.GetUserByIdAsync(Id);
            if (user == null)
            {
                return NotFound("User not found."); //status code 404
            }
            return Ok(user);
        }

        //-----------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(User user)
        {
            await userRepository.AddUserAsync(user);
            return Ok(user);
        }

        //-----------------------------------------------------------------------------------------------------------------------------

        [HttpPut("{Id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User updatedUserModel)
        {
            var dbUser = await userRepository.GetUserByIdAsync(updatedUserModel.Id);
            if (dbUser == null)
            {
                return NotFound("User not found.");
            }
            dbUser.Name = updatedUserModel.Name;
            await userRepository.UpdateUserAsync(dbUser);

            return Ok(dbUser);
        }

        //-----------------------------------------------------------------------------------------------------------------------------

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int Id)
        {
            var dbUser = await userRepository.GetUserByIdAsync(Id);
            if (dbUser == null)
            {
                return NotFound("User not found.");
            }
            await userRepository.DeleteUserAsync(Id);

            return Ok("Data has been deleted successfully.");
        }

    }
}
