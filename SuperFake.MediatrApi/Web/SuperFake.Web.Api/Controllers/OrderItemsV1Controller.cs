using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperFake.Web.Domain;
using System.Threading.Tasks;

namespace SuperFake.Web.Api.Controllers
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
        public async Task<ActionResult<GetOrderItemDetailsV1QueryResult>> GetOrderItem(int id)
        {
            var orderItemData = await _mediator.Send(new GetOrderItemDetailsV1Query { OrderItemID = id });

            if (orderItemData == null)
            {
                return NotFound();
            }

            return orderItemData;
        }
    }
}
