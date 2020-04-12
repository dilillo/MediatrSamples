using MediatR;
using System.Collections.Generic;

namespace SuperSimple.Web
{
    public class WeatherForecastRequest : IRequest<List<WeatherForecast>>
    {
    }
}
