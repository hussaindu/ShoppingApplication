using Cart.Application.Commands;
using Cart.Application.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cart_Controller : ControllerBase
    {
        private readonly AddToCartHandler _add;
        private readonly GetCartHandler _get;

        public Cart_Controller(AddToCartHandler add, GetCartHandler get)
        {
            _add = add;
            _get = get;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddToCartCommand cmd)
        {
            await _add.Handle(cmd);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _get.Handle());
    }
}
