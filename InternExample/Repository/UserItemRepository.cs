using InternExample.Data;
using InternExample.Entity;
using Microsoft.EntityFrameworkCore;

namespace InternExample.Repository
{
    public class UserItemRepository
    {
        private readonly DataContext _context;
        public UserItemRepository(DataContext context)
        {
            _context = context;
        }

        //-----------------------------------------------------------------------------------------------------------------------------
        public async Task<List<UserItem>> GetAllUserItemsAsync()
        {
            return await _context.UserItems.ToListAsync();
        }
        //-----------------------------------------------------------------------------------------------------------------------------

        public async Task<UserItem> GetUserItemsByIdAsync(int Id) 
        {
            return await _context.UserItems.FindAsync(Id);
        }

        //-----------------------------------------------------------------------------------------------------------------------------

        public async Task<UserItem> AddUserItemAsync(UserItem userItem)
        {
            _context.UserItems.Add(userItem);
            await _context.SaveChangesAsync();
            return userItem;
        }

        //-----------------------------------------------------------------------------------------------------------------------------

        public async Task UpdateUserItemAsync(UserItem userItem)
        {
            _context.UserItems.Update(userItem);
            await _context.SaveChangesAsync();
        }
        //-----------------------------------------------------------------------------------------------------------------------------

        public async Task<UserItem> DeleteUserItemAsync(int Id)
        {
            var userItem = await _context.UserItems.FindAsync(Id);
            if (userItem != null)
            {
                _context.UserItems.Remove(userItem);
                await _context.SaveChangesAsync();
            }
            return userItem;
        }
    }
}