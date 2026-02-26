using Cart.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Infrastructure.Repository
{
    public interface ICartRepository
    {
        public Task AddAsync(CartItem item);
        public  Task<List<CartItem>> GetUserCartAsync(int userId);
        public  Task RemoveAsync(int id);
        public Task ClearAsync(int userId);
        public Task UpdateAsync(CartItem item);
    }
}
