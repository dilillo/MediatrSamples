using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SuperSimple.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TornadoSpottedController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TornadoSpottedController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/TornadoSpotted
        [HttpGet]
        public async Task<string> Get()
        {
            await _mediator.Publish(new TornadoSpottedNotification());

            return "received.";
        }
    }
}
