using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperFake.Shared.Domain;
using System.Threading.Tasks;

namespace SuperFake.Orders.Api.Controllers
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
        [Route("api/[controller]/" + nameof(ProductCreatedV1Notification))]
        public Task<ActionResult> Post([FromBody] ProductCreatedV1Notification value) => PublishNotification(value);

        [HttpPost]
        [Route("api/[controller]/" + nameof(ProductUpdatedV1Notification))]
        public Task<ActionResult> Post([FromBody] ProductUpdatedV1Notification value) => PublishNotification(value);

        [HttpPost]
        [Route("api/[controller]/" + nameof(ProductDeletedV1Notification))]
        public Task<ActionResult> Post([FromBody] ProductDeletedV1Notification value) => PublishNotification(value);

        
        [HttpPost]
        [Route("api/[controller]/" + nameof(CustomerCreatedV1Notification))]
        public Task<ActionResult> Post([FromBody] CustomerCreatedV1Notification value) => PublishNotification(value);

        [HttpPost]
        [Route("api/[controller]/" + nameof(CustomerUpdatedV1Notification))]
        public Task<ActionResult> Post([FromBody] CustomerUpdatedV1Notification value) => PublishNotification(value);

        [HttpPost]
        [Route("api/[controller]/" + nameof(CustomerDeletedV1Notification))]
        public Task<ActionResult> Post([FromBody] CustomerDeletedV1Notification value) => PublishNotification(value);


        private async Task<ActionResult> PublishNotification<T>(T value) where T: INotification
        {
            await _mediator.Publish(value);

            return NoContent();
        }
    }
}
