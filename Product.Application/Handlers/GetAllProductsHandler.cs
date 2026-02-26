using Product.Domain.Entities;
using Product.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Product.Application.Handlers
{
    public class GetAllProductsHandler
    {
        private readonly ProductRepository _repo;
        private readonly IMemoryCache _cache;

        public GetAllProductsHandler(ProductRepository repo, IMemoryCache cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<IEnumerable<ProductItem>> Handle()
        {
            var cacheKey = "products_all";

            if (_cache.TryGetValue(cacheKey, out IEnumerable<ProductItem> products))
            {
                return products!;
            }

            products = await _repo.GetAllAsync();

            _cache.Set(cacheKey, products, TimeSpan.FromMinutes(5));

            return products;
        }
    }
}
