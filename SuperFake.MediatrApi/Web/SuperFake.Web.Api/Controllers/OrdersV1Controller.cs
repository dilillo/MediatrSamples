using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperFake.Web.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperFake.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersV1Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersV1Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/OrdersV1
        public async Task<ActionResult<IEnumerable<GetAllOrdersV1QueryResult>>> GetOrders()
        {
            return await _mediator.Send(new GetAllOrdersV1Query());
        }

        // GET: api/OrdersV1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderDetailsV1QueryResult>> GetOrder(int id)
        {
            var orderData = await _mediator.Send(new GetOrderDetailsV1Query { OrderID = id });

            if (orderData == null)
            {
                return NotFound();
            }

            return orderData;
        }
    }
}
