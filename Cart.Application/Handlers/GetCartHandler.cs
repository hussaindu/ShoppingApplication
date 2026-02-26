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
    public class GetCartHandler
    {
        private readonly CartRepository _repo;
        private readonly IHttpContextAccessor _http;

        public GetCartHandler(CartRepository repo, IHttpContextAccessor http)
        {
            _repo = repo;
            _http = http;
        }

        public async Task<List<CartItem>> Handle()
        {
            var claim = _http.HttpContext?.User?.FindFirst("userId");

            if (claim == null)
                throw new UnauthorizedAccessException("User not authenticated");

            var userId = int.Parse(claim.Value);
            return await _repo.GetUserCartAsync(userId);
        }
    }
}
