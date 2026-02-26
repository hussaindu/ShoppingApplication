using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repositories
{
    public  class ProductRepository: IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductItem>> GetAllAsync()
            => await _context.Products.ToListAsync();

        public async Task<ProductItem?> GetByIdAsync(int id)
            => await _context.Products.FindAsync(id);

        public async Task AddAsync(ProductItem product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductItem product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductItem product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
