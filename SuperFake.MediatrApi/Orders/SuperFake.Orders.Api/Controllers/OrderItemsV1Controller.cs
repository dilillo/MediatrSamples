using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperFake.Orders.Data;
using SuperFake.Orders.Domain;
using System.Threading.Tasks;

namespace SuperFake.Orders.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsV1Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderItemsV1Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/OrderItemsV1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItem(int id)
        {
            var orderItem = await _mediator.Send(new GetOrderItemDetailsV1Query { OrderItemID = id });

            if (orderItem == null)
            {
                return NotFound();
            }

            return orderItem;
        }

        // PUT: api/OrderItemsV1/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItem(int id, OrderItem orderItem)
        {
            if (id != orderItem.ID)
            {
                return BadRequest();
            }

            await _mediator.Send(new UpdateOrderItemV1Command { OrderItem = orderItem });

            return NoContent();
        }

        // POST: api/OrderItemsV1
        [HttpPost]
        public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItem orderItem)
        {
            await _mediator.Send(new CreateOrderItemV1Command { OrderItem = orderItem });

            return CreatedAtAction("GetOrderItem", new { id = orderItem.ID }, orderItem);
        }

        // DELETE: api/OrderItemsV1/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderItem(int id)
        {
            await _mediator.Send(new DeleteOrderItemV1Command { OrderItemID = id });

            return NoContent();
        }
    }
}
