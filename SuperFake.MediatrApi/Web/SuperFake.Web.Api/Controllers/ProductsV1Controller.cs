using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperFake.Web.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperFake.Web.Api.Controllers
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

        // GET: api/ProductsV1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllProductsV1QueryResult>>> GetProducts()
        {
            return await _mediator.Send(new GetAllProductsV1Query());
        }

        // GET: api/ProductsV1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDetailsV1QueryResult>> GetProduct(int id)
        {
            var productData = await _mediator.Send(new GetProductDetailsV1Query { ProductID = id });

            if (productData == null)
            {
                return NotFound();
            }

            return productData;
        }
    }
}
