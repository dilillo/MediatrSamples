using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SuperFake.Shared.Domain;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Customers.Api
{
    public class NotificationSender : 
        INotificationHandler<CustomerCreatedV1Notification>, 
        INotificationHandler<CustomerUpdatedV1Notification>, 
        INotificationHandler<CustomerDeletedV1Notification>
    {
        private readonly string _serviceBusConnectionString;
        private readonly string _topicName;
       
        public NotificationSender(IConfiguration configuration)
        {
            _serviceBusConnectionString = configuration["ServiceBusConnectionString"];
            _topicName = "customers";
        }

        public Task Handle(CustomerCreatedV1Notification notification, CancellationToken cancellationToken) => SendNotificationToAzure(notification);

        public Task Handle(CustomerUpdatedV1Notification notification, CancellationToken cancellationToken) => SendNotificationToAzure(notification);

        public Task Handle(CustomerDeletedV1Notification notification, CancellationToken cancellationToken) => SendNotificationToAzure(notification);

        private async Task SendNotificationToAzure(object notification)
        {
            var topicClient = new TopicClient(_serviceBusConnectionString, _topicName);
            var notificationJson = JsonConvert.SerializeObject(notification);
            var message = new Message(Encoding.UTF8.GetBytes(notificationJson));

            message.ContentType = notification.GetType().Name;

            // Send the message to the topic.
            await topicClient.SendAsync(message);

            await topicClient.CloseAsync();
        }
    }
}