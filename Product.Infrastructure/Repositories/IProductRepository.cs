using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repositories
{
    public interface IProductRepository
    {
        public  Task<List<ProductItem>> GetAllAsync();
        public  Task<ProductItem?> GetByIdAsync(int id);
        public  Task AddAsync(ProductItem product);
        public  Task UpdateAsync(ProductItem product);
        public  Task DeleteAsync(ProductItem product);



    }
}
