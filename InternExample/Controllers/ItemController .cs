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
    public class ItemController(ItemRepository itemRepository) : ControllerBase
    {
       /* private readonly ItemRepository _itemRepository;
        public ItemController(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }*/
        //-----------------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAllItem()
        {
            var item = await itemRepository.GetAllItemsAsync();
            return Ok(item);
        }
       
        //-----------------------------------------------------------------------------------------------------------------------------

        [HttpGet("{Id}")]
        public async Task<ActionResult<Item>> GetItemById(int Id)
        {
            var item = await itemRepository.GetItemByIdAsync(Id);
            if (item == null)
            {
                return NotFound("Item not found.");
            }
            return Ok(item);
        }

        //-----------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public async Task<ActionResult<List<Item>>> AddItem(Item item)
        {
            await itemRepository.AddItemAsync(item);
            return Ok(item);
        }

        //-----------------------------------------------------------------------------------------------------------------------------

        [HttpPut("{Id}")]
        public async Task<ActionResult<Item>> UpdateItem(int id, [FromBody] Item updatedItemModel)
        {
            var dbItem = await itemRepository.GetItemByIdAsync(updatedItemModel.Id);
            if (dbItem == null)
            {
                return NotFound("Item not found.");
            }

            // ใช้ Mapster หรือวิธีการอื่น ๆ เพื่อทำการแมปข้อมูลจาก UserModel ไปยัง User
            dbItem.Name = updatedItemModel.Name;
            // ทำการอัปเดตฟิลด์อื่น ๆ ตามที่ต้องการ

            await itemRepository.UpdateItemAsync(dbItem);

            return Ok(dbItem);
        }

        //-----------------------------------------------------------------------------------------------------------------------------

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<Item>>> DeleteItem(int Id)
        {
            var dbItem = await itemRepository.GetItemByIdAsync(Id);
            if (dbItem == null)
            {
                return NotFound("Item not found.");
            }
            await itemRepository.DeleteItemAsync(Id);

            return Ok("Data has been deleted successfully.");
        }

    }
}
