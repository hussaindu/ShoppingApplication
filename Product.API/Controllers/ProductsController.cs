using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Commands;
using Product.Application.Handlers;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly GetAllProductsHandler _getAll;
        private readonly CreateProductHandler _create;
        private readonly UpdateProductHandler _update;
        private readonly DeleteProductHandler _delete;

        public ProductsController(
            GetAllProductsHandler getAll,
            CreateProductHandler create,
            UpdateProductHandler update,
            DeleteProductHandler delete)
        {
            _getAll = getAll;
            _create = create;
            _update = update;
            _delete = delete;
        }

        // USER — VIEW PRODUCTS
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProducts()
            => Ok(await _getAll.Handle());

        // ADMIN — CREATE
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand cmd)
        {
            await _create.Handle(cmd);
            return Ok();
        }

        // ADMIN — UPDATE
        //[Authorize(Roles = "Admin")]

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductCommand cmd)
        {
            await _update.Handle(cmd);
            return Ok();
        }

        // ADMIN — DELETE
        //[Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _delete.Handle(id);
            return Ok();
        }

        //[Authorize]
        //[HttpGet("debug")]
        //public IActionResult Debug()
        //{
        //    var claims = User.Claims.Select(c => new { c.Type, c.Value });
        //    return Ok(claims);
        //}
    }
}
