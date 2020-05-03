using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperFake.Orders.Data;
using SuperFake.Orders.Domain;
using System.Threading.Tasks;

namespace SuperFake.Orders.Api.Controllers
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

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _mediator.Send(new GetOrderDetailsV1Query { OrderID = id });

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.ID)
            {
                return BadRequest();
            }

            await _mediator.Send(new UpdateOrderV1Command { Order = order });

            return NoContent();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            await _mediator.Send(new CreateOrderV1Command { Order = order });

            return CreatedAtAction("GetOrder", new { id = order.ID }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            await _mediator.Send(new DeleteOrderV1Command { OrderID = id });

            return NoContent();
        }
    }
}
