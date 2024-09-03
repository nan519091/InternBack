using InternExample.Entity;
using InternExample.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserItemController(UserItemRepository userItemRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<UserItem>>> GetAllUserItem()
        {
            var userItem = await userItemRepository.GetAllUserItemsAsync();
            return Ok(userItem);
        }
        //-----------------------------------------------------------------------------------------------------------------------------

        [HttpGet("{Id}")]
        public async Task<ActionResult<UserItem>> GetUserItemById(int Id)
        {
            var userItem = await userItemRepository.GetUserItemsByIdAsync(Id);
            if (userItem == null)
            {
                return NotFound("Useritem not found.");
            }
            return Ok(userItem);
        }
        //-----------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public async Task<ActionResult<UserItem>> AddUserItem(UserItem userItem)
        {
            await userItemRepository.AddUserItemAsync(userItem);
            return Ok(userItem);
        }

        //-----------------------------------------------------------------------------------------------------------------------------

        [HttpPut("{Id}")]
        public async Task<ActionResult<UserItem>> UpdateUser(int id, [FromBody] UserItem updatedUserItem)
        {
            var dbUi = await userItemRepository.GetUserItemsByIdAsync(updatedUserItem.Id);
            if (dbUi == null)
            {
                return NotFound("Item not found.");
            }
            dbUi.Id = updatedUserItem.Id;
            await userItemRepository.UpdateUserItemAsync(dbUi);
            return Ok(dbUi);
        }

        //-----------------------------------------------------------------------------------------------------------------------------

        [HttpDelete("{Id}")]
        public async Task<ActionResult<UserItem>> DeleteUserItem(int Id) 
        {
            var dbUi = await userItemRepository.GetUserItemsByIdAsync(Id);
            if (dbUi == null) 
            {
                return NotFound("Useritem not found.");
            }
            await userItemRepository.DeleteUserItemAsync(Id);

            return Ok("Data has been deleted successfully.");
        }

    }
}
