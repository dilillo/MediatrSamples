using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperFake.Shared.Domain;
using System.Threading.Tasks;

namespace SuperFake.Products.Api.Controllers
{
    [ApiController]
    public class NotificationsV1Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationsV1Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/[controller]/" + nameof(OrderCreatedV1Notification))]
        public Task<ActionResult> Post([FromBody] OrderCreatedV1Notification value) => PublishNotification(value);

        [HttpPost]
        [Route("api/[controller]/" + nameof(OrderUpdatedV1Notification))]
        public Task<ActionResult> Post([FromBody] OrderUpdatedV1Notification value) => PublishNotification(value);

        [HttpPost]
        [Route("api/[controller]/" + nameof(OrderDeletedV1Notification))]
        public Task<ActionResult> Post([FromBody] OrderDeletedV1Notification value) => PublishNotification(value);


        private async Task<ActionResult> PublishNotification<T>(T value) where T : INotification
        {
            await _mediator.Publish(value);

            return NoContent();
        }
    }
}
