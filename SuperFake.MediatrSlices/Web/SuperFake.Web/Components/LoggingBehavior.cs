using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Web.Components
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            System.Diagnostics.Debug.WriteLine($"Received {typeof(TRequest).Name}");

            var response = await next();

            System.Diagnostics.Debug.WriteLine($"Returned {typeof(TResponse).Name}");

            return response;
        }
    }
}
