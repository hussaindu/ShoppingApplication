using Cart.Domain;
using Cart.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Infrastructure.Repository
{
    public class CartRepository: ICartRepository
    {
        private readonly CartDbContext _db;

        public CartRepository(CartDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(CartItem item)
        {
            _db.CartItems.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task<List<CartItem>> GetUserCartAsync(int userId)
            => await _db.CartItems.Where(x => x.UserId == userId).ToListAsync();

        public async Task RemoveAsync(int id)
        {
            var item = await _db.CartItems.FindAsync(id);
            if (item != null)
            {
                _db.CartItems.Remove(item);
                await _db.SaveChangesAsync();
            }
        }

        public async Task ClearAsync(int userId)
        {
            var items = _db.CartItems.Where(x => x.UserId == userId);
            _db.CartItems.RemoveRange(items);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateAsync(CartItem item)
        {
            _db.CartItems.Update(item);
            await _db.SaveChangesAsync();
        }
    }
}
