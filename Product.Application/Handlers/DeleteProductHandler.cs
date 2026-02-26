using Microsoft.Extensions.Caching.Memory;
using Product.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Handlers
{
    public class DeleteProductHandler
    {
        private readonly ProductRepository _repo;
        private readonly IMemoryCache _cache;

        public DeleteProductHandler(ProductRepository repo, IMemoryCache cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task Handle(int id)
        {
            var product = await _repo.GetByIdAsync(id)
                ?? throw new Exception("Product not found");

            await _repo.DeleteAsync(product);
            _cache.Remove("products_all");
        }
        
    }
}
