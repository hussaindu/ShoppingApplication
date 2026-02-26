using Microsoft.Extensions.Caching.Memory;
using Product.Application.Commands;
using Product.Domain.Entities;
using Product.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Handlers
{
    public class CreateProductHandler
    {
        private readonly ProductRepository _repo;
        private readonly IMemoryCache _cache;

        public CreateProductHandler(ProductRepository repo, IMemoryCache cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task Handle(CreateProductCommand cmd)
        {
            await _repo.AddAsync(new ProductItem
            {
                Name = cmd.Name,
                Type = cmd.Type,
                Price = cmd.Price,
                PhotoUrl = cmd.PhotoUrl
            });
            _cache.Remove("products_all");
        }
    }
}
