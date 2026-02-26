using Microsoft.Extensions.Caching.Memory;
using Product.Application.Commands;
using Product.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Handlers
{
    public class UpdateProductHandler
    {
        private readonly ProductRepository _repo;
        private readonly IMemoryCache _cache;

        public UpdateProductHandler(ProductRepository repo, IMemoryCache cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task Handle(UpdateProductCommand cmd)
        {
            var product = await _repo.GetByIdAsync(cmd.Id)
                ?? throw new Exception("Product not found");

            product.Name = cmd.Name;
            product.Type = cmd.Type;
            product.Price = cmd.Price;
            product.PhotoUrl = cmd.PhotoUrl;

            await _repo.UpdateAsync(product);
            _cache.Remove("products_all");
        }
    }
}
