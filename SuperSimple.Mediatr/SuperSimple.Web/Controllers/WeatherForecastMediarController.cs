using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperSimple.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastMediatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherForecastMediatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<List<WeatherForecast>> Get()
        {
            return _mediator.Send(new WeatherForecastRequest());
        }
    }
}
