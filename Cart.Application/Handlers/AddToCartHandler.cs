using Cart.Application.Commands;
using Cart.Domain;
using Cart.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Handlers
{
    public class AddToCartHandler
    {
        private readonly CartRepository _repo;
        private readonly IHttpContextAccessor _http;

        public AddToCartHandler(CartRepository repo, IHttpContextAccessor http)
        {
            _repo = repo;
            _http = http;
        }

        public async Task Handle(AddToCartCommand cmd)
        {
            var claim = _http.HttpContext?.User?.FindFirst("userId");

            if (claim == null)
                throw new UnauthorizedAccessException("User not authenticated");

            var userId = int.Parse(claim.Value);

            var existingItems = await _repo.GetUserCartAsync(userId);

            var item = existingItems.FirstOrDefault(x => x.ProductId == cmd.ProductId);

            if (item == null)
            {
                // New product → insert
                await _repo.AddAsync(new CartItem
                {
                    ProductId = cmd.ProductId,
                    ProductName = cmd.ProductName,
                    Price = cmd.Price,
                    Quantity = cmd.Quantity,
                    UserId = userId
                });
            }
            else
            {
                // Existing product → increase quantity
                item.Quantity += cmd.Quantity;
                await _repo.UpdateAsync(item);
            }
        }
    }
}
