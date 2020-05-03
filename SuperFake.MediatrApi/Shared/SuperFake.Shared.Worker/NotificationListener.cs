using Microsoft.Azure.ServiceBus;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperFake.Shared.Worker
{
    public class NotificationListener
    {
        private readonly ISubscriptionClient _subscriptionClient;
        private readonly HttpClient _httpClient;
        private readonly string _notificationsApiUrl;

        public NotificationListener(HttpClient httpClient, string serviceBusConnectionString, string topicName, string subscriptionName, string notificationsApiUrl)
        {
            _httpClient = httpClient;
            _subscriptionClient = new SubscriptionClient(serviceBusConnectionString, topicName, subscriptionName);
            _notificationsApiUrl = notificationsApiUrl;
        }

        public void StartListening()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        public async Task StopListening()
        {
            await _subscriptionClient.CloseAsync();
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var notificationType = message.ContentType;
            var serializedNotification = Encoding.UTF8.GetString(message.Body);
            var response = await _httpClient.PostAsync(_notificationsApiUrl + notificationType, new StringContent(serializedNotification));

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"failed to send {message.ContentType}: statuscode = {response.StatusCode}, reason = {response.ReasonPhrase}");

                return;
            }

            if (token.IsCancellationRequested)
                return;

            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");

            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");

            return Task.CompletedTask;
        }
    }
}
