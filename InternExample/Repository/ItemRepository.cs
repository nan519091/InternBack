using InternExample.Data;
using InternExample.Entity;
using Microsoft.EntityFrameworkCore;

namespace InternExample.Repository
{
    public class ItemRepository
    {
        private readonly DataContext _context;
        public ItemRepository(DataContext context)
        {
            _context = context;
        }

        //-----------------------------------------------------------------------------------------------------------------------------
        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }
        //-----------------------------------------------------------------------------------------------------------------------------

        public async Task<Item> GetItemByIdAsync(int Id)
        {
            return await _context.Items.FindAsync(Id);
        }

        //-----------------------------------------------------------------------------------------------------------------------------
        public async Task AddItemAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        //-----------------------------------------------------------------------------------------------------------------------------
        public async Task UpdateItemAsync(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        //-----------------------------------------------------------------------------------------------------------------------------
        public async Task<Item> DeleteItemAsync(int Id)
        {
            var item = await _context.Items.FindAsync(Id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            return item;
        }
    }
}
