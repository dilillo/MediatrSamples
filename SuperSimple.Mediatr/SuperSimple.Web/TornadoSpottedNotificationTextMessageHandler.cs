using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SuperSimple.Web
{
    public class TornadoSpottedNotificationTextMessageHandler : INotificationHandler<TornadoSpottedNotification>
    {
        public Task Handle(TornadoSpottedNotification notification, CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine("Text message sent!.");

            return Task.CompletedTask;
        }
    }
}
