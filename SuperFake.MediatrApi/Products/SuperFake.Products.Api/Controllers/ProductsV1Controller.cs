using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperFake.Products.Data;
using SuperFake.Products.Domain;
using System.Threading.Tasks;

namespace SuperFake.Products.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsV1Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsV1Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _mediator.Send(new GetProductDetailsV1Query { ProductID = id });

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ID)
            {
                return BadRequest();
            }

            await _mediator.Send(new UpdateProductV1Command { Product = product });

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await _mediator.Send(new CreateProductV1Command { Product = product });

            return CreatedAtAction("GetProduct", new { id = product.ID }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _mediator.Send(new DeleteProductV1Command { ProductID = id });

            return NoContent();
        }
    }
}
