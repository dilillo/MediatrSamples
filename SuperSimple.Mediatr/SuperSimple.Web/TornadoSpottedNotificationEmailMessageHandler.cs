using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SuperSimple.Web
{
    public class TornadoSpottedNotificationEmailMessageHandler : INotificationHandler<TornadoSpottedNotification>
    {
        public Task Handle(TornadoSpottedNotification notification, CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine("Email message sent!.");

            return Task.CompletedTask;
        }
    }
}
